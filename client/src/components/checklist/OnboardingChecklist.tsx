import { useState, useEffect, useMemo } from 'react'
import { TimelinePhase, type TaskDto, type TaskInstructionsDto } from '../../types/index'
import type { OnboardingChecklistProps } from '../../types/components'
import { isTaskUnlocked, computeCascadeUnchecks } from '../../utils/checklistLogic'
import { OnboardingProgressHeader } from './OnboardingProgressHeader'
import { OnboardingPhaseTabs } from './OnboardingPhaseTabs'
import { OnboardingTaskItem } from './OnboardingTaskItem'
import { TaskInstructionsDrawer } from './TaskInstructionsDrawer'
import { fetchTaskInstructions, fetchTasks } from '../../utils/tasksApi'

export function OnboardingChecklist({ role }: OnboardingChecklistProps) {
  const [tasks, setTasks] = useState<TaskDto[]>([])
  const [completedIds, setCompletedIds] = useState<number[]>([])
  const [activePhase, setActivePhase] = useState<TimelinePhase>(TimelinePhase.WeekOne)
  const [isLoading, setIsLoading] = useState<boolean>(true)
  const [error, setError] = useState<string | null>(null)
  const [loadingInstructionsTaskId, setLoadingInstructionsTaskId] = useState<number | null>(null)
  const [drawerInstructions, setDrawerInstructions] = useState<TaskInstructionsDto | null>(null)

  const localStorageKey = `meridian_completed_tasks_${role}`

  // Fetch tasks when department role changes (handling in-flight aborts)
  useEffect(() => {
    const controller = new AbortController()
    setIsLoading(true)
    setError(null)
    setDrawerInstructions(null)
    setLoadingInstructionsTaskId(null)
    
    fetchTasks(role, controller.signal)
      .then((data: TaskDto[]) => {
        setTasks(data)
        setIsLoading(false)
      })
      .catch(err => {
        if (err.name !== 'AbortError') {
          setError(err.message || 'An error occurred while loading your tasks.')
          setIsLoading(false)
        }
      })

    return () => {
      controller.abort()
    }
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

  const handleCardClick = (taskId: number) => {
    if (loadingInstructionsTaskId !== null) return

    setLoadingInstructionsTaskId(taskId)
    fetchTaskInstructions(taskId)
      .then(data => {
        setDrawerInstructions(data)
        setLoadingInstructionsTaskId(null)
      })
      .catch(err => {
        console.error(err)
        setLoadingInstructionsTaskId(null)
      })
  }

  // Group tasks by timeline phase enum keys
  const phaseGroupedTasks = useMemo(() => {
    const groups: { [key in TimelinePhase]?: TaskDto[] } = {
      [TimelinePhase.WeekOne]: [],
      [TimelinePhase.WeekTwo]: [],
      [TimelinePhase.WeekThree]: [],
      [TimelinePhase.WeekFour]: []
    }
    tasks.forEach(t => {
      if (groups[t.timelinePhase] !== undefined) {
        groups[t.timelinePhase]?.push(t)
      }
    })
    return groups
  }, [tasks])

  // Compute total progress
  const totalTasksCount = tasks.length
  const totalCompletedCount = completedIds.length
  const progressPercent = totalTasksCount > 0 ? Math.round((totalCompletedCount / totalTasksCount) * 100) : 0

  // Phase-specific statistics mapped by TimelinePhase enum keys
  const phaseStats = useMemo(() => {
    const stats: { [key in TimelinePhase]?: { total: number; completed: number } } = {
      [TimelinePhase.WeekOne]: { total: 0, completed: 0 },
      [TimelinePhase.WeekTwo]: { total: 0, completed: 0 },
      [TimelinePhase.WeekThree]: { total: 0, completed: 0 },
      [TimelinePhase.WeekFour]: { total: 0, completed: 0 }
    }
    tasks.forEach(t => {
      const stat = stats[t.timelinePhase]
      if (stat) {
        stat.total += 1
        if (completedSet.has(t.id)) {
          stat.completed += 1
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
                onClick={() => handleCardClick(task.id)}
                isLoadingInstructions={loadingInstructionsTaskId === task.id}
              />
            )
          })
        )}
      </div>

      {/* Slide up detailed instructions panel */}
      {drawerInstructions && (
        <TaskInstructionsDrawer
          instructions={drawerInstructions}
          onClose={() => setDrawerInstructions(null)}
        />
      )}
    </div>
  )
}
