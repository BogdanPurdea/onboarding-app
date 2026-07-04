import { useState, useEffect } from 'react'
import { AppLayout } from './components/AppLayout'
import { DepartmentSelector } from './components/DepartmentSelector'
import { OnboardingChecklist } from './components/OnboardingChecklist'
import { ThemeToggle } from './components/ThemeToggle'
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
    const params = new URLSearchParams(window.location.search)
    if (roleKey) {
      params.set('role', roleKey)
    } else {
      params.delete('role')
    }
    const searchString = params.toString()
    const newUrl = searchString ? `?${searchString}` : window.location.pathname
    window.history.pushState({ role: roleKey }, '', newUrl)
  }

  const handleClearRole = () => {
    setSelectedRole(null)
    const params = new URLSearchParams(window.location.search)
    params.delete('role')
    const searchString = params.toString()
    const newUrl = searchString ? `?${searchString}` : window.location.pathname
    window.history.pushState({ role: null }, '', newUrl)
  }

  // Look up resolved configuration
  const currentDept: DepartmentConfig | undefined = DEPARTMENTS.find(
    d => d.roleKey === selectedRole
  )

  return (
    <AppLayout>
      <div className="flex items-center justify-between pb-4 mb-6 border-b border-slate-200 dark:border-slate-800">
        <h1 className="text-xl font-bold tracking-tight text-slate-900 dark:text-slate-100">
          Meridian Navigator
        </h1>
        <div className="flex items-center gap-3">
          {selectedRole && (
            <button
              onClick={handleClearRole}
              className="text-sm font-medium text-indigo-600 hover:text-indigo-500 transition-colors dark:text-orange-300 dark:hover:text-orange-400 cursor-pointer"
            >
              Change Role
            </button>
          )}
          <ThemeToggle />
        </div>
      </div>

      {selectedRole ? (
        <div className="py-2">
          <div className="mb-4">
            <span className="inline-flex items-center rounded-md bg-indigo-50 px-2.5 py-1 text-xs font-medium text-indigo-700 ring-1 ring-inset ring-indigo-700/10 dark:bg-orange-950/20 dark:text-orange-300 dark:ring-orange-400/20">
              {currentDept?.name} Onboarding Flow
            </span>
          </div>
          <p className="text-sm text-slate-500 leading-relaxed dark:text-slate-400">
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
