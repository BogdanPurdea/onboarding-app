import { useEffect, useRef } from 'react'
import type { ChatMessageListProps } from '../../types/components'

export function ChatMessageList({ messages, isTyping }: ChatMessageListProps) {
  const messagesEndRef = useRef<HTMLDivElement>(null)

  // Scroll to bottom whenever messages or typing state changes
  useEffect(() => {
    messagesEndRef.current?.scrollIntoView({ behavior: 'smooth' })
  }, [messages, isTyping])

  return (
    <div className="flex-1 overflow-y-auto p-4 space-y-4 bg-slate-50/30 dark:bg-slate-950/10">
      {messages.map(msg => (
        <div
          key={msg.id}
          className={`flex ${msg.sender === 'user' ? 'justify-end' : 'justify-start'}`}
        >
          <div
            className={`max-w-[85%] rounded-2xl px-4 py-2.5 text-sm leading-relaxed shadow-xs
              ${
                msg.sender === 'user'
                  ? 'bg-indigo-600 text-white rounded-br-none dark:bg-orange-500'
                  : 'bg-white text-slate-700 border border-slate-100 rounded-bl-none dark:bg-slate-800 dark:border-slate-800 dark:text-slate-200'
              }`}
          >
            <p className="whitespace-pre-wrap">{msg.text}</p>
            <span className={`text-[9px] block text-right mt-1.5 ${msg.sender === 'user' ? 'text-indigo-200 dark:text-orange-200' : 'text-slate-400 dark:text-slate-500'}`}>
              {msg.timestamp.toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' })}
            </span>
          </div>
        </div>
      ))}

      {/* Typing Indicator */}
      {isTyping && (
        <div className="flex justify-start">
          <div className="bg-white border border-slate-100 rounded-2xl rounded-bl-none px-4 py-3 dark:bg-slate-800 dark:border-slate-800">
            <div className="flex items-center gap-1">
              <span className="w-2.5 h-2.5 bg-slate-300 rounded-full animate-bounce dark:bg-slate-600"></span>
              <span className="w-2.5 h-2.5 bg-slate-300 rounded-full animate-bounce [animation-delay:0.2s] dark:bg-slate-600"></span>
              <span className="w-2.5 h-2.5 bg-slate-300 rounded-full animate-bounce [animation-delay:0.4s] dark:bg-slate-600"></span>
            </div>
          </div>
        </div>
      )}
      <div ref={messagesEndRef} />
    </div>
  )
}
