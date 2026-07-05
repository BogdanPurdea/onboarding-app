import type { OnboardingTaskItemProps } from '../../types/components'
import React from 'react'

export function OnboardingTaskItem({
  task,
  isCompleted,
  isUnlocked,
  onToggle,
  prerequisiteTitles,
  onClick,
  isLoadingInstructions
}: OnboardingTaskItemProps) {
  const handleCardClick = (e: React.MouseEvent) => {
    // If user clicked the checkbox, let the default toggle proceed and ignore instruction drawer
    if ((e.target as HTMLElement).closest('input[type="checkbox"]')) {
      return
    }
    // Prevent standard label behavior which triggers the checkbox
    if ((e.target as HTMLElement).closest('label')) {
      e.preventDefault()
    }
    if (isUnlocked && onClick) {
      onClick()
    }
  }

  return (
    <div
      onClick={handleCardClick}
      className={[
        'p-5 card-base transition duration-200 flex items-start gap-4',
        !isUnlocked
          ? 'border-slate-100 bg-slate-50/50 opacity-60 dark:border-slate-900/40 dark:bg-slate-950/20'
          : isCompleted
            ? 'border-emerald-200 bg-emerald-50/10 dark:border-emerald-950/40 dark:bg-emerald-950/5 cursor-pointer hover:border-emerald-300 dark:hover:border-emerald-900/80 hover:shadow-sm'
            : 'cursor-pointer hover:border-indigo-500/50 dark:hover:border-orange-500/50 hover:shadow-sm'
      ].join(' ')}
    >
      {/* Checkbox wrapper */}
      <div className="flex items-center h-6">
        <input
          type="checkbox"
          id={`task-${task.id}`}
          checked={isCompleted}
          disabled={!isUnlocked}
          onChange={onToggle}
          className={[
            'h-5 w-5 rounded border-slate-300 text-indigo-600 focus:ring-indigo-500 focus:ring-offset-2 transition-colors duration-150 dark:border-slate-700 dark:bg-slate-800 dark:text-orange-300',
            !isUnlocked
              ? 'cursor-not-allowed text-slate-300 bg-slate-100 dark:bg-slate-900 dark:text-slate-700'
              : 'cursor-pointer'
          ].join(' ')}
        />
      </div>

      {/* Task Content */}
      <div className="flex-1 min-w-0">
        <div className="flex flex-wrap items-center gap-x-2">
          <label
            htmlFor={`task-${task.id}`}
            className={[
              'block text-base font-semibold leading-6 select-none',
              !isUnlocked
                ? 'text-slate-400 cursor-not-allowed dark:text-slate-500'
                : isCompleted
                  ? 'text-slate-500 line-through cursor-pointer'
                  : 'text-slate-900 cursor-pointer dark:text-slate-100'
            ].join(' ')}
          >
            {task.title}
          </label>
          {isLoadingInstructions && (
            <span className="inline-flex items-center text-xs font-medium text-indigo-600 dark:text-orange-400 animate-pulse">
              ⏳ Loading...
            </span>
          )}
        </div>
        <p className={['mt-1 text-sm leading-relaxed', isCompleted ? 'text-slate-400' : 'text-slate-600 dark:text-slate-400'].join(' ')}>
          {task.description}
        </p>

        {/* Prerequisites Locks Warning */}
        {!isUnlocked && prerequisiteTitles.length > 0 && (
          <div className="mt-3 flex items-center gap-1.5 text-xs font-medium text-amber-700 bg-amber-50 border border-amber-100 rounded-md px-2.5 py-1.5 w-fit dark:text-amber-300 dark:bg-amber-950/30 dark:border-amber-900/20">
            <span>
              Requires: {prerequisiteTitles.join(', ')}
            </span>
          </div>
        )}
      </div>
    </div>
  )
}
