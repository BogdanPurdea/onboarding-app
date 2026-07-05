import type { ContactDto } from '../../types/index'

export function ContactCard({ name, role, avatarInitials, email, bio }: ContactDto) {
  return (
    <div className="card-base p-4 flex gap-4 items-start">
      <div className="w-10 h-10 rounded-full flex items-center justify-center text-sm font-bold shrink-0 bg-indigo-100 text-indigo-700 dark:bg-orange-900/30 dark:text-orange-300">
        {avatarInitials}
      </div>
      <div className="min-w-0">
        <p className="text-sm font-semibold text-slate-900 dark:text-slate-100 truncate">{name}</p>
        <p className="text-xs text-slate-500 dark:text-slate-400 truncate">{role}</p>
        <p className="mt-1 text-xs text-slate-500 dark:text-slate-400 leading-relaxed line-clamp-2">{bio}</p>
        <a
          href={`mailto:${email}`}
          className="mt-1.5 inline-block text-xs text-indigo-600 hover:underline dark:text-orange-400"
        >
          {email}
        </a>
      </div>
    </div>
  )
}
