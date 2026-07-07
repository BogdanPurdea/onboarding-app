import { describe, it, expect, beforeEach } from 'vitest'
import { getOrCreateSessionToken, buildRecoveryLink } from './sessionToken'

describe('sessionToken', () => {
  beforeEach(() => {
    localStorage.clear()
    // Reset history state / URL search params using a relative URL to avoid JSDOM origin conflicts
    window.history.replaceState({}, '', '/')
  })

  it('should generate a new UUID token if none exists in localStorage or URL', () => {
    const role = 'engineering'
    const token = getOrCreateSessionToken(role)

    expect(token).toBeDefined()
    expect(token.length).toBe(36) // UUID length
    expect(localStorage.getItem(`meridian_session_token_${role}`)).toBe(token)
  })

  it('should return existing token from localStorage if present', () => {
    const role = 'sales'
    const existingToken = '12345678-1234-1234-1234-1234567890ab'
    localStorage.setItem(`meridian_session_token_${role}`, existingToken)

    const token = getOrCreateSessionToken(role)
    expect(token).toBe(existingToken)
  })

  it('should read token from URL, save to localStorage, and clean the address bar', () => {
    const role = 'marketing'
    const urlToken = 'abcdef01-abcd-abcd-abcd-abcdef012345'
    
    // Set up window.location.search with a relative URL
    window.history.replaceState({}, '', `/?role=${role}&token=${urlToken}`)

    const token = getOrCreateSessionToken(role)
    expect(token).toBe(urlToken)
    expect(localStorage.getItem(`meridian_session_token_${role}`)).toBe(urlToken)

    // Verify token search param is stripped from the URL
    const searchParams = new URLSearchParams(window.location.search)
    expect(searchParams.has('token')).toBe(false)
    expect(searchParams.get('role')).toBe(role)
  })

  it('should build correct recovery link', () => {
    const role = 'finance'
    // First generate the token
    const token = getOrCreateSessionToken(role)

    const recoveryLink = buildRecoveryLink(role)
    expect(recoveryLink).toContain(`role=${role}`)
    expect(recoveryLink).toContain(`token=${token}`)
  })
})
