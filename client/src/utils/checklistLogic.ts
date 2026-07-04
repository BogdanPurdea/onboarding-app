import type { TaskDto } from '../types'

/**
 * Determines if a task is unlocked based on completed prerequisite IDs.
 */
export const isTaskUnlocked = (task: TaskDto, completedSet: Set<number>): boolean => {
  if (!task.prerequisiteTaskIds || task.prerequisiteTaskIds.length === 0) {
    return true
  }
  return task.prerequisiteTaskIds.every(preId => completedSet.has(preId))
}

/**
 * Computes list of task IDs that should remain completed after unchecking a task,
 * recursively cascading unchecks to any dependent tasks whose prerequisites are no longer met.
 */
export const computeCascadeUnchecks = (
  uncheckedId: number,
  currentCompleted: number[],
  tasks: TaskDto[]
): number[] => {
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
