export interface IAssesmentHome {
  codingAssesmentId: string
  title: string
  description: string
  joinCode: string
  startDate: Date
  endDate: Date
  evaluationCases: any
}

export class AssesmentHome implements IAssesmentHome{
  codingAssesmentId: string
  title: string
  description: string
  joinCode: string
  startDate: Date
  endDate: Date
  evaluationCases: any
}
