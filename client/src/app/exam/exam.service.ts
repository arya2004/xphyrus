import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment.development';
import { TestRun } from '../shared/models/ITestRun';
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
  classroomId: string;
  codingQuestions: CodingQuestion[];
}

export interface CodingQuestion {
  codingQuestionId: string;
  questionText: string;
  maxMarks: number;
  testId: string;
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
  private testSubject = new BehaviorSubject<StartTestResponseDto | null>(null);

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
    this.testSubject.next(null);
  }
}
