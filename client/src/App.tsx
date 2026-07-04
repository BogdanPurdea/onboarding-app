import { useState, useEffect } from 'react'
import { AppLayout } from './components/AppLayout'
import { DepartmentSelector } from './components/DepartmentSelector'
import { OnboardingChecklist } from './components/OnboardingChecklist'
import { DEPARTMENTS } from './config/departments'
import type { DepartmentConfig } from './types/index'

function App() {
  const [selectedRole, setSelectedRole] = useState<string | null>(null)

  // Initialize selectedRole from query param on mount
  useEffect(() => {
    const params = new URLSearchParams(window.location.search)
    const roleParam = params.get('role')?.toLowerCase()
    
    if (roleParam && DEPARTMENTS.some(d => d.roleKey === roleParam)) {
      setSelectedRole(roleParam)
    } else {
      setSelectedRole(null)
    }
  }, [])

  // Listen to popstate to sync state with browser back/forward buttons
  useEffect(() => {
    const handlePopState = () => {
      const params = new URLSearchParams(window.location.search)
      const roleParam = params.get('role')?.toLowerCase()
      if (roleParam && DEPARTMENTS.some(d => d.roleKey === roleParam)) {
        setSelectedRole(roleParam)
      } else {
        setSelectedRole(null)
      }
    }

    window.addEventListener('popstate', handlePopState)
    return () => window.removeEventListener('popstate', handlePopState)
  }, [])

  const handleSelectRole = (roleKey: string) => {
    setSelectedRole(roleKey)
    const newUrl = roleKey ? `?role=${roleKey}` : window.location.pathname
    window.history.pushState({ role: roleKey }, '', newUrl)
  }

  const handleClearRole = () => {
    setSelectedRole(null)
    window.history.pushState({ role: null }, '', window.location.pathname)
  }

  // Look up resolved configuration
  const currentDept: DepartmentConfig | undefined = DEPARTMENTS.find(
    d => d.roleKey === selectedRole
  )

  return (
    <AppLayout>
      <div className="flex items-center justify-between pb-4 mb-6 border-b border-slate-200">
        <h1 className="text-xl font-bold tracking-tight text-slate-900">
          Meridian Navigator
        </h1>
        {selectedRole && (
          <button
            onClick={handleClearRole}
            className="text-sm font-medium text-indigo-600 hover:text-indigo-500 transition-colors"
          >
            Change Team
          </button>
        )}
      </div>

      {selectedRole ? (
        <div className="py-2">
          <div className="mb-4">
            <span className="inline-flex items-center rounded-md bg-indigo-50 px-2.5 py-1 text-xs font-medium text-indigo-700 ring-1 ring-inset ring-indigo-700/10">
              {currentDept?.name} Onboarding Flow
            </span>
          </div>
          <p className="text-sm text-slate-500 leading-relaxed">
            {currentDept?.welcomeMessage}
          </p>
          <OnboardingChecklist role={selectedRole} />
        </div>
      ) : (
        <DepartmentSelector onSelectRole={handleSelectRole} />
      )}
    </AppLayout>
  )
}

export default App
