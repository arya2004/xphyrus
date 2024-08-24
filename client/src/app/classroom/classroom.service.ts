import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

import { createClassroom } from './classroom-dashboard/classroom-dashboard.component';

export interface IResponse<T> {
  result: T;
  isSuccess: boolean;
  message: string;
}

export interface Classroom {
  classroomId: string;
  name: string;
  number: string;
  description: string;
  type: number;
  teacher: any; // Consider defining a Teacher interface if necessary
  creationDate: string;
  isArchived: boolean;
  tests: any[]; // Consider defining a Test interface if necessary
}

export interface TestDto {
  title: string;
  description: string;
  startDate: string;
  endDate: string;
  duration: number;
  classroomId: string;
}

export interface Test {
  testId: string;
  title: string;
  description: string;
  startDate: string;
  endDate: string;
  duration: number;
  classroomId: string;
}

@Injectable({
  providedIn: 'root'
})
export class ClassroomService {
  private readonly apiUrl = 'https://localhost:5000/api/Classroom'; // Base API URL
  private readonly getMyUrl = `${this.apiUrl}/GetMy`; // URL to get classrooms for the current user
  private readonly testUrl = 'https://localhost:5000/api/Test'; // Base API URL for tests

  constructor(private http: HttpClient) { }

  /**
   * Creates a new classroom.
   * @param classroom - The classroom data to create.
   * @returns An observable with the response.
   */
  createClassroom(classroom: createClassroom): Observable<IResponse<any>> {
    return this.http.post<IResponse<any>>(this.apiUrl, classroom);
  }

  /**
   * Retrieves all classrooms associated with the current user.
   * @returns An observable with the response containing an array of classrooms.
   */
  getMyClassrooms(): Observable<IResponse<Classroom[]>> {
    return this.http.get<IResponse<Classroom[]>>(this.getMyUrl);
  }

  /**
   * Creates a new test within a specific classroom.
   * @param test - The test data to create.
   * @returns An observable with the response.
   */
  createTest(test: TestDto): Observable<IResponse<any>> {
    return this.http.post<IResponse<any>>(this.testUrl, test);
  }

  /**
   * Retrieves all tests associated with a specific classroom.
   * @param classroomId - The ID of the classroom.
   * @returns An observable with the response containing an array of tests.
   */
  getTestsByClassroom(classroomId: string): Observable<IResponse<Test[]>> {
    const url = `${this.testUrl}/GetAllForClassroom?classroomId=${classroomId}`;
    return this.http.get<IResponse<Test[]>>(url);
  }
}
