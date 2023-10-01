export interface Assesment{
  name: string
  description: string
  code: string
  isStrict: boolean
  startDate: string
  endDate: string
  duration: string
  codings: Coding[]
  mcQs: McQ[]
}

export interface Coding {
  title: string
  prompt: string
  language: string
  inputFormat: string
  outputFormat: string
  constrain1: string
  constrain2: string
  constrain3: string
  evliationCases: EvliationCase[]
}

export interface EvliationCase {
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
