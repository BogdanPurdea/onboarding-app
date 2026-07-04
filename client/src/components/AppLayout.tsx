import type { AppLayoutProps } from '../types/components'

export function AppLayout({ children }: AppLayoutProps) {
  return (
    <div className="min-h-svh bg-slate-50 font-sans text-slate-800 antialiased dark:bg-slate-950 dark:text-slate-200">
      <main className="mx-auto w-full max-w-2xl px-4 py-8 sm:px-6">
        {children}
      </main>
    </div>
  )
}
