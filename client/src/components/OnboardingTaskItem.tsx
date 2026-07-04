import type { OnboardingTaskItemProps } from '../types/components'

export function OnboardingTaskItem({
  task,
  isCompleted,
  isUnlocked,
  onToggle,
  prerequisiteTitles
}: OnboardingTaskItemProps) {
  return (
    <div
      className={`p-5 border rounded-xl bg-white shadow-sm transition duration-200 flex items-start gap-4 ${
        !isUnlocked
          ? 'border-slate-100 bg-slate-50/50 opacity-60'
          : isCompleted
            ? 'border-emerald-200 bg-emerald-50/10'
            : 'border-slate-200 hover:border-slate-300'
      }`}
    >
      {/* Checkbox wrapper */}
      <div className="flex items-center h-6">
        <input
          type="checkbox"
          id={`task-${task.id}`}
          checked={isCompleted}
          disabled={!isUnlocked}
          onChange={onToggle}
          className={`h-5 w-5 rounded border-slate-300 text-indigo-600 focus:ring-indigo-500 focus:ring-offset-2 transition-colors duration-150 ${
            !isUnlocked
              ? 'cursor-not-allowed text-slate-300 bg-slate-100'
              : 'cursor-pointer'
          }`}
        />
      </div>

      {/* Task Content */}
      <div className="flex-1 min-w-0">
        <label
          htmlFor={`task-${task.id}`}
          className={`block text-base font-semibold leading-6 select-none ${
            !isUnlocked
              ? 'text-slate-400 cursor-not-allowed'
              : isCompleted
                ? 'text-slate-500 line-through'
                : 'text-slate-900 cursor-pointer'
          }`}
        >
          {task.title}
        </label>
        <p className={`mt-1 text-sm leading-relaxed ${
          isCompleted ? 'text-slate-400' : 'text-slate-600'
        }`}>
          {task.description}
        </p>

        {/* Prerequisites Locks Warning */}
        {!isUnlocked && prerequisiteTitles.length > 0 && (
          <div className="mt-3 flex items-center gap-1.5 text-xs font-medium text-amber-700 bg-amber-50 border border-amber-100 rounded-md px-2.5 py-1.5 w-fit">
            <span>
              Requires: {prerequisiteTitles.join(', ')}
            </span>
          </div>
        )}
      </div>
    </div>
  )
}
