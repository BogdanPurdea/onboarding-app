import type { ReactNode } from 'react'
import type { TaskDto, DepartmentConfig, TimelinePhase } from './index'

export interface AppLayoutProps {
  children: ReactNode
}

export interface DepartmentCardProps {
  dept: DepartmentConfig
  onSelectRole: (roleKey: string) => void
}

export interface DepartmentSelectorProps {
  onSelectRole: (roleKey: string) => void
}

export interface OnboardingChecklistProps {
  role: string
}

export interface OnboardingProgressHeaderProps {
  completedCount: number
  totalCount: number
  progressPercent: number
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
}
