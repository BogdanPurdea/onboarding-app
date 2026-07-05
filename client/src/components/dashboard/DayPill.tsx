import type { DayScheduleDto } from '../../types/index'

export function DayPill({ day, type }: DayScheduleDto) {
  const isOffice = type === 'office'
  return (
    <div className="flex flex-col items-center gap-1.5 flex-1">
      <span className="text-xs font-medium text-slate-500 dark:text-slate-400 uppercase tracking-wide">
        {day.slice(0, 3)}
      </span>
      <span
        className={[
          'w-full text-center rounded-lg py-2 px-1 text-xs font-semibold',
          isOffice
            ? 'bg-indigo-100 text-indigo-700 dark:bg-orange-900/30 dark:text-orange-300'
            : 'bg-slate-100 text-slate-500 dark:bg-slate-800 dark:text-slate-400'
        ].join(' ')}
      >
        {isOffice ? 'Office' : 'Remote'}
      </span>
    </div>
  )
}
