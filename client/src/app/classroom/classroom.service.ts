import { Injectable } from '@angular/core';
import { catchError, Observable, of, throwError } from 'rxjs';

import { HttpClient, HttpErrorResponse } from '@angular/common/http';

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
  teacher: any; // You might want to define a Teacher interface if necessary
  creationDate: string;
  isArchived: boolean;
  tests: any[]; // You might want to define a Test interface if necessary
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

  private apiUrl = 'https://localhost:5000/api/Classroom'; // Replace with your actual API URL
  private getMyUrl = 'https://localhost:5000/api/classroom/GetMy';

  constructor(private http: HttpClient) { }

  createClassroom(classroom: createClassroom): Observable<IResponse<any>> {
    return this.http.post<IResponse<any>>(this.apiUrl, classroom);
  }

  getMyClassrooms(): Observable<IResponse<Classroom[]>> {
    return this.http.get<IResponse<Classroom[]>>(this.getMyUrl);
  }

  private testUrl = 'https://localhost:5000/api/test';
  createTest(test: TestDto): Observable<IResponse<any>> {
    return this.http.post<IResponse<any>>(`https://localhost:5000/api/Test`, test);
  }

  getTestsByClassroom(classroomId: string): Observable<IResponse<Test[]>> {
    return this.http.get<IResponse<Test[]>>(`https://localhost:5000/api/Test/GetAllForClassroom?classroomId=${classroomId}`);
  }
}
