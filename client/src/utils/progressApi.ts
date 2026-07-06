import type { ProgressSyncRequest } from '../types/index'

const API_BASE = import.meta.env.VITE_API_URL ?? ''

/**
 * Fetches the array of completed task IDs for a given anonymous session token.
 */
export async function fetchProgress(token: string, signal?: AbortSignal): Promise<number[]> {
  const res = await fetch(`${API_BASE}/api/progress/${token}`, { signal })
  if (!res.ok) {
    throw new Error(`Failed to fetch progress (${res.status})`)
  }
  return res.json() as Promise<number[]>
}

/**
 * Overwrites the completed task IDs for a given session token on the server.
 * This is a full replace — the backend deletes existing records and inserts the new array.
 */
export async function syncProgress(request: ProgressSyncRequest, signal?: AbortSignal): Promise<void> {
  await fetch(`${API_BASE}/api/progress/sync`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(request),
    signal,
  })
}
