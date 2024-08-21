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

  private baseUrl = 'https://localhost:5000/api/CodingQuestion/GetAllForTest';

  constructor(private http: HttpClient) {}

 
  getCodingQuestionsByTest(testId: string): Observable<IResponse<CodingQuestion[]>> {
    return this.http.get<IResponse<CodingQuestion[]>>(`${this.baseUrl}?testId=${testId}`);
  }
}
