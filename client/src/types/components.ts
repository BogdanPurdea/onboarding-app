import type { ReactNode } from 'react'
import type { TaskDto, DepartmentConfig, TimelinePhase, TaskInstructionsDto, ChatMessage } from './index'

export interface AppLayoutProps {
  children: ReactNode
}

export interface DepartmentCardProps {
  dept: DepartmentConfig
  onSelectRole: (roleKey: string) => void
}

export interface DepartmentSelectorProps {
  onSelectRole: (roleKey: string) => void
  departments: DepartmentConfig[]
}

export interface OnboardingChecklistProps {
  role: string
}

export interface OnboardingProgressHeaderProps {
  completedCount: number
  totalCount: number
  progressPercent: number
  onCopyLink?: () => void
  copyStatus?: 'idle' | 'copied' | 'error'
}

export interface OnboardingPhaseTabsProps {
  activePhase: TimelinePhase
  onChangePhase: (phase: TimelinePhase) => void
  phaseStats: { [key in TimelinePhase]?: { total: number; completed: number } }
}

export interface OnboardingTaskItemProps {
  task: TaskDto
  isCompleted: boolean
  isUnlocked: boolean
  onToggle: () => void
  prerequisiteTitles: string[]
  onClick?: () => void
  isLoadingInstructions?: boolean
}

export interface OnboardingDashboardProps {
  roleKey: string
}

export interface ContactPickerModalProps {
  name: string
  role: string
  avatarInitials: string
  slackMemberId?: string
  googleMeetUrl?: string
  onClose: () => void
}

export interface TaskInstructionsDrawerProps {
  instructions: TaskInstructionsDto
  onClose: () => void
}

export interface AskMeridianChatProps {
  role: string | null
}

export interface ChatFabProps {
  onClick: () => void
}

export interface ChatDrawerProps {
  isOpen: boolean
  onClose: () => void
  role: string | null
  messages: ChatMessage[]
  isTyping: boolean
  onSendMessage: (text: string) => void
}

export interface ChatMessageListProps {
  messages: ChatMessage[]
  isTyping: boolean
}

export interface ChatSuggestionChipsProps {
  role: string | null
  onChipClick: (text: string) => void
  isTyping: boolean
}

export interface ChatInputProps {
  onSendMessage: (text: string) => void
  isTyping: boolean
}

