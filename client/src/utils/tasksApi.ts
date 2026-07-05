import type { TaskDto, TaskInstructionsDto } from '../types/index'

const API_BASE = import.meta.env.VITE_API_URL ?? ''

/**
 * Fetches tasks for a specific department role key.
 */
export async function fetchTasks(role: string, signal?: AbortSignal): Promise<TaskDto[]> {
  const res = await fetch(`${API_BASE}/api/tasks?role=${encodeURIComponent(role)}`, { signal })
  if (!res.ok) {
    throw new Error(`Failed to fetch tasks for role ${role}`)
  }
  return res.json() as Promise<TaskDto[]>
}

/**
 * Fetches the ordered detailed instructions for a specific onboarding task.
 */
export async function fetchTaskInstructions(taskId: number, signal?: AbortSignal): Promise<TaskInstructionsDto> {
  const res = await fetch(`${API_BASE}/api/tasks/${taskId}/instructions`, { signal })
  if (!res.ok) {
    throw new Error(`Failed to load task instructions (${res.status})`)
  }
  return res.json() as Promise<TaskInstructionsDto>
}
