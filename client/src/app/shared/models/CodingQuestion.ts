export enum Difficulty {
    Easy = 1,
    Medium = 2,
    Hard = 3
}

export interface CodingQuestion {
    title: string;
    difficulty: Difficulty;
    totalTestCases: number;
    totalMarks: number;
}