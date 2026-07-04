import { useState, useEffect, useMemo } from 'react'
import type { TaskDto } from '../types'

interface OnboardingChecklistProps {
  role: string
}

export function OnboardingChecklist({ role }: OnboardingChecklistProps) {
  const [tasks, setTasks] = useState<TaskDto[]>([])
  const [completedIds, setCompletedIds] = useState<number[]>([])
  const [activePhase, setActivePhase] = useState<number>(0) // 0 = DayOne, 1 = WeekOne, 2 = MonthOne
  const [isLoading, setIsLoading] = useState<boolean>(true)
  const [error, setError] = useState<string | null>(null)

  const localStorageKey = `meridian_completed_tasks_${role}`

  // Fetch tasks when department role changes
  useEffect(() => {
    setIsLoading(true)
    setError(null)
    
    fetch(`/api/tasks?role=${role}`)
      .then(res => {
        if (!res.ok) {
          throw new Error(`Failed to fetch tasks for role ${role}`)
        }
        return res.json()
      })
      .then((data: TaskDto[]) => {
        setTasks(data)
        setIsLoading(false)
      })
      .catch(err => {
        setError(err.message || 'An error occurred while loading your tasks.')
        setIsLoading(false)
      })
  }, [role])

  // Load completed task IDs from localStorage on mount/role change
  useEffect(() => {
    try {
      const saved = localStorage.getItem(localStorageKey)
      if (saved) {
        setCompletedIds(JSON.parse(saved))
      } else {
        setCompletedIds([])
      }
    } catch {
      setCompletedIds([])
    }
  }, [role, localStorageKey])

  // Save completed task IDs to localStorage
  const updateCompletedIds = (newIds: number[]) => {
    setCompletedIds(newIds)
    try {
      localStorage.setItem(localStorageKey, JSON.stringify(newIds))
    } catch (e) {
      console.error('Failed to save progress to localStorage:', e)
    }
  }

  // Set of completed task IDs for O(1) lookups
  const completedSet = useMemo(() => new Set(completedIds), [completedIds])

  // Map to associate task IDs to their titles for showing prerequisite details
  const taskTitleMap = useMemo(() => {
    const map = new Map<number, string>()
    tasks.forEach(t => map.set(t.id, t.title))
    return map
  }, [tasks])

  // Helper to determine if a task's prerequisites are met
  const isTaskUnlocked = (task: TaskDto): boolean => {
    if (!task.prerequisiteTaskIds || task.prerequisiteTaskIds.length === 0) {
      return true
    }
    return task.prerequisiteTaskIds.every(preId => completedSet.has(preId))
  }

  // Recursive unchecker for dependent tasks
  const computeCascadeUnchecks = (uncheckedId: number, currentCompleted: number[]): number[] => {
    const activeCompleted = new Set(currentCompleted)
    activeCompleted.delete(uncheckedId)

    let changed = true
    while (changed) {
      changed = false
      // Find any task that is completed but whose prerequisites are no longer satisfied
      for (const task of tasks) {
        if (activeCompleted.has(task.id)) {
          const hasUnsatisfiedPre = task.prerequisiteTaskIds.some(preId => !activeCompleted.has(preId))
          if (hasUnsatisfiedPre) {
            activeCompleted.delete(task.id)
            changed = true
          }
        }
      }
    }

    return Array.from(activeCompleted)
  }

  const handleToggleTask = (task: TaskDto) => {
    const isCompleted = completedSet.has(task.id)

    if (isCompleted) {
      // Unchecking: compute cascade changes
      const remainingIds = computeCascadeUnchecks(task.id, completedIds)
      updateCompletedIds(remainingIds)
    } else {
      // Checking: only allow if unlocked
      if (isTaskUnlocked(task)) {
        updateCompletedIds([...completedIds, task.id])
      }
    }
  }

  // Group tasks by timeline phase
  const phaseGroupedTasks = useMemo(() => {
    const groups: { [key: number]: TaskDto[] } = { 0: [], 1: [], 2: [] }
    tasks.forEach(t => {
      if (groups[t.timelinePhase] !== undefined) {
        groups[t.timelinePhase].push(t)
      }
    })
    return groups
  }, [tasks])

  // Compute total progress
  const totalTasksCount = tasks.length
  const totalCompletedCount = completedIds.length
  const progressPercent = totalTasksCount > 0 ? Math.round((totalCompletedCount / totalTasksCount) * 100) : 0

  // Phase-specific statistics
  const phaseStats = useMemo(() => {
    const stats: { [key: number]: { total: number; completed: number } } = {
      0: { total: 0, completed: 0 },
      1: { total: 0, completed: 0 },
      2: { total: 0, completed: 0 }
    }
    tasks.forEach(t => {
      stats[t.timelinePhase].total += 1
      if (completedSet.has(t.id)) {
        stats[t.timelinePhase].completed += 1
      }
    })
    return stats
  }, [tasks, completedSet])

  if (isLoading) {
    return (
      <div className="py-12 text-center text-slate-500 font-medium animate-pulse">
        Loading onboarding checklist...
      </div>
    )
  }

  if (error) {
    return (
      <div className="py-6">
        <div className="p-4 border border-rose-200 rounded-xl bg-rose-50 text-rose-800 text-sm">
          {error}
        </div>
      </div>
    )
  }

  const activePhaseTasks = phaseGroupedTasks[activePhase] || []

  return (
    <div className="space-y-6 py-4">
      {/* Progress Header Card */}
      <div className="p-6 border border-slate-200 rounded-xl bg-white shadow-sm">
        <div className="flex items-center justify-between">
          <span className="text-sm font-semibold text-slate-500">
            Overall Completion
          </span>
          <span className="text-sm font-bold text-indigo-600">
            {totalCompletedCount} / {totalTasksCount} tasks ({progressPercent}%)
          </span>
        </div>
        <div className="mt-3 w-full bg-slate-100 rounded-full h-2">
          <div
            className="bg-indigo-600 h-2 rounded-full transition-all duration-300 ease-out"
            style={{ width: `${progressPercent}%` }}
          />
        </div>
      </div>

      {/* Phase Tabs Selector */}
      <div className="flex space-x-1 border-b border-slate-200">
        {[
          { key: 0, label: 'Day 1' },
          { key: 1, label: 'Week 1' },
          { key: 2, label: 'Month 1' }
        ].map(phase => {
          const stats = phaseStats[phase.key]
          const isCurrent = activePhase === phase.key
          return (
            <button
              key={phase.key}
              onClick={() => setActivePhase(phase.key)}
              className={`flex-1 pb-3 text-sm font-semibold transition-colors duration-200 border-b-2 focus:outline-none ${
                isCurrent
                  ? 'border-indigo-600 text-indigo-600'
                  : 'border-transparent text-slate-500 hover:text-slate-800'
              }`}
            >
              <span>{phase.label}</span>
              <span className={`ml-2 text-xs px-2 py-0.5 rounded-full ${
                isCurrent ? 'bg-indigo-100 text-indigo-700' : 'bg-slate-100 text-slate-600'
              }`}>
                {stats.completed}/{stats.total}
              </span>
            </button>
          )
        })}
      </div>

      {/* Task List */}
      <div className="space-y-4">
        {activePhaseTasks.length === 0 ? (
          <div className="p-8 text-center text-slate-400 text-sm border border-dashed border-slate-200 rounded-xl bg-slate-50">
            No tasks found for this phase.
          </div>
        ) : (
          activePhaseTasks.map(task => {
            const isCompleted = completedSet.has(task.id)
            const isUnlocked = isTaskUnlocked(task)

            return (
              <div
                key={task.id}
                className={`p-5 border rounded-xl bg-white shadow-sm transition duration-200 flex items-start gap-4 ${
                  !isUnlocked
                    ? 'border-slate-100 bg-slate-50/50 opacity-60'
                    : isCompleted
                      ? 'border-emerald-200 bg-emerald-50/10'
                      : 'border-slate-200 hover:border-slate-300'
                }`}
              >
                {/* Checkbox wrapper */}
                <div className="flex items-center h-6">
                  <input
                    type="checkbox"
                    id={`task-${task.id}`}
                    checked={isCompleted}
                    disabled={!isUnlocked}
                    onChange={() => handleToggleTask(task)}
                    className={`h-5.5 w-5.5 rounded border-slate-300 text-indigo-600 focus:ring-indigo-500 focus:ring-offset-2 transition-colors duration-150 ${
                      !isUnlocked
                        ? 'cursor-not-allowed text-slate-300 bg-slate-100'
                        : 'cursor-pointer'
                    }`}
                  />
                </div>

                {/* Task Content */}
                <div className="flex-1 min-w-0">
                  <label
                    htmlFor={`task-${task.id}`}
                    className={`block text-base font-semibold leading-6 select-none ${
                      !isUnlocked
                        ? 'text-slate-400 cursor-not-allowed'
                        : isCompleted
                          ? 'text-slate-500 line-through'
                          : 'text-slate-900 cursor-pointer'
                    }`}
                  >
                    {task.title}
                  </label>
                  <p className={`mt-1 text-sm leading-relaxed ${
                    isCompleted ? 'text-slate-400' : 'text-slate-600'
                  }`}>
                    {task.description}
                  </p>

                  {/* Prerequisites Locks Warning */}
                  {!isUnlocked && task.prerequisiteTaskIds && (
                    <div className="mt-3 flex items-center gap-1.5 text-xs font-medium text-amber-700 bg-amber-50 border border-amber-100 rounded-md px-2.5 py-1.5 w-fit">
                      <span>
                        Requires:{' '}
                        {task.prerequisiteTaskIds
                          .map(preId => taskTitleMap.get(preId) || `Task #${preId}`)
                          .join(', ')}
                      </span>
                    </div>
                  )}
                </div>
              </div>
            )
          })
        )}
      </div>
    </div>
  )
}
