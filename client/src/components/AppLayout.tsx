import type { ReactNode } from 'react'

interface AppLayoutProps {
  children: ReactNode
}

export function AppLayout({ children }: AppLayoutProps) {
  return (
    <div className="min-h-svh bg-slate-50 font-sans text-slate-800 antialiased">
      <main className="mx-auto w-full max-w-2xl px-4 py-8 sm:px-6">
        {children}
      </main>
    </div>
  )
}
