import { fetchTasks, fetchTaskInstructions } from './tasksApi'
import { fetchDashboardData } from './dashboardApi'
import { CHAT_CONFIG } from '../config/chatConfig'
import { CHAT_TOOLS } from '../config/chatTools'
import type { ChatMessage } from '../types/index'

interface OllamaMessage {
  role: string
  content: string
  tool_calls?: any[]
}

/**
 * Communicates directly with the local Ollama instance.
 * If the model requests a tool call to fetch onboarding tasks, resolves it by invoking the client-side API,
 * appends the tool payload, and queries the model again to generate a final response.
 */
export async function sendChatMessage(messages: ChatMessage[], activeRole: string | null): Promise<string> {
  const ollamaMessages: OllamaMessage[] = [
    { role: 'system', content: CHAT_CONFIG.SYSTEM_PROMPT },
    ...messages.map(msg => ({
      role: msg.sender,
      content: msg.text
    }))
  ]

  const res = await fetch(CHAT_CONFIG.OLLAMA_CHAT_ENDPOINT, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({
      model: CHAT_CONFIG.MODEL_NAME,
      messages: ollamaMessages,
      stream: false,
      tools: CHAT_TOOLS
    })
  })

  if (!res.ok) {
    throw new Error(`Ollama fetch failed (${res.status}). Verify Ollama is running and OLLAMA_ORIGINS is set.`)
  }

  const data = await res.json()
  const assistantMessage = data.message

  // Intercept and resolve Tool Calls
  if (assistantMessage.tool_calls && assistantMessage.tool_calls.length > 0) {
    const toolCall = assistantMessage.tool_calls[0]
    let toolResultContent = ''

    if (toolCall.function.name === 'get_tasks') {
      const args = toolCall.function.arguments || {}
      const targetRole = args.role || activeRole || 'engineering'
      const tasks = await fetchTasks(targetRole)
      const simplifiedTasks = tasks.map(t => ({
        id: t.id,
        title: t.title,
        description: t.description
      }))
      toolResultContent = JSON.stringify(simplifiedTasks)

    } else if (toolCall.function.name === 'get_dashboard') {
      const args = toolCall.function.arguments || {}
      const targetRole = args.role || activeRole || 'engineering'
      const dbData = await fetchDashboardData(targetRole)
      // Extract only vital properties to preserve LLM token context
      const simplifiedDashboard = {
        departmentName: dbData.departmentName,
        weeklySchedule: dbData.weeklySchedule,
        contacts: dbData.contacts.map(c => ({
          name: c.name,
          role: c.role,
          email: c.email,
          slackMemberId: c.slackMemberId,
          googleMeetUrl: c.googleMeetUrl,
          bio: c.bio
        }))
      }
      toolResultContent = JSON.stringify(simplifiedDashboard)

    } else if (toolCall.function.name === 'get_task_instructions') {
      const args = toolCall.function.arguments || {}
      const taskId = args.taskId
      if (typeof taskId !== 'number') {
        throw new Error('Invalid taskId provided for get_task_instructions')
      }
      const instructions = await fetchTaskInstructions(taskId)
      const simplifiedInstructions = {
        taskTitle: instructions.taskTitle,
        steps: instructions.steps
      }
      toolResultContent = JSON.stringify(simplifiedInstructions)
    }

    if (toolResultContent) {
      // Prepare the message history including the assistant's tool call request and the tool response
      const updatedHistory = [
        ...ollamaMessages,
        assistantMessage,
        {
          role: 'tool',
          content: toolResultContent
        }
      ]

      // Query Ollama again to summarize the tool results
      const secondRes = await fetch(CHAT_CONFIG.OLLAMA_CHAT_ENDPOINT, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
          model: CHAT_CONFIG.MODEL_NAME,
          messages: updatedHistory,
          stream: false
        })
      })

      if (!secondRes.ok) throw new Error('Tool resolution request failed')
      const secondData = await secondRes.json()
      return secondData.message.content
    }
  }

  return assistantMessage.content || ''
}
