const SESSION_TOKEN_PREFIX = 'meridian_session_token_'
const inMemoryTokens: Record<string, string> = {}

/**
 * Reads the anonymous session token scoped to a specific department role.
 *
 * Resolution order:
 * 1. `?token=` URL query parameter (recovery link flow).
 *    If found and the URL `role` parameter matches the requested role,
 *    the token is adopted into localStorage for this role and the token param
 *    is stripped from the address bar via `history.replaceState`.
 * 2. An existing token already stored in `localStorage` for this role.
 * 3. A freshly generated UUID v4, persisted in `localStorage` for this role.
 *
 * Falls back to an in-memory cache if localStorage is unavailable (e.g., disabled).
 */
export function getOrCreateSessionToken(role: string): string {
  const tokenKey = `${SESSION_TOKEN_PREFIX}${role}`

  // 1. Check for a recovery token in the URL
  const params = new URLSearchParams(window.location.search)
  const urlToken = params.get('token')
  const urlRole = params.get('role')

  if (urlToken && urlRole === role) {
    try {
      localStorage.setItem(tokenKey, urlToken)
    } catch {
      // localStorage is unavailable
    }
    inMemoryTokens[role] = urlToken

    // Strip ?token= from the address bar without causing a page reload
    params.delete('token')
    const newSearch = params.toString()
    const cleanUrl = newSearch
      ? `${window.location.pathname}?${newSearch}`
      : window.location.pathname
    window.history.replaceState({}, '', cleanUrl)
    return urlToken
  }

  // 2. Check in-memory cache first
  if (inMemoryTokens[role]) {
    return inMemoryTokens[role]
  }

  // 3. Existing persisted token
  let token: string | null = null
  try {
    token = localStorage.getItem(tokenKey)
  } catch {
    // localStorage is unavailable
  }

  if (token) {
    inMemoryTokens[role] = token
    return token
  }

  // 4. Generate new token
  token = crypto.randomUUID()
  inMemoryTokens[role] = token
  try {
    localStorage.setItem(tokenKey, token)
  } catch {
    // localStorage is unavailable
  }
  return token
}

/**
 * Builds a shareable recovery URL that encodes the current session token for the active role.
 * Preserves the active department role query parameter.
 *
 * Example output: `https://example.com/?role=engineering&token=d30f019b-...`
 */
export function buildRecoveryLink(role: string): string {
  const token = getOrCreateSessionToken(role)
  const params = new URLSearchParams(window.location.search)
  params.set('role', role)
  params.set('token', token)
  return `${window.location.origin}${window.location.pathname}?${params.toString()}`
}
