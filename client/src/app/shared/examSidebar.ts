export interface Question {
    id: number;
    title: string;
  }
  
  export interface Exam {
    id: number;
    title: string;
    questions: Question[];
  }
  