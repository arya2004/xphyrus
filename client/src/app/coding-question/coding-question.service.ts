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


export interface TestCase {
  testCaseId?: string;
  inputCase: string;
  outputCase: string;
  description: string;
  isHidden: boolean;
  marks: number;
}

export interface CreateTestCase {
  inputCase: string;
  outputCase: string;
  description: string;
  isHidden: boolean;
  codingQuestionId: string;
  marks: number;
}

@Injectable({
  providedIn: 'root'
})
export class CodingQuestionService {

  private baseUrl = 'https://localhost:5000/api/CodingQuestion';

  constructor(private http: HttpClient) {}

  createCodingQuestion(codingQuestion: CodingQuestion): Observable<IResponse<any>> {
    return this.http.post<IResponse<any>>(`${this.baseUrl}`, codingQuestion);
  }

  getCodingQuestionsByTest(testId: string): Observable<IResponse<CodingQuestion[]>> {
    return this.http.get<IResponse<CodingQuestion[]>>(`${this.baseUrl}/GetMy?testId=${testId}`);
  }

  private testUrl = 'https://localhost:5000/api/TestCase';

  createTestCase(testCase: CreateTestCase): Observable<IResponse<any>> {
    return this.http.post<IResponse<any>>(`${this.testUrl}`, testCase);
  }

  getTestCasesByCodingQuestion(codingQuestionId: string): Observable<IResponse<TestCase[]>> {
    return this.http.get<IResponse<TestCase[]>>(`${this.testUrl}/GetAllForAssessment?assessmentId=${codingQuestionId}`);
  }
}
