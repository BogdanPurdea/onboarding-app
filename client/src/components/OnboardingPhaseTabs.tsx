import { TimelinePhase } from '../types/index'
import type { OnboardingPhaseTabsProps } from '../types/components'

export function OnboardingPhaseTabs({
  activePhase,
  onChangePhase,
  phaseStats
}: OnboardingPhaseTabsProps) {
  const tabs = [
    { key: TimelinePhase.WeekOne, label: 'Week 1' },
    { key: TimelinePhase.WeekTwo, label: 'Week 2' },
    { key: TimelinePhase.WeekThree, label: 'Week 3' },
    { key: TimelinePhase.WeekFour, label: 'Week 4' }
  ]

  return (
    <div className="flex space-x-1 border-b border-slate-200 dark:border-slate-800">
      {tabs.map(phase => {
        const stats = phaseStats[phase.key] || { total: 0, completed: 0 }
        const isCurrent = activePhase === phase.key
        return (
          <button
            key={phase.key}
            onClick={() => onChangePhase(phase.key)}
            className={`flex-1 pb-3 text-sm font-semibold transition-colors duration-200 border-b-2 focus:outline-none cursor-pointer ${
              isCurrent
                ? 'border-indigo-600 text-indigo-600 dark:border-orange-400 dark:text-orange-300'
                : 'border-transparent text-slate-500 hover:text-slate-800 dark:text-slate-400 dark:hover:text-slate-200'
            }`}
          >
            <span>{phase.label}</span>
            <span className={`ml-2 text-xs px-2 py-0.5 rounded-full ${
              isCurrent
                ? 'bg-indigo-100 text-indigo-700 dark:bg-orange-950/20 dark:text-orange-300'
                : 'bg-slate-100 text-slate-600 dark:bg-slate-800 dark:text-slate-400'
            }`}>
              {stats.completed}/{stats.total}
            </span>
          </button>
        )
      })}
    </div>
  )
}
