import { useState, useEffect } from 'react'
import { ChatFab } from './ChatFab'
import { ChatDrawer } from './ChatDrawer'
import { getDeptName, getMockResponse } from '../../utils/chatLogic'
import type { ChatMessage } from '../../types/index'
import type { AskMeridianChatProps } from '../../types/components'

export function AskMeridianChat({ role }: AskMeridianChatProps) {
  const [isOpen, setIsOpen] = useState(false)
  const [messages, setMessages] = useState<ChatMessage[]>([])
  const [isTyping, setIsTyping] = useState(false)

  // Initialize messages on role change or mount
  useEffect(() => {
    const deptName = getDeptName(role)
    const welcomeText = role
      ? `Welcome to the ${deptName} team! I'm your Meridian Navigator assistant. Ask me anything about your department tasks, development requirements, or tool setups.`
      : "Hi! Welcome to Meridian. I'm your onboarding assistant. Select a department role or ask me general onboarding questions to get started."

    setMessages([
      {
        id: 'welcome',
        sender: 'assistant',
        text: welcomeText,
        timestamp: new Date()
      }
    ])
    setIsTyping(false)
  }, [role])

  const handleSendMessage = (text: string) => {
    if (!text.trim() || isTyping) return

    const userMsg: ChatMessage = {
      id: crypto.randomUUID(),
      sender: 'user',
      text,
      timestamp: new Date()
    }

    setMessages(prev => [...prev, userMsg])
    setIsTyping(true)

    // Simulate thinking/typing delay
    setTimeout(() => {
      const assistantMsg: ChatMessage = {
        id: crypto.randomUUID(),
        sender: 'assistant',
        text: getMockResponse(text, role),
        timestamp: new Date()
      }
      setMessages(prev => [...prev, assistantMsg])
      setIsTyping(false)
    }, 750)
  }

  return (
    <>
      <ChatFab onClick={() => setIsOpen(true)} />
      <ChatDrawer
        isOpen={isOpen}
        onClose={() => setIsOpen(false)}
        role={role}
        messages={messages}
        isTyping={isTyping}
        onSendMessage={handleSendMessage}
      />
    </>
  )
}
