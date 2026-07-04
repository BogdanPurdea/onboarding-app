import { DEPARTMENTS } from '../config/departments'
import type { DepartmentConfig } from '../types/index'
import type { DepartmentSelectorProps } from '../types/components'
import { DepartmentCard } from './DepartmentCard'

export function DepartmentSelector({ onSelectRole }: DepartmentSelectorProps) {
  return (
    <div className="py-6">
      <div className="text-center mb-8">
        <h2 className="text-3xl font-bold tracking-tight text-slate-900 dark:text-slate-100">
          Welcome to Meridian
        </h2>
        <p className="mt-2 text-base text-slate-500 dark:text-slate-400">
          Select your team department to begin your first month onboarding checklist.
        </p>
      </div>

      <div className="grid gap-4 sm:grid-cols-2 lg:grid-cols-1">
        {DEPARTMENTS.map((dept: DepartmentConfig) => (
          <DepartmentCard
            key={dept.id}
            dept={dept}
            onSelectRole={onSelectRole}
          />
        ))}
      </div>
    </div>
  )
}
