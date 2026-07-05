import { useState, useEffect } from 'react'
import type { DashboardDto } from '../types/index'
import type { OnboardingDashboardProps } from '../types/components'
import { DayPill } from './DayPill'
import { ContactCard } from './ContactCard'
import { fetchDashboardData } from '../utils/dashboardApi'

export function OnboardingDashboard({ roleKey }: OnboardingDashboardProps) {
  const [dashboard, setDashboard] = useState<DashboardDto | null>(null)
  const [loading, setLoading] = useState(true)
  const [error, setError] = useState<string | null>(null)

  useEffect(() => {
    const controller = new AbortController()
    setLoading(true)
    setError(null)

    fetchDashboardData(roleKey, controller.signal)
      .then(data => {
        setDashboard(data)
        setLoading(false)
      })
      .catch(err => {
        if (err.name !== 'AbortError') {
          setError(err.message)
          setLoading(false)
        }
      })

    return () => controller.abort()
  }, [roleKey])

  if (loading) {
    return (
      <div className="py-12 text-center text-slate-400 dark:text-slate-500 text-sm">
        Loading team dashboard…
      </div>
    )
  }

  if (error || !dashboard) {
    return (
      <div className="py-12 text-center text-red-500 text-sm">
        {error ?? 'Dashboard unavailable.'}
      </div>
    )
  }

  return (
    <div className="space-y-8">
      {/* Welcome banner */}
      <div className="card-base p-5">
        <p className="text-xs font-medium uppercase tracking-wider text-indigo-600 dark:text-orange-400 mb-1">
          {dashboard.departmentName} · Welcome
        </p>
        <p className="text-sm text-slate-600 dark:text-slate-300 leading-relaxed">
          {dashboard.welcomeMessage}
        </p>
      </div>

      {/* Weekly schedule strip */}
      <section>
        <h2 className="text-sm font-semibold text-slate-700 dark:text-slate-300 mb-3">
          Weekly Hybrid Schedule
        </h2>
        <div className="card-base p-4">
          <div className="flex gap-2">
            {dashboard.weeklySchedule.map(day => (
              <DayPill key={day.day} day={day.day} type={day.type} />
            ))}
          </div>
          <p className="mt-3 text-xs text-slate-400 dark:text-slate-500">
            3 office days · 2 remote days per week
          </p>
        </div>
      </section>

      {/* Who's Who directory */}
      <section>
        <h2 className="text-sm font-semibold text-slate-700 dark:text-slate-300 mb-3">
          Who's Who
        </h2>
        <div className="grid gap-3 sm:grid-cols-2 lg:grid-cols-3">
          {dashboard.contacts.map(contact => (
            <ContactCard key={contact.email} {...contact} />
          ))}
        </div>
      </section>
    </div>
  )
}
