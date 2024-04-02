export interface IExaminee {
    name: string
    linkedin: string
    twitter: string
    email: string
    language: string
    source_code: string
  }
  
export class Examinee implements IExaminee {
   
      public name: string
      public linkedin: string
      public twitter: string
      public email: string
      public language: string
      public source_code: string
 
  }