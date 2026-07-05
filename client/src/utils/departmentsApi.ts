import type { DepartmentConfig } from '../types/index'

const API_BASE = import.meta.env.VITE_API_URL ?? ''

/**
 * Fetches the lightweight list of all departments.
 */
export async function fetchDepartments(signal?: AbortSignal): Promise<DepartmentConfig[]> {
  const res = await fetch(`${API_BASE}/api/departments`, { signal })
  if (!res.ok) {
    throw new Error('Failed to fetch departments')
  }
  return res.json() as Promise<DepartmentConfig[]>
}
