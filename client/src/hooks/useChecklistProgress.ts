import { useState, useEffect, useRef } from 'react'
import { fetchProgress, syncProgress } from '../utils/progressApi'
import { getOrCreateSessionToken } from '../utils/sessionToken'

const LOCAL_STORAGE_KEY_PREFIX = 'meridian_completed_tasks_'
const DEBOUNCE_MS = 500

/**
 * Manages the onboarding progress lifecycle for a given role:
 *
 * - On mount: fetches completed task IDs from the database (using a stable
 *   per-browser session token), falling back to localStorage on failure.
 * - On update: updates React state immediately (optimistic), writes to
 *   localStorage for instant cache, and debounces a POST to the backend.
 *
 * @param role - The department role key (e.g. "engineering")
 */
export function useChecklistProgress(role: string) {
  const localStorageKey = `${LOCAL_STORAGE_KEY_PREFIX}${role}`
  const sessionToken = getOrCreateSessionToken(role)

  const [completedIds, setCompletedIds] = useState<number[]>([])
  const [isProgressLoading, setIsProgressLoading] = useState<boolean>(true)

  // Ref to hold the debounce timer handle
  const debounceTimer = useRef<ReturnType<typeof setTimeout> | null>(null)

  // On mount / role change: load from DB, fall back to localStorage
  useEffect(() => {
    const controller = new AbortController()
    setIsProgressLoading(true)

    fetchProgress(sessionToken, controller.signal)
      .then(ids => {
        setCompletedIds(ids)
        // Keep localStorage in sync with the DB values
        try {
          localStorage.setItem(localStorageKey, JSON.stringify(ids))
        } catch {
          // Storage write failure is non-critical
        }
      })
      .catch(err => {
        if (err.name === 'AbortError') return
        // Graceful fallback: use the localStorage cache
        console.warn('Could not fetch progress from server, falling back to localStorage.', err)
        try {
          const saved = localStorage.getItem(localStorageKey)
          setCompletedIds(saved ? (JSON.parse(saved) as number[]) : [])
        } catch {
          setCompletedIds([])
        }
      })
      .finally(() => {
        setIsProgressLoading(false)
      })

    return () => {
      controller.abort()
      // Clear any pending sync on unmount / role change
      if (debounceTimer.current) {
        clearTimeout(debounceTimer.current)
      }
    }
  }, [role, sessionToken, localStorageKey])

  /**
   * Updates the completed task IDs.
   * - Applies change immediately to React state.
   * - Writes to localStorage synchronously (cache).
   * - Debounces the backend sync to avoid rapid consecutive POSTs.
   */
  const updateCompletedIds = (newIds: number[]) => {
    setCompletedIds(newIds)

    try {
      localStorage.setItem(localStorageKey, JSON.stringify(newIds))
    } catch {
      console.error('Failed to write progress to localStorage.')
    }

    if (debounceTimer.current) {
      clearTimeout(debounceTimer.current)
    }

    debounceTimer.current = setTimeout(() => {
      syncProgress({ sessionToken, completedTaskIds: newIds }).catch(err => {
        console.error('Background progress sync failed:', err)
      })
    }, DEBOUNCE_MS)
  }

  return { completedIds, isProgressLoading, updateCompletedIds }
}
