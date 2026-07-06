import { ChatMessageList } from './ChatMessageList'
import { ChatSuggestionChips } from './ChatSuggestionChips'
import { ChatInput } from './ChatInput'
import type { ChatDrawerProps } from '../../types/components'


export function ChatDrawer({
  isOpen,
  onClose,
  role,
  messages,
  isTyping,
  onSendMessage
}: ChatDrawerProps) {
  if (!isOpen) return null

  return (
    <div
      className="fixed inset-0 z-50 flex justify-end bg-slate-900/40 animate-fade-in"
      onClick={onClose}
    >
      {/* Drawer Container */}
      <div
        className="w-full sm:w-[440px] h-full bg-white dark:bg-slate-900 border-l border-slate-200 dark:border-slate-800 shadow-2xl flex flex-col justify-between animate-slide-in-right"
        onClick={e => e.stopPropagation()}
      >
        {/* Header */}
        <div className="p-4 border-b border-slate-100 dark:border-slate-800/80 flex items-center justify-between bg-slate-50/50 dark:bg-slate-900/50">
          <div className="flex items-center gap-3">
            <div className="w-9 h-9 rounded-full bg-indigo-100 dark:bg-orange-950/40 flex items-center justify-center text-indigo-600 dark:text-orange-400 font-bold text-sm">
              M
            </div>
            <div>
              <h3 className="text-sm font-bold text-slate-800 dark:text-slate-100">
                Ask Meridian
              </h3>
              <p className="text-[10px] text-emerald-600 dark:text-emerald-400 font-semibold flex items-center gap-1">
                <span className="w-1.5 h-1.5 rounded-full bg-emerald-500 inline-block animate-pulse"></span>
                Navigator Bot • Active
              </p>
            </div>
          </div>

          {/* Close Button */}
          <button
            onClick={onClose}
            className="p-1.5 rounded-lg border border-slate-200 bg-white hover:bg-slate-50 text-slate-400 hover:text-slate-600 dark:border-slate-800 dark:bg-slate-800 dark:hover:bg-slate-700/80 dark:text-slate-500 dark:hover:text-slate-300 transition duration-150 cursor-pointer focus-ring"
            aria-label="Close Chat"
          >
            <svg className="w-4 h-4" fill="none" viewBox="0 0 24 24" stroke="currentColor" strokeWidth={2.5}>
              <path strokeLinecap="round" strokeLinejoin="round" d="M6 18L18 6M6 6l12 12" />
            </svg>
          </button>
        </div>

        {/* Message Log */}
        <ChatMessageList messages={messages} isTyping={isTyping} />

        {/* Suggestion Prompt Chips */}
        <ChatSuggestionChips role={role} onChipClick={onSendMessage} isTyping={isTyping} />

        {/* Input Bar */}
        <ChatInput onSendMessage={onSendMessage} isTyping={isTyping} />
      </div>
    </div>
  )
}
