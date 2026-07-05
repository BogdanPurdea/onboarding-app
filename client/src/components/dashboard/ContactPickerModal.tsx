import type { ContactPickerModalProps } from '../../types/components'

export function ContactPickerModal({
  name,
  role,
  avatarInitials,
  slackMemberId,
  googleMeetUrl,
  onClose
}: ContactPickerModalProps) {
  return (
    <div
      className="fixed inset-0 z-50 flex items-center justify-center bg-slate-900/40 backdrop-blur-sm"
      onClick={onClose}
    >
      <div
        className="bg-white dark:bg-slate-900 rounded-2xl shadow-2xl border border-slate-200 dark:border-slate-800 p-6 w-72 flex flex-col gap-4"
        onClick={e => e.stopPropagation()}
      >
        {/* Header */}
        <div className="flex items-center gap-3">
          <div className="w-10 h-10 rounded-full flex items-center justify-center text-sm font-bold bg-indigo-50 text-indigo-700 dark:bg-orange-950/20 dark:text-orange-300">
            {avatarInitials}
          </div>
          <div className="min-w-0">
            <p className="text-sm font-semibold text-slate-900 dark:text-slate-100 truncate">{name}</p>
            <p className="text-xs text-slate-500 dark:text-slate-400 truncate">{role}</p>
          </div>
        </div>

        <p className="text-xs text-slate-500 dark:text-slate-400">Choose how you'd like to reach out:</p>

        {/* Channel options */}
        <div className="flex flex-col gap-2">
          {slackMemberId && (
            <a
              href={`https://slack.com/app_redirect?channel=${slackMemberId}`}
              target="_blank"
              rel="noopener noreferrer"
              onClick={onClose}
              className="flex items-center gap-3 px-4 py-3 rounded-xl bg-indigo-50 hover:bg-indigo-100 text-indigo-700 dark:bg-orange-950/20 dark:hover:bg-orange-950/40 dark:text-orange-300 transition duration-150"
            >
              <span className="text-sm font-semibold">Open in Slack</span>
            </a>
          )}
          {googleMeetUrl && (
            <a
              href={googleMeetUrl}
              target="_blank"
              rel="noopener noreferrer"
              onClick={onClose}
              className="flex items-center gap-3 px-4 py-3 rounded-xl bg-indigo-50 hover:bg-indigo-100 text-indigo-700 dark:bg-orange-950/20 dark:hover:bg-orange-950/40 dark:text-orange-300 transition duration-150"
            >
              <span className="text-sm font-semibold">Join on Google Meet</span>
            </a>
          )}
        </div>

        <button
          onClick={onClose}
          className="text-xs text-slate-400 hover:text-slate-600 dark:hover:text-slate-300 transition-colors cursor-pointer text-center"
        >
          Dismiss
        </button>
      </div>
    </div>
  )
}
