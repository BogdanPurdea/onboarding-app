import { describe, it, expect } from 'vitest'
import { isTaskUnlocked, computeCascadeUnchecks } from './checklistLogic'
import type { TaskDto } from '../types'

describe('checklistLogic', () => {
  describe('isTaskUnlocked', () => {
    it('should return true if task has no prerequisites', () => {
      const task: TaskDto = {
        id: 1,
        title: 'Task A',
        description: '',
        timelinePhase: 1,
        displayOrder: 1,
        departmentId: 1,
        departmentName: 'Engineering',
        prerequisiteTaskIds: []
      }
      const completedSet = new Set<number>()
      expect(isTaskUnlocked(task, completedSet)).toBe(true)
    })

    it('should return false if any prerequisite task is not completed', () => {
      const task: TaskDto = {
        id: 3,
        title: 'Task C',
        description: '',
        timelinePhase: 1,
        displayOrder: 3,
        departmentId: 1,
        departmentName: 'Engineering',
        prerequisiteTaskIds: [1, 2]
      }
      const completedSet = new Set<number>([1]) // 2 is missing
      expect(isTaskUnlocked(task, completedSet)).toBe(false)
    })

    it('should return true if all prerequisite tasks are completed', () => {
      const task: TaskDto = {
        id: 3,
        title: 'Task C',
        description: '',
        timelinePhase: 1,
        displayOrder: 3,
        departmentId: 1,
        departmentName: 'Engineering',
        prerequisiteTaskIds: [1, 2]
      }
      const completedSet = new Set<number>([1, 2])
      expect(isTaskUnlocked(task, completedSet)).toBe(true)
    })
  })

  describe('computeCascadeUnchecks', () => {
    const tasks: TaskDto[] = [
      {
        id: 1,
        title: 'Task 1',
        description: '',
        timelinePhase: 1,
        displayOrder: 1,
        departmentId: 1,
        departmentName: 'Engineering',
        prerequisiteTaskIds: []
      },
      {
        id: 2,
        title: 'Task 2',
        description: '',
        timelinePhase: 1,
        displayOrder: 2,
        departmentId: 1,
        departmentName: 'Engineering',
        prerequisiteTaskIds: [1]
      },
      {
        id: 3,
        title: 'Task 3',
        description: '',
        timelinePhase: 1,
        displayOrder: 3,
        departmentId: 1,
        departmentName: 'Engineering',
        prerequisiteTaskIds: [2]
      }
    ]

    it('should remove unchecked task and cascade uncheck to dependent tasks', () => {
      const currentCompleted = [1, 2, 3]
      // Unchecking task 1 should cascade-uncheck 2 and 3
      const result = computeCascadeUnchecks(1, currentCompleted, tasks)
      expect(result).toEqual([])
    })

    it('should remove unchecked task and dependent tasks but keep unrelated tasks', () => {
      const currentCompleted = [1, 2, 3]
      // Unchecking task 2 should cascade-uncheck 3 but keep 1
      const result = computeCascadeUnchecks(2, currentCompleted, tasks)
      expect(result).toEqual([1])
    })

    it('should simply remove the unchecked task if there are no dependent tasks', () => {
      const currentCompleted = [1, 2, 3]
      // Unchecking task 3 should only remove 3
      const result = computeCascadeUnchecks(3, currentCompleted, tasks)
      expect(result.sort()).toEqual([1, 2])
    })
  })
})
