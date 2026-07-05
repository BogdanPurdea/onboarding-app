import type { TaskInstructionsDrawerProps } from '../../types/components'

export function TaskInstructionsDrawer({ instructions, onClose }: TaskInstructionsDrawerProps) {
  // Format steps into a structured list block
  const formattedInstructions = instructions.steps
    .map((step, index) => `${index + 1}. ${step}`)
    .join('\n\n')

  return (
    <div
      className="fixed inset-0 z-50 flex items-end justify-center bg-slate-900/40 backdrop-blur-sm"
      onClick={onClose}
    >
      {/* Slide Up Panel */}
      <div
        className="w-full max-w-2xl bg-white dark:bg-slate-900 rounded-t-2xl shadow-2xl border-t border-slate-200 dark:border-slate-800 p-6 flex flex-col gap-4 animate-slide-up max-h-[85vh] overflow-y-auto"
        onClick={e => e.stopPropagation()}
      >
        {/* iOS style drag handle */}
        <div className="mx-auto w-12 h-1.5 rounded-full bg-slate-200 dark:bg-slate-700 mb-1" />

        {/* Title Bar */}
        <div className="flex items-start justify-between gap-4">
          <div className="min-w-0">
            <span className="text-xs font-semibold text-indigo-600 dark:text-orange-400 uppercase tracking-wider">
              Detailed Instructions
            </span>
            <h3 className="text-lg font-bold text-slate-900 dark:text-slate-100 truncate mt-0.5">
              {instructions.taskTitle}
            </h3>
          </div>
          {/* Explicit close button */}
          <button
            onClick={onClose}
            className="p-1.5 rounded-lg border border-slate-200 bg-white hover:bg-slate-50 text-slate-400 hover:text-slate-600 dark:border-slate-800 dark:bg-slate-800 dark:hover:bg-slate-700/80 dark:text-slate-500 dark:hover:text-slate-300 transition duration-150 focus-ring cursor-pointer"
            aria-label="Close instructions panel"
          >
            <svg className="w-5 h-5" fill="none" viewBox="0 0 24 24" stroke="currentColor" strokeWidth={2}>
              <path strokeLinecap="round" strokeLinejoin="round" d="M6 18L18 6M6 6l12 12" />
            </svg>
          </button>
        </div>

        {/* Instructions Body */}
        <div className="bg-slate-50 dark:bg-slate-800/40 border border-slate-100 dark:border-slate-800/60 p-5 rounded-xl">
          <p className="text-sm font-medium text-slate-700 dark:text-slate-300 whitespace-pre-wrap leading-relaxed">
            {formattedInstructions}
          </p>
        </div>

        <button
          onClick={onClose}
          className="text-xs text-slate-400 hover:text-slate-600 dark:hover:text-slate-300 transition-colors cursor-pointer text-center py-2"
        >
          Dismiss
        </button>
      </div>
    </div>
  )
}
