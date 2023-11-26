export interface ITestRun {
    source_code: string
    language_id: number
    stdin: string
    exprected_output: string
  }
  
  export class TestRun implements ITestRun {
    source_code: string
    language_id: number
    stdin: string
    exprected_output: string
  }
  