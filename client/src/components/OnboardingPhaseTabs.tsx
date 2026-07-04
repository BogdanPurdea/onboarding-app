interface OnboardingPhaseTabsProps {
  activePhase: number
  onChangePhase: (phase: number) => void
  phaseStats: { [key: number]: { total: number; completed: number } }
}

export function OnboardingPhaseTabs({
  activePhase,
  onChangePhase,
  phaseStats
}: OnboardingPhaseTabsProps) {
  const tabs = [
    { key: 1, label: 'Week 1' },
    { key: 2, label: 'Week 2' },
    { key: 3, label: 'Week 3' },
    { key: 4, label: 'Week 4' }
  ]

  return (
    <div className="flex space-x-1 border-b border-slate-200">
      {tabs.map(phase => {
        const stats = phaseStats[phase.key] || { total: 0, completed: 0 }
        const isCurrent = activePhase === phase.key
        return (
          <button
            key={phase.key}
            onClick={() => onChangePhase(phase.key)}
            className={`flex-1 pb-3 text-sm font-semibold transition-colors duration-200 border-b-2 focus:outline-none ${
              isCurrent
                ? 'border-indigo-600 text-indigo-600'
                : 'border-transparent text-slate-500 hover:text-slate-800'
            }`}
          >
            <span>{phase.label}</span>
            <span className={`ml-2 text-xs px-2 py-0.5 rounded-full ${
              isCurrent ? 'bg-indigo-100 text-indigo-700' : 'bg-slate-100 text-slate-600'
            }`}>
              {stats.completed}/{stats.total}
            </span>
          </button>
        )
      })}
    </div>
  )
}
