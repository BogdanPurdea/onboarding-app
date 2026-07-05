export const TimelinePhase = {
  WeekOne: 1,
  WeekTwo: 2,
  WeekThree: 3,
  WeekFour: 4
} as const;

export type TimelinePhase = typeof TimelinePhase[keyof typeof TimelinePhase];

export interface TaskDto {
  id: number
  departmentId: number
  departmentName: string
  title: string
  description: string
  timelinePhase: TimelinePhase
  displayOrder: number
  prerequisiteTaskIds: number[]
}

/** Matches GET /api/departments response items */
export interface DepartmentConfig {
  id: number
  name: string
  roleKey: string
  tagline: string
}

/** Matches GET /api/departments/{roleKey}/dashboard */
export interface DayScheduleDto {
  day: string
  type: 'office' | 'remote'
}

export interface ContactDto {
  name: string
  role: string
  avatarInitials: string
  email: string
  bio: string
}

export interface DashboardDto {
  departmentName: string
  roleKey: string
  tagline: string
  welcomeMessage: string
  weeklySchedule: DayScheduleDto[]
  contacts: ContactDto[]
}
