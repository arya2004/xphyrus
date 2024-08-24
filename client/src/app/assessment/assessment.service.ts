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
}
