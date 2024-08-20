import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { Classroom } from '../shared/teacherSidebar';

@Injectable({
  providedIn: 'root'
})
export class CoreService {

  constructor() { }
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
          },
          {
            id: 3,
            title: 'Test 3',
            questions: [
              { id: 1, title: 'Question 1' },
              { id: 2, title: 'Question 2' },
              { id: 3, title: 'Question 3' },
              { id: 4, title: 'Question 4' }
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
              { id: 3, title: 'Question 3' },
              { id: 4, title: 'Question 4' }
            ]
          },
          {
            id: 2,
            title: 'Test 2',
            questions: [
              { id: 1, title: 'Question 1' },
              { id: 2, title: 'Question 2' },
              { id: 3, title: 'Question 3' }
            ]
          },
          {
            id: 3,
            title: 'Test 3',
            questions: [
              { id: 1, title: 'Question 1' },
              { id: 2, title: 'Question 2' }
            ]
          }
        ]
      },
      {
        id: 103,
        title: 'Classroom 3',
        tests: [
          {
            id: 1,
            title: 'Test 1',
            questions: [
              { id: 1, title: 'Question 1' },
              { id: 2, title: 'Question 2' }
            ]
          },
          {
            id: 2,
            title: 'Test 2',
            questions: [
              { id: 1, title: 'Question 1' },
              { id: 2, title: 'Question 2' },
              { id: 3, title: 'Question 3' }
            ]
          }
        ]
      },
      {
        id: 104,
        title: 'Classroom 4',
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
          },
          {
            id: 3,
            title: 'Test 3',
            questions: [
              { id: 1, title: 'Question 1' },
              { id: 2, title: 'Question 2' },
              { id: 3, title: 'Question 3' },
              { id: 4, title: 'Question 4' },
              { id: 5, title: 'Question 5' }
            ]
          },
          {
            id: 4,
            title: 'Test 4',
            questions: [
              { id: 1, title: 'Question 1' },
              { id: 2, title: 'Question 2' },
              { id: 3, title: 'Question 3' },
              { id: 4, title: 'Question 4' }
            ]
          }
        ]
      },
      {
        id: 105,
        title: 'Classroom 5',
        tests: [
          {
            id: 1,
            title: 'Test 1',
            questions: [
              { id: 1, title: 'Question 1' },
              { id: 2, title: 'Question 2' }
            ]
          },
          {
            id: 2,
            title: 'Test 2',
            questions: [
              { id: 1, title: 'Question 1' },
              { id: 2, title: 'Question 2' },
              { id: 3, title: 'Question 3' }
            ]
          },
          {
            id: 3,
            title: 'Test 3',
            questions: [
              { id: 1, title: 'Question 1' },
              { id: 2, title: 'Question 2' },
              { id: 3, title: 'Question 3' },
              { id: 4, title: 'Question 4' }
            ]
          },
          {
            id: 4,
            title: 'Test 4',
            questions: [
              { id: 1, title: 'Question 1' },
              { id: 2, title: 'Question 2' },
              { id: 3, title: 'Question 3' }
            ]
          },
          {
            id: 5,
            title: 'Test 5',
            questions: [
              { id: 1, title: 'Question 1' },
              { id: 2, title: 'Question 2' },
              { id: 3, title: 'Question 3' },
              { id: 4, title: 'Question 4' },
              { id: 5, title: 'Question 5' }
            ]
          }
        ]
      }
    ];
    
    
    // Use 'of' to simulate an Observable data stream
    return of(sidebarData);
  }
}
