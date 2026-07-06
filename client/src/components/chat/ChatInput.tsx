import { useState, type FormEvent } from 'react'
import type { ChatInputProps } from '../../types/components'

export function ChatInput({ onSendMessage, isTyping }: ChatInputProps) {
  const [input, setInput] = useState('')

  const handleSubmit = (e: FormEvent) => {
    e.preventDefault()
    if (!input.trim() || isTyping) return
    onSendMessage(input)
    setInput('')
  }

  return (
    <form
      onSubmit={handleSubmit}
      className="p-3 border-t border-slate-100 dark:border-slate-800/80 bg-slate-50/50 dark:bg-slate-900/50 flex items-center gap-2"
    >
      <input
        type="text"
        value={input}
        onChange={e => setInput(e.target.value)}
        placeholder="Ask about setups, buddies, etc..."
        disabled={isTyping}
        className="flex-1 bg-white border border-slate-200 rounded-xl px-4 py-2 text-sm text-slate-800 placeholder-slate-400 focus:outline-none focus:ring-2 focus:ring-indigo-500 focus:border-transparent dark:bg-slate-800 dark:border-slate-800 dark:text-slate-100 dark:placeholder-slate-500 dark:focus:ring-orange-400"
      />
      <button
        type="submit"
        disabled={!input.trim() || isTyping}
        className="p-2 rounded-xl bg-indigo-600 hover:bg-indigo-500 text-white disabled:opacity-40 disabled:cursor-not-allowed transition-all duration-150 shadow-sm shadow-indigo-600/10 cursor-pointer dark:bg-orange-500 dark:hover:bg-orange-400 dark:shadow-orange-500/10 focus-ring"
        aria-label="Send Message"
      >
        <svg
          xmlns="http://www.w3.org/2000/svg"
          className="w-5 h-5 transform rotate-90"
          fill="none"
          viewBox="0 0 24 24"
          stroke="currentColor"
          strokeWidth={2}
        >
          <path strokeLinecap="round" strokeLinejoin="round" d="M12 19l9 2-9-18-9 18 9-2zm0 0v-8" />
        </svg>
      </button>
    </form>
  )
}
