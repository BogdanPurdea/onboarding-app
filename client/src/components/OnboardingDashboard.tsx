import { useState, useEffect } from 'react'
import type { DashboardDto, DayScheduleDto, ContactDto } from '../types/index'
import type { OnboardingDashboardProps } from '../types/components'

const API_BASE = import.meta.env.VITE_API_URL ?? ''

// ─── Schedule Strip ────────────────────────────────────────────────────────

function DayPill({ day, type }: DayScheduleDto) {
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

// ─── Contact Card ──────────────────────────────────────────────────────────

function ContactCard({ name, role, avatarInitials, email, bio }: ContactDto) {
  return (
    <div className="card-base p-4 flex gap-4 items-start">
      <div className="w-10 h-10 rounded-full flex items-center justify-center text-sm font-bold shrink-0 bg-indigo-100 text-indigo-700 dark:bg-orange-900/30 dark:text-orange-300">
        {avatarInitials}
      </div>
      <div className="min-w-0">
        <p className="text-sm font-semibold text-slate-900 dark:text-slate-100 truncate">{name}</p>
        <p className="text-xs text-slate-500 dark:text-slate-400 truncate">{role}</p>
        <p className="mt-1 text-xs text-slate-500 dark:text-slate-400 leading-relaxed line-clamp-2">{bio}</p>
        <a
          href={`mailto:${email}`}
          className="mt-1.5 inline-block text-xs text-indigo-600 hover:underline dark:text-orange-400"
        >
          {email}
        </a>
      </div>
    </div>
  )
}

// ─── Main Component ────────────────────────────────────────────────────────

export function OnboardingDashboard({ roleKey }: OnboardingDashboardProps) {
  const [dashboard, setDashboard] = useState<DashboardDto | null>(null)
  const [loading, setLoading] = useState(true)
  const [error, setError] = useState<string | null>(null)

  useEffect(() => {
    const controller = new AbortController()
    setLoading(true)
    setError(null)

    fetch(`${API_BASE}/api/departments/${encodeURIComponent(roleKey)}/dashboard`, {
      signal: controller.signal,
    })
      .then(res => {
        if (!res.ok) throw new Error(`Failed to load dashboard (${res.status})`)
        return res.json() as Promise<DashboardDto>
      })
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
