export interface ISubmit {
    source_code: string
    language_id: number
    codingAssignmentId: string
}
  
export class Submit implements ISubmit {
    source_code: string
    language_id: number
    codingAssignmentId: string
  }
  