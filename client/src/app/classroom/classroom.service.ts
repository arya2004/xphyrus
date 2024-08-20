import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { Classroom } from '../shared/teacherSidebar';

@Injectable({
  providedIn: 'root'
})
export class ClassroomService {

  constructor() { }


  // Simulate a data fetch from a server with an Observable
  getSidebarData(): Observable<Classroom[]> {
    const sidebarData: Classroom[] = [
      {
        id: 101,
        title: 'Classroom 1',
        tests: [
          {
            id: 1,
            title: 'Test 1',
            questions: [
              { id: 1, title: 'Question 1' },
              { id: 2, title: 'Question 2' }
            ]
          }
        ]
      },
      {
        id: 102,
        title: 'Classroom 2',
        tests: [
          {
            id: 1,
            title: 'Test 1',
            questions: [
              { id: 1, title: 'Question 1' },
              { id: 2, title: 'Question 2' },
              { id: 3, title: 'Question 3' }
            ]
          },
          {
            id: 2,
            title: 'Test 2',
            questions: [
              { id: 1, title: 'Question 1' },
              { id: 2, title: 'Question 2' }
            ]
          }
        ]
      }
      // Add more classrooms as needed...
    ];
    
    // Use 'of' to simulate an Observable data stream
    return of(sidebarData);
  }
}
