import type { DashboardDto } from '../types/index'

const API_BASE = import.meta.env.VITE_API_URL ?? ''

/**
 * Fetches dashboard details for a specific department role key.
 */
export async function fetchDashboardData(roleKey: string, signal?: AbortSignal): Promise<DashboardDto> {
  const res = await fetch(`${API_BASE}/api/departments/${encodeURIComponent(roleKey)}/dashboard`, {
    signal,
  })
  if (!res.ok) {
    throw new Error(`Failed to load dashboard (${res.status})`)
  }
  return res.json() as Promise<DashboardDto>
}
