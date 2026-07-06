const SESSION_TOKEN_KEY = 'meridian_session_token'

/**
 * Reads the anonymous session token.
 *
 * Resolution order:
 * 1. `?token=` URL query parameter (recovery link flow).
 *    If found, the token is adopted into localStorage and the param is
 *    stripped from the address bar via `history.replaceState`.
 * 2. An existing token already stored in `localStorage`.
 * 3. A freshly generated UUID v4, persisted in `localStorage`.
 */
export function getOrCreateSessionToken(): string {
  // 1. Check for a recovery token in the URL
  const params = new URLSearchParams(window.location.search)
  const urlToken = params.get('token')
  if (urlToken) {
    localStorage.setItem(SESSION_TOKEN_KEY, urlToken)
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
  let token = localStorage.getItem(SESSION_TOKEN_KEY)
  if (!token) {
    // 3. Generate a new token
    token = crypto.randomUUID()
    localStorage.setItem(SESSION_TOKEN_KEY, token)
  }
  return token
}

/**
 * Builds a shareable recovery URL that encodes the current session token.
 * Preserves any existing query parameters (e.g. `?role=`).
 *
 * Example output: `https://example.com/?role=engineering&token=d30f019b-...`
 */
export function buildRecoveryLink(): string {
  const token = getOrCreateSessionToken()
  const params = new URLSearchParams(window.location.search)
  params.set('token', token)
  return `${window.location.origin}${window.location.pathname}?${params.toString()}`
}

