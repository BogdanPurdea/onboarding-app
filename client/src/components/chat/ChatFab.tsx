import type { ChatFabProps } from '../../types/components'

export function ChatFab({ onClick }: ChatFabProps) {
  return (
    <button
      onClick={onClick}
      className="fixed bottom-6 right-6 z-40 flex items-center justify-center w-14 h-14 rounded-full bg-gradient-to-tr from-indigo-600 to-violet-600 text-white shadow-lg shadow-indigo-500/30 hover:scale-105 transition-all duration-200 cursor-pointer dark:from-orange-500 dark:to-orange-400 dark:shadow-orange-500/20 active:scale-95 group focus-ring"
      aria-label="Open Ask Meridian Chat"
    >
      <span className="absolute -top-1 -right-1 flex h-3.5 w-3.5">
        <span className="animate-ping absolute inline-flex h-full w-full rounded-full bg-sky-400 opacity-75"></span>
        <span className="relative inline-flex rounded-full h-3.5 w-3.5 bg-sky-500"></span>
      </span>
      <svg
        xmlns="http://www.w3.org/2000/svg"
        className="w-6 h-6 group-hover:rotate-12 transition-transform duration-200"
        fill="none"
        viewBox="0 0 24 24"
        stroke="currentColor"
        strokeWidth={2}
      >
        <path
          strokeLinecap="round"
          strokeLinejoin="round"
          d="M8 12h.01M12 12h.01M16 12h.01M21 12c0 4.418-4.03 8-9 8a9.863 9.863 0 01-4.255-.949L3 20l1.395-3.72C3.512 15.042 3 13.574 3 12c0-4.418 4.03-8 9-8s9 3.582 9 8z"
        />
      </svg>
    </button>
  )
}
