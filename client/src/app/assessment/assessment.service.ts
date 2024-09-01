import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

export interface IResponse<T> {
  result: T;
  isSuccess: boolean;
  message: string;
}

export interface CodingQuestion {
  codingQuestionId?: string;
  title: string;
  description: string;
  difficulty: number;
  testId: string;
}


export interface IResponse<T> {
  result: T;
  isSuccess: boolean;
  message: string;
}

export interface TeacherTestMetadataDto {
  studentAnswerMetadataId: string;
  studentName: string;
  startDate: string;
  endDate: string;
  duration: number;
  test: TeacherTestDto;
  studentAnswers: TeacherStudentAnswerDto[];
}

export interface TeacherStudentAnswerDto {
  studentAnswerId: string;
  submittedCode: string;
  marksAwarded: number;
  submittedDate: string;
  codingQuestion: TeacherCodingQuestionDto;
}

export interface TeacherStudentExamDetailsDto {
  studentAnswerMetadataId: string;
  studentName: string;
  startDate: string;
  endDate: string;
  duration: number;
  studentAnswers: TeacherStudentAnswerDto[];
}

export interface TeacherTestDto {
  testId: string;
  testName: string;
  description: string;
  dateCreated: string;
  dateScheduled: string;
}

export interface TeacherCodingQuestionDto {
  codingQuestionId: string;
  questionText: string;
  maximumMarks: number;
}


@Injectable({
  providedIn: 'root'
})
export class AssessmentService {
  private readonly baseUrl = 'https://localhost:5000/api/CodingQuestion/GetAllForTest';

  constructor(private http: HttpClient) {}

  /**
   * Fetches all coding questions associated with a specific test.
   * @param testId - The ID of the test for which to retrieve coding questions.
   * @returns An observable containing the response with an array of coding questions.
   */
  getCodingQuestionsByTest(testId: string): Observable<IResponse<CodingQuestion[]>> {
    const url = `${this.baseUrl}?testId=${testId}`;
    return this.http.get<IResponse<CodingQuestion[]>>(url);
  }

  private resUrl = 'https://localhost:5000/api/TeacherDashboard'; // Replace with your actual API URL


  getTestMetadata(testId: string): Observable<IResponse<TeacherTestMetadataDto[]>> {
    return this.http.get<IResponse<TeacherTestMetadataDto[]>>(`${this.resUrl}/GetTestMetadata?testId=${testId}`);
  }

  // Get detailed information for a specific student's exam
  getStudentExamDetails(studentAnswerMetadataId: string): Observable<IResponse<{ examDetails: TeacherStudentExamDetailsDto; test: TeacherTestDto }>> {
    return this.http.get<IResponse<{ examDetails: TeacherStudentExamDetailsDto; test: TeacherTestDto }>>(`${this.resUrl}/GetStudentExamDetails?studentAnswerMetadataId=${studentAnswerMetadataId}`);
  }
}
