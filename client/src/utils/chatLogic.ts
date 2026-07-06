import type { ChatMessage } from '../types/index'
import { CHAT_CONFIG } from '../config/chatConfig'

/**
 * Returns a human-friendly department name from the role key.
 */
export function getDeptName(role: string | null): string {
  if (!role) return 'Meridian'
  return role.charAt(0).toUpperCase() + role.slice(1)
}

/**
 * Returns dynamic helper quick-chips depending on the active role.
 */
export function getSuggestionChips(role: string | null): string[] {
  switch (role) {
    case 'engineering':
      return [
        'How do I set up my dev environment?',
        'What is the docker setup command?',
        'Who is my onboarding buddy?',
        'Where is the developer wiki?'
      ]
    case 'sales':
      return [
        'How do I log in to Salesforce CRM?',
        'Who is the Sales department contact?',
        'What are my targets for Week 1?',
        'Where are the outreach templates?'
      ]
    default:
      return [
        'Where can I find my HR paperwork?',
        'When is the next company welcome session?',
        'How do I request help?',
        'What are my week 1 checklist items?'
      ]
  }
}

/**
 * Resolves an automated assistant text reply matching keywords in user queries.
 */
export function getMockResponse(userInput: string, role: string | null): string {
  const query = userInput.toLowerCase()
  const deptName = getDeptName(role)

  if (query.includes('hello') || query.includes('hi') || query.includes('hey')) {
    return `Hello! How can I help you with your ${deptName} onboarding today?`
  }

  if (query.includes('docker')) {
    return "To run the application locally, you'll need Docker Desktop. After installing, run `docker compose up` in your terminal inside the project root to spin up the API and database stack. Verify everything is healthy at http://localhost:5000/healthz."
  }

  if (query.includes('setup') || query.includes('environment') || query.includes('install')) {
    if (role === 'engineering') {
      return "Your development setup requires: VS Code/Visual Studio, Git, Node.js, and the .NET 10 SDK. Check the task 'Set Up Your Development Environment' on your checklist for detailed steps."
    }
    return "To get started with your environment, verify your logins for company Slack and CRM (Salesforce) or accounting tools (QuickBooks). If you are missing credentials, reach out to your department coordinator."
  }

  if (query.includes('buddy') || query.includes('who is my')) {
    if (role === 'engineering') {
      return "Your onboarding buddy is Liam O'Brien (Senior Engineer). He is your go-to contact for architecture questions and code reviews. You can schedule a Google Meet or ping him on Slack!"
    }
    if (role === 'sales') {
      return "Your onboarding buddy is Sofia Reyes (Account Executive). She is a top performer in enterprise deals and can help you shadow live client demo calls."
    }
    return "Your onboarding buddy is assigned by your manager on Day 1. Look at the 'Team Contacts' section on your dashboard to see your assigned buddy's email and Slack details."
  }

  if (query.includes('salesforce') || query.includes('crm')) {
    return "Log in to Salesforce CRM using your company credentials sent to your work email on Day 1. Once logged in, make sure to set up your outreach pipeline and task notifications."
  }

  if (query.includes('wiki') || query.includes('documentation') || query.includes('document')) {
    return "Our internal engineering wiki contains architecture blueprints, styling standards, and deploy guidelines. You can access it via the link in the shared Engineering welcome document."
  }

  if (query.includes('welcome') || query.includes('session')) {
    return "The company-wide welcome session is held on Day 1. You will meet the founders, learn about Meridian's mission, and take a quick virtual tour of our offices."
  }

  if (query.includes('paperwork') || query.includes('hr') || query.includes('contract')) {
    return "All employment contracts, tax forms, and benefits selections are processed in the HR portal. Ensure you submit your signed paperwork by the end of your second day."
  }

  if (query.includes('help') || query.includes('manager') || query.includes('lead')) {
    return `If you have blocker issues, you can reach out directly to your manager or team lead. For the ${deptName} department, check the Team Contacts dashboard for direct emails, Slack IDs, and Google Meet scheduling links.`
  }

  return CHAT_CONFIG.FALLBACK_RESPONSE
}

/**
 * Pings the local Ollama instance tags endpoint to verify if the specified model is present.
 */
export async function checkModelAvailability(modelName: string): Promise<boolean> {
  try {
    const res = await fetch(`${CHAT_CONFIG.OLLAMA_BASE_URL}/api/tags`)
    if (!res.ok) return false
    const data = await res.json()
    const models = data.models || []
    return models.some((m: any) => m.name.startsWith(modelName))
  } catch {
    return false
  }
}

/**
 * Builds the dynamic welcome text greeting matching the selected department role.
 */
export function getWelcomeMessage(role: string | null): string {
  const deptName = getDeptName(role)
  return role
    ? CHAT_CONFIG.ROLE_WELCOME_TEMPLATE(deptName)
    : CHAT_CONFIG.DEFAULT_WELCOME
}

/**
 * Instantiates a ChatMessage object with generated UUIDs and timestamps.
 */
export function createMessage(sender: 'user' | 'assistant', text: string, id?: string): ChatMessage {
  return {
    id: id || crypto.randomUUID(),
    sender,
    text,
    timestamp: new Date()
  }
}

