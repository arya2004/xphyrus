import { CloseScrollStrategy } from "@angular/cdk/overlay"

export interface IAssignment {
  title: string
  isStrict: boolean
  description: string
  startDate: Date
  endDate: Date
  duration: TimeRanges
  evaluationCases: IEvaluationCase[]
}
export class Assignment implements IAssignment {
  title: string
  isStrict: boolean
  description: string
  startDate: Date
  endDate: Date
  duration: TimeRanges
  evaluationCases: IEvaluationCase[]
}

export interface IEvaluationCase {
  inputCase: string
  outputCase: string
}
export class EvaluationCase implements IEvaluationCase {
  inputCase: string
  outputCase: string
}


export interface McQ {
  question: string
  correctAnswer: number
  options: Option[]
}

export interface Option {
  optionNumber: number
  value: string
}
