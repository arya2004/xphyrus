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
  private readonly baseUrl = 'https://localhost:5000/api/CodingQuestion';
  private readonly testUrl = 'https://localhost:5000/api/TestCase';

  constructor(private http: HttpClient) {}

  /**
   * Creates a new coding question.
   * @param codingQuestion - The coding question data to create.
   * @returns An observable with the response.
   */
  createCodingQuestion(codingQuestion: CodingQuestion): Observable<IResponse<any>> {
    return this.http.post<IResponse<any>>(this.baseUrl, codingQuestion);
  }

  /**
   * Retrieves all coding questions associated with a specific test.
   * @param testId - The ID of the test.
   * @returns An observable with the response containing an array of coding questions.
   */
  getCodingQuestionsByTest(testId: string): Observable<IResponse<CodingQuestion[]>> {
    const url = `${this.baseUrl}/GetMy?testId=${testId}`;
    return this.http.get<IResponse<CodingQuestion[]>>(url);
  }

  /**
   * Creates a new test case for a specific coding question.
   * @param testCase - The test case data to create.
   * @returns An observable with the response.
   */
  createTestCase(testCase: CreateTestCase): Observable<IResponse<any>> {
    return this.http.post<IResponse<any>>(this.testUrl, testCase);
  }

  /**
   * Retrieves all test cases associated with a specific coding question.
   * @param codingQuestionId - The ID of the coding question.
   * @returns An observable with the response containing an array of test cases.
   */
  getTestCasesByCodingQuestion(codingQuestionId: string): Observable<IResponse<TestCase[]>> {
    const url = `${this.testUrl}/GetAllForAssessment?assessmentId=${codingQuestionId}`;
    return this.http.get<IResponse<TestCase[]>>(url);
  }
}
