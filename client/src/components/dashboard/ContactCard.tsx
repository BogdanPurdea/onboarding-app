import { useState } from 'react'
import type { ContactDto } from '../../types/index'
import { ContactPickerModal } from './ContactPickerModal'

export function ContactCard({
  name,
  role,
  avatarInitials,
  email,
  bio,
  slackMemberId,
  googleMeetUrl
}: ContactDto) {
  const [open, setOpen] = useState(false)

  return (
    <>
      {/* Card */}
      <div className="card-base p-5 flex gap-4 items-start hover:shadow-md transition-shadow duration-200">
        {/* Avatar */}
        <div className="w-12 h-12 rounded-full flex items-center justify-center text-base font-bold shrink-0 bg-indigo-50 text-indigo-700 dark:bg-orange-950/20 dark:text-orange-300">
          {avatarInitials}
        </div>

        {/* Info Block */}
        <div className="flex-1 min-w-0">
          <h3 className="text-sm font-semibold text-slate-900 dark:text-slate-100 truncate">{name}</h3>
          <p className="text-xs font-medium text-slate-500 dark:text-slate-400 truncate">{role}</p>
          <p className="mt-2 text-xs text-slate-600 dark:text-slate-400 leading-relaxed line-clamp-2">{bio}</p>

          <div className="mt-3 flex items-center justify-between gap-2">
            <a
              href={`mailto:${email}`}
              className="text-xs text-slate-500 hover:text-slate-900 dark:text-slate-400 dark:hover:text-slate-200 underline transition-colors truncate"
              title="Send Email"
            >
              {email}
            </a>
            {(slackMemberId || googleMeetUrl) && (
              <button
                onClick={() => setOpen(true)}
                className="shrink-0 px-3 py-1 rounded text-xs font-semibold bg-indigo-600 hover:bg-indigo-700 text-white dark:bg-orange-400 dark:hover:bg-orange-600 transition duration-150 cursor-pointer"
              >
                Have a chat
              </button>
            )}
          </div>
        </div>
      </div>

      {/* Channel picker modal */}
      {open && (
        <ContactPickerModal
          name={name}
          role={role}
          avatarInitials={avatarInitials}
          slackMemberId={slackMemberId}
          googleMeetUrl={googleMeetUrl}
          onClose={() => setOpen(false)}
        />
      )}
    </>
  )
}
