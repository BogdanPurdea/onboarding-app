import type { DepartmentCardProps } from '../../types/components'

export function DepartmentCard({ dept, onSelectRole }: DepartmentCardProps) {
  return (
    <button
      onClick={() => onSelectRole(dept.roleKey)}
      className="flex flex-col items-start p-5 text-left card-base focus-ring hover:border-indigo-500 hover:shadow-md transition duration-200 w-full group dark:hover:border-indigo-500/50"
    >
      <div className="flex items-center justify-between w-full">
        <span className="text-lg font-semibold text-slate-950 group-hover:text-indigo-600 transition-colors dark:text-slate-100 dark:group-hover:text-orange-300">
          {dept.name}
        </span>
        <svg
          className="w-5 h-5 text-slate-400 group-hover:text-indigo-500 transition-colors transform group-hover:translate-x-1 dark:text-slate-500 dark:group-hover:text-orange-300"
          fill="none"
          viewBox="0 0 24 24"
          stroke="currentColor"
          strokeWidth={2}
        >
          <path strokeLinecap="round" strokeLinejoin="round" d="M9 5l7 7-7 7" />
        </svg>
      </div>
      <span className="mt-1 text-sm text-slate-500 dark:text-slate-400">
        {dept.tagline}
      </span>
    </button>
  )
}
