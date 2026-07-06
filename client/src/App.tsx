import { useState, useEffect, useCallback } from 'react'
import { AppLayout } from './components/layout/AppLayout'
import { DepartmentSelector } from './components/department/DepartmentSelector'
import { OnboardingChecklist } from './components/checklist/OnboardingChecklist'
import { OnboardingDashboard } from './components/dashboard/OnboardingDashboard'
import { ThemeToggle } from './components/layout/ThemeToggle'
import { fetchDepartments } from './utils/departmentsApi'
import type { DepartmentConfig } from './types/index'
import { AskMeridianChat } from './components/chat/AskMeridianChat'


type ActiveTab = 'checklist' | 'dashboard'

function App() {
  const [selectedRole, setSelectedRole] = useState<string | null>(null)
  const [activeTab, setActiveTab] = useState<ActiveTab>('dashboard')
  const [departments, setDepartments] = useState<DepartmentConfig[]>([])

  // Fetch department list from the API
  useEffect(() => {
    const controller = new AbortController()

    fetchDepartments(controller.signal)
      .then(setDepartments)
      .catch(err => {
        if (err.name !== 'AbortError') console.error('Department fetch error:', err)
      })

    return () => controller.abort()
  }, [])

  const resolveRoleFromParams = useCallback(
    (depts: DepartmentConfig[]) => {
      const params = new URLSearchParams(window.location.search)
      const roleParam = params.get('role')?.toLowerCase()
      if (roleParam && depts.some(d => d.roleKey === roleParam)) {
        setSelectedRole(roleParam)
      } else {
        setSelectedRole(null)
      }
    },
    []
  )

  // Initialize selectedRole from query param once departments are loaded
  useEffect(() => {
    if (departments.length > 0) {
      resolveRoleFromParams(departments)
    }
  }, [departments, resolveRoleFromParams])

  // Listen to popstate to sync state with browser back/forward buttons
  useEffect(() => {
    const handlePopState = () => resolveRoleFromParams(departments)
    window.addEventListener('popstate', handlePopState)
    return () => window.removeEventListener('popstate', handlePopState)
  }, [departments, resolveRoleFromParams])

  const handleSelectRole = (roleKey: string) => {
    setSelectedRole(roleKey)
    setActiveTab('dashboard')
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

  const currentDept = departments.find(d => d.roleKey === selectedRole)

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
          {/* Sub-tab navigation */}
          <div className="flex gap-1 mb-6 border-b border-slate-200 dark:border-slate-800">
            <button
              id="tab-dashboard"
              onClick={() => setActiveTab('dashboard')}
              className={[
                'px-4 py-2 text-sm font-medium rounded-t-md transition-colors',
                activeTab === 'dashboard'
                  ? 'text-indigo-600 border-b-2 border-indigo-600 dark:text-orange-400 dark:border-orange-400'
                  : 'text-slate-500 hover:text-slate-700 dark:text-slate-400 dark:hover:text-slate-200'
              ].join(' ')}
            >
              Team Dashboard
            </button>
            <button
              id="tab-checklist"
              onClick={() => setActiveTab('checklist')}
              className={[
                'px-4 py-2 text-sm font-medium rounded-t-md transition-colors',
                activeTab === 'checklist'
                  ? 'text-indigo-600 border-b-2 border-indigo-600 dark:text-orange-400 dark:border-orange-400'
                  : 'text-slate-500 hover:text-slate-700 dark:text-slate-400 dark:hover:text-slate-200'
              ].join(' ')}
            >
              Onboarding Checklist
            </button>
          </div>

          {activeTab === 'checklist' ? (
            <>
              <div className="mb-4">
                <span className="inline-flex items-center rounded-md bg-indigo-50 px-2.5 py-1 text-xs font-medium text-indigo-700 ring-1 ring-inset ring-indigo-700/10 dark:bg-orange-950/20 dark:text-orange-300 dark:ring-orange-400/20">
                  {currentDept?.name} Onboarding Flow
                </span>
              </div>
              <OnboardingChecklist role={selectedRole} />
            </>
          ) : (
            <OnboardingDashboard roleKey={selectedRole} />
          )}
        </div>
      ) : (
        <DepartmentSelector onSelectRole={handleSelectRole} departments={departments} />
      )}
      <AskMeridianChat role={selectedRole} />
    </AppLayout>
  )
}

export default App
