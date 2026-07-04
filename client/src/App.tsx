import { useState, useEffect } from 'react'
import { AppLayout } from './components/AppLayout'
import { DepartmentSelector } from './components/DepartmentSelector'
import { DEPARTMENTS, type DepartmentConfig } from './config/departments'

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

      {currentDept ? (
        <div className="py-6">
          <div className="p-6 border border-slate-200 rounded-xl bg-white shadow-sm">
            <span className="inline-flex items-center rounded-md bg-indigo-50 px-2 py-1 text-xs font-medium text-indigo-700 ring-1 ring-inset ring-indigo-700/10">
              {currentDept.name} Flow
            </span>
            <h2 className="mt-4 text-2xl font-bold tracking-tight text-slate-950">
              Onboarding Checklist
            </h2>
            <p className="mt-4 text-slate-600 leading-relaxed">
              {currentDept.welcomeMessage}
            </p>
            <div className="mt-8 border-t border-slate-100 pt-6">
              <span className="text-sm text-slate-400">
                Tasks view is coming soon...
              </span>
            </div>
          </div>
        </div>
      ) : (
        <DepartmentSelector onSelectRole={handleSelectRole} />
      )}
    </AppLayout>
  )
}

export default App
