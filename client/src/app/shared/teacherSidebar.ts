export interface Question {
    id: number;
    title: string;
  }
  
  export interface Test {
    id: number;
    title: string;
    questions?: Question[];
  }
  
  export interface Classroom {
    id: number;
    title: string;
    tests?: Test[];
  }
  