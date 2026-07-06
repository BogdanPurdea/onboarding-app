import { getSuggestionChips } from '../../utils/chatLogic'
import type { ChatSuggestionChipsProps } from '../../types/components'

export function ChatSuggestionChips({ role, onChipClick, isTyping }: ChatSuggestionChipsProps) {
  const chips = getSuggestionChips(role)

  return (
    <div className="p-3 bg-white dark:bg-slate-900 border-t border-slate-100 dark:border-slate-800/80">
      <span className="text-[10px] font-bold text-slate-400 dark:text-slate-500 uppercase tracking-wider block mb-2 px-1">
        Suggested Questions
      </span>
      <div className="flex flex-wrap gap-1.5 max-h-[88px] overflow-y-auto">
        {chips.map((chip, idx) => (
          <button
            key={idx}
            onClick={() => onChipClick(chip)}
            disabled={isTyping}
            className="text-xs text-left px-3 py-1.5 rounded-full border border-slate-200 text-slate-600 bg-slate-50 hover:bg-indigo-50 hover:text-indigo-600 hover:border-indigo-200 transition-colors disabled:opacity-50 disabled:cursor-not-allowed dark:border-slate-800 dark:text-slate-300 dark:bg-slate-800/40 dark:hover:bg-orange-950/20 dark:hover:text-orange-300 dark:hover:border-orange-400/40 cursor-pointer"
          >
            {chip}
          </button>
        ))}
      </div>
    </div>
  )
}
