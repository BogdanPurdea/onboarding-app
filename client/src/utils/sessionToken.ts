const SESSION_TOKEN_PREFIX = 'meridian_session_token_'

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
 */
export function getOrCreateSessionToken(role: string): string {
  const tokenKey = `${SESSION_TOKEN_PREFIX}${role}`

  // 1. Check for a recovery token in the URL
  const params = new URLSearchParams(window.location.search)
  const urlToken = params.get('token')
  const urlRole = params.get('role')

  if (urlToken && urlRole === role) {
    localStorage.setItem(tokenKey, urlToken)
    // Strip ?token= from the address bar without causing a page reload
    params.delete('token')
    const newSearch = params.toString()
    const cleanUrl = newSearch
      ? `${window.location.pathname}?${newSearch}`
      : window.location.pathname
    window.history.replaceState({}, '', cleanUrl)
    return urlToken
  }

  // 2. Existing persisted token
  let token = localStorage.getItem(tokenKey)
  if (!token) {
    // 3. Generate a new token
    token = crypto.randomUUID()
    localStorage.setItem(tokenKey, token)
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


