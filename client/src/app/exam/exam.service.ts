import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { BehaviorSubject, Observable } from 'rxjs';

export interface IResponse<T> {
  result: T;
  isSuccess: boolean;
  message: string;
}


export interface Test {
  testId: string;
  title: string;
  description: string;
  startDate: string;
  endDate: string;
  duration: number;
  codingQuestions: CodingQuestion[];
}

export interface CodingQuestion {
  codingQuestionId: string;
  title: string;
  description: string;
  difficulty: number;
}

export interface StartTestResponseDto {
  test: Test;
  studentAnswerMetadataId: string;
}

export interface SubmitQuestionDto {
  submittedCode: string;
  marksAwarded: number;
}



@Injectable({
  providedIn: 'root'
})
export class ExamService {
  private apiUrl = 'https://localhost:5000/api/StudentTest'; // Replace with your actual API URL

  // BehaviorSubject to hold the current test data
  private testSubject = new BehaviorSubject<StartTestResponseDto | null>(this.getTestFromLocalStorage());

  constructor(private http: HttpClient) {}

  // Expose the test as an observable for components to subscribe to
  getTest(): Observable<StartTestResponseDto | null> {
    return this.testSubject.asObservable();
  }

  // Update the test subject when a test is started
  startTest(testId: string): Observable<IResponse<StartTestResponseDto>> {
    return new Observable<IResponse<StartTestResponseDto>>(observer => {
      this.http.post<IResponse<StartTestResponseDto>>(`${this.apiUrl}/StartTest?testId=${testId}`, {}).subscribe({
        next: (data) => {
          if (data.isSuccess) {
            this.setTestInLocalStorage(data.result); // Save test data to local storage
            this.testSubject.next(data.result); // Update the BehaviorSubject with the new test data
          }
          observer.next(data);
          observer.complete();
        },
        error: (err) => {
          observer.error(err);
        }
      });
    });
  }

  // Method to submit a question
  submitQuestion(submitQuestionDto: SubmitQuestionDto, questionId: string): Observable<IResponse<any>> {
    return this.http.post<IResponse<any>>(`${this.apiUrl}/SubmitQuestion?questionId=${questionId}`, submitQuestionDto);
  }

  // Method to submit the entire test
  submitTest(testId: string): Observable<IResponse<any>> {
    return new Observable<IResponse<any>>(observer => {
      this.http.post<IResponse<any>>(`${this.apiUrl}/SubmitTest?testId=${testId}`, {}).subscribe({
        next: (data) => {
          if (data.isSuccess) {
            this.clearTestInLocalStorage(); // Clear the local storage when the test is submitted
            this.testSubject.next(null); // Clear the test data when the test is submitted
          }
          observer.next(data);
          observer.complete();
        },
        error: (err) => {
          observer.error(err);
        }
      });
    });
  }

  // Optional method to clear the test data manually
  clearTest(): void {
    this.clearTestInLocalStorage(); // Clear local storage when manually clearing the test data
    this.testSubject.next(null);
  }

  // Helper method to get test data from local storage
  private getTestFromLocalStorage(): StartTestResponseDto | null {
    const testJson = localStorage.getItem('test');
    return testJson ? JSON.parse(testJson) : null;
  }

  // Helper method to set test data in local storage
  private setTestInLocalStorage(test: StartTestResponseDto): void {
    localStorage.setItem('test', JSON.stringify(test));
  }

  // Helper method to clear test data from local storage
  private clearTestInLocalStorage(): void {
    localStorage.removeItem('test');
  }
}