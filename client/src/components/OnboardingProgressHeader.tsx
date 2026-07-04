import type { OnboardingProgressHeaderProps } from '../types/components'

export function OnboardingProgressHeader({
  completedCount,
  totalCount,
  progressPercent
}: OnboardingProgressHeaderProps) {
  return (
    <div className="p-6 border border-slate-200 rounded-xl bg-white shadow-sm dark:border-slate-800 dark:bg-slate-900/50">
      <div className="flex items-center justify-between">
        <span className="text-sm font-semibold text-slate-500 dark:text-slate-400">
          Overall Completion
        </span>
        <span className="text-sm font-bold text-indigo-600 dark:text-indigo-400">
          {completedCount} / {totalCount} tasks ({progressPercent}%)
        </span>
      </div>
      <div className="mt-3 w-full bg-slate-100 rounded-full h-2 dark:bg-slate-800">
        <div
          className="bg-indigo-600 h-2 rounded-full transition-all duration-300 ease-out"
          style={{ width: `${progressPercent}%` }}
        />
      </div>
    </div>
  )
}
