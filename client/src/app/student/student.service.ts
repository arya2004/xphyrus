import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';


export interface IResponse<T> {
  result: T;
  isSuccess: boolean;
  message: string;
}

export interface StudentExamOverviewDto {
  examId: string;
  testTitle: string;
  startDate: string;
  endDate: string;
  duration: number;
}

export interface StudentExamDetailsDto {
  examId: string;
  startDate: string;
  endDate: string;
  duration: number;
  studentAnswers: StudentAnswerDto[];
}

export interface StudentAnswerDto {
  submittedCode: string;
  marksAwarded: number;
  submittedDate: string;
  questionText: string;
}

export interface StudentTestDto {
  testId: string;
  title: string;
  description: string;
  codingQuestions: StudentCodingQuestionDto[];
}

export interface StudentCodingQuestionDto {
  codingQuestionId: string;
  questionText: string;
  maxMarks: number;
}


@Injectable({
  providedIn: 'root'
})
export class StudentService {

  private apiUrl = 'https://localhost:5000/api/StudentDashboard'; // Replace with your actual API URL

  constructor(private http: HttpClient) { }

  // Get all exams taken by the student
  getExamsTaken(): Observable<IResponse<StudentExamOverviewDto[]>> {
    return this.http.get<IResponse<StudentExamOverviewDto[]>>(`${this.apiUrl}/GetExamsTaken`);
  }

  // Get details of a specific exam with all student answers and the associated test details
  getExamDetails(examId: string): Observable<IResponse<{ examDetails: StudentExamDetailsDto; test: StudentTestDto }>> {
    return this.http.get<IResponse<{ examDetails: StudentExamDetailsDto; test: StudentTestDto }>>(`${this.apiUrl}/GetExamDetails?examId=${examId}`);
  }
}
