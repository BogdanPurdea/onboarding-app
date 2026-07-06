import type { OnboardingProgressHeaderProps } from '../../types/components'

export function OnboardingProgressHeader({
  completedCount,
  totalCount,
  progressPercent,
  onCopyLink,
  linkCopied
}: OnboardingProgressHeaderProps) {
  return (
    <div className="p-6 card-base">
      <div className="flex items-center justify-between">
        <span className="text-sm font-semibold text-slate-500 dark:text-slate-400">
          Overall Completion
        </span>
        <div className="flex items-center gap-3">
          <span className="text-sm font-bold text-indigo-600 dark:text-orange-300">
            {completedCount} / {totalCount} tasks ({progressPercent}%)
          </span>
          {onCopyLink && (
            <button
              onClick={onCopyLink}
              title="Copy recovery link to clipboard"
              className="flex items-center gap-1.5 text-xs font-medium px-2.5 py-1 rounded-md border transition-colors
                border-slate-200 text-slate-500 hover:text-indigo-600 hover:border-indigo-300
                dark:border-slate-700 dark:text-slate-400 dark:hover:text-orange-300 dark:hover:border-orange-400"
            >
              {linkCopied ? (
                <>
                  <svg xmlns="http://www.w3.org/2000/svg" className="w-3.5 h-3.5" viewBox="0 0 20 20" fill="currentColor">
                    <path fillRule="evenodd" d="M16.707 5.293a1 1 0 010 1.414l-8 8a1 1 0 01-1.414 0l-4-4a1 1 0 011.414-1.414L8 12.586l7.293-7.293a1 1 0 011.414 0z" clipRule="evenodd" />
                  </svg>
                  Copied!
                </>
              ) : (
                <>
                  <svg xmlns="http://www.w3.org/2000/svg" className="w-3.5 h-3.5" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round">
                    <path d="M10 13a5 5 0 007.54.54l3-3a5 5 0 00-7.07-7.07l-1.72 1.71" />
                    <path d="M14 11a5 5 0 00-7.54-.54l-3 3a5 5 0 007.07 7.07l1.71-1.71" />
                  </svg>
                  Copy link
                </>
              )}
            </button>
          )}
        </div>
      </div>
      <div className="mt-3 w-full bg-slate-100 rounded-full h-2 dark:bg-slate-800">
        <div
          className="bg-indigo-600 h-2 rounded-full transition-all duration-300 ease-out dark:bg-orange-300"
          style={{ width: `${progressPercent}%` }}
        />
      </div>
    </div>
  )
}

