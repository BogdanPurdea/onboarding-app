const SESSION_TOKEN_KEY = 'meridian_session_token'

/**
 * Reads the anonymous session token from localStorage.
 * If one does not exist yet, generates a new UUID v4 and persists it.
 * The token is stable per browser and serves as the anonymous identity
 * for syncing onboarding progress with the backend.
 */
export function getOrCreateSessionToken(): string {
  let token = localStorage.getItem(SESSION_TOKEN_KEY)
  if (!token) {
    token = crypto.randomUUID()
    localStorage.setItem(SESSION_TOKEN_KEY, token)
  }
  return token
}
