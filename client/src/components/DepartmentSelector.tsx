import { DEPARTMENTS, type DepartmentConfig } from '../config/departments'

interface DepartmentSelectorProps {
  onSelectRole: (roleKey: string) => void
}

export function DepartmentSelector({ onSelectRole }: DepartmentSelectorProps) {
  return (
    <div className="py-6">
      <div className="text-center mb-8">
        <h2 className="text-3xl font-bold tracking-tight text-slate-900">
          Welcome to Meridian
        </h2>
        <p className="mt-2 text-base text-slate-500">
          Select your team department to begin your first month onboarding checklist.
        </p>
      </div>

      <div className="grid gap-4 sm:grid-cols-2 lg:grid-cols-1">
        {DEPARTMENTS.map((dept: DepartmentConfig) => (
          <button
            key={dept.id}
            onClick={() => onSelectRole(dept.roleKey)}
            className="flex flex-col items-start p-5 text-left border border-slate-200 rounded-xl bg-white shadow-sm hover:border-indigo-500 hover:shadow-md transition duration-200 focus:outline-none focus:ring-2 focus:ring-indigo-500 focus:ring-offset-2 w-full group"
          >
            <div className="flex items-center justify-between w-full">
              <span className="text-lg font-semibold text-slate-950 group-hover:text-indigo-600 transition-colors">
                {dept.name}
              </span>
              <svg
                className="w-5 h-5 text-slate-400 group-hover:text-indigo-500 transition-colors transform group-hover:translate-x-1"
                fill="none"
                viewBox="0 0 24 24"
                stroke="currentColor"
                strokeWidth={2}
              >
                <path strokeLinecap="round" strokeLinejoin="round" d="M9 5l7 7-7 7" />
              </svg>
            </div>
            <span className="mt-1 text-sm text-slate-500">
              {dept.tagline}
            </span>
          </button>
        ))}
      </div>
    </div>
  )
}
