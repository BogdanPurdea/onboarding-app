import { useState, useEffect } from 'react'

export function useThemeToggle() {
  const [isDark, setIsDark] = useState<boolean>(false)

  // SSR-safe theme initialization
  useEffect(() => {
    try {
      const saved = localStorage.getItem('meridian_theme')
      if (saved === 'dark') {
        setIsDark(true)
      } else if (saved === 'light') {
        setIsDark(false)
      } else {
        const matchesSystem = window.matchMedia('(prefers-color-scheme: dark)').matches
        setIsDark(matchesSystem)
      }
    } catch {
      // Fallback to light mode in case of browser security/sandbox restrictions
      setIsDark(false)
    }
  }, [])

  // Sync theme changes with DOM and localStorage
  useEffect(() => {
    const root = document.documentElement
    const theme = isDark ? 'dark' : 'light'
    root.classList.toggle('dark', isDark)
    try {
      localStorage.setItem('meridian_theme', theme)
    } catch {}
  }, [isDark])

  const toggle = () => setIsDark(prev => !prev)

  return { isDark, toggle }
}
