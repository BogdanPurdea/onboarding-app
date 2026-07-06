import { useState, useEffect } from 'react'
import { ChatFab } from './ChatFab'
import { ChatDrawer } from './ChatDrawer'
import {
  getWelcomeMessage,
  checkModelAvailability,
  createMessage,
  getMockResponse
} from '../../utils/chatLogic'
import { sendChatMessage } from '../../utils/chatLlm'
import { CHAT_CONFIG } from '../../config/chatConfig'
import type { ChatMessage } from '../../types/index'
import type { AskMeridianChatProps } from '../../types/components'

export function AskMeridianChat({ role }: AskMeridianChatProps) {
  const [isOpen, setIsOpen] = useState(false)
  const [messages, setMessages] = useState<ChatMessage[]>([])
  const [isTyping, setIsTyping] = useState(false)
  const [isLlmActive, setIsLlmActive] = useState<boolean>(true)

  // Query local Ollama instance on mount to check if gemma4 is available
  useEffect(() => {
    checkModelAvailability(CHAT_CONFIG.MODEL_NAME)
      .then(available => setIsLlmActive(available))
  }, [])

  // Initialize messages on role change or mount
  useEffect(() => {
    const welcomeText = getWelcomeMessage(role)
    setMessages([createMessage('assistant', welcomeText, 'welcome')])
    setIsTyping(false)
  }, [role])

  const handleSendMessage = (text: string) => {
    if (!text.trim() || isTyping) return

    const userMsg = createMessage('user', text)
    const updatedMessages = [...messages, userMsg]
    setMessages(updatedMessages)
    setIsTyping(true)

    sendChatMessage(updatedMessages, role)
      .then(reply => {
        setMessages(prev => [...prev, createMessage('assistant', reply)])
      })
      .catch(err => {
        console.warn('Ollama communication error, falling back to local mock response:', err)
        setIsLlmActive(false) // Set to inactive when connection/model error happens
        const fallbackReply = getMockResponse(text, role)
        setMessages(prev => [...prev, createMessage('assistant', fallbackReply)])
      })
      .finally(() => {
        setIsTyping(false)
      })
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
        isLlmActive={isLlmActive}
      />
    </>
  )
}
