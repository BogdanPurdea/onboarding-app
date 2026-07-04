import { useState, useEffect, useMemo } from 'react'
import type { TaskDto } from '../types'
import type { OnboardingChecklistProps } from '../types/components'
import { isTaskUnlocked, computeCascadeUnchecks } from '../utils/checklistLogic'
import { OnboardingProgressHeader } from './OnboardingProgressHeader'
import { OnboardingPhaseTabs } from './OnboardingPhaseTabs'
import { OnboardingTaskItem } from './OnboardingTaskItem'

export function OnboardingChecklist({ role }: OnboardingChecklistProps) {
  const [tasks, setTasks] = useState<TaskDto[]>([])
  const [completedIds, setCompletedIds] = useState<number[]>([])
  const [activePhase, setActivePhase] = useState<number>(1) // 1 = WeekOne, 2 = WeekTwo, 3 = WeekThree, 4 = WeekFour
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

  const handleToggleTask = (task: TaskDto) => {
    const isCompleted = completedSet.has(task.id)

    if (isCompleted) {
      // Unchecking: compute cascade changes
      const remainingIds = computeCascadeUnchecks(task.id, completedIds, tasks)
      updateCompletedIds(remainingIds)
    } else {
      // Checking: only allow if unlocked
      if (isTaskUnlocked(task, completedSet)) {
        updateCompletedIds([...completedIds, task.id])
      }
    }
  }

  // Group tasks by timeline phase
  const phaseGroupedTasks = useMemo(() => {
    const groups: { [key: number]: TaskDto[] } = { 1: [], 2: [], 3: [], 4: [] }
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
      1: { total: 0, completed: 0 },
      2: { total: 0, completed: 0 },
      3: { total: 0, completed: 0 },
      4: { total: 0, completed: 0 }
    }
    tasks.forEach(t => {
      if (stats[t.timelinePhase]) {
        stats[t.timelinePhase].total += 1
        if (completedSet.has(t.id)) {
          stats[t.timelinePhase].completed += 1
        }
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
      {/* Progress Header */}
      <OnboardingProgressHeader
        completedCount={totalCompletedCount}
        totalCount={totalTasksCount}
        progressPercent={progressPercent}
      />

      {/* Phase Tabs */}
      <OnboardingPhaseTabs
        activePhase={activePhase}
        onChangePhase={setActivePhase}
        phaseStats={phaseStats}
      />

      {/* Task List */}
      <div className="space-y-4">
        {activePhaseTasks.length === 0 ? (
          <div className="p-8 text-center text-slate-400 text-sm border border-dashed border-slate-200 rounded-xl bg-slate-50">
            No tasks found for this phase.
          </div>
        ) : (
          activePhaseTasks.map(task => {
            const isCompleted = completedSet.has(task.id)
            const isUnlocked = isTaskUnlocked(task, completedSet)
            const prerequisiteTitles = task.prerequisiteTaskIds
              .map(preId => taskTitleMap.get(preId) || `Task #${preId}`)

            return (
              <OnboardingTaskItem
                key={task.id}
                task={task}
                isCompleted={isCompleted}
                isUnlocked={isUnlocked}
                onToggle={() => handleToggleTask(task)}
                prerequisiteTitles={prerequisiteTitles}
              />
            )
          })
        )}
      </div>
    </div>
  )
}
