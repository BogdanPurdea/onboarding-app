export interface TaskDto {
  id: number
  departmentId: number
  departmentName: string
  title: string
  description: string
  timelinePhase: number
  displayOrder: number
  prerequisiteTaskIds: number[]
}
