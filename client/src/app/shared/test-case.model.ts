// test-case.model.ts
export interface TestCase {
    id: number;
    description: string;
    input: string;
    expectedOutput: string;
    marks: number;
    hidden?: boolean;
  }
  