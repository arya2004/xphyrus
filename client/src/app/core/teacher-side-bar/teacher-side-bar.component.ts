import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Classroom } from 'src/app/shared/teacherSidebar';
import { CoreService } from '../core.service';

@Component({
  selector: 'app-teacher-side-bar',
  templateUrl: './teacher-side-bar.component.html',
  styleUrls: ['./teacher-side-bar.component.scss']
})
export class TeacherSideBarComponent implements OnInit {
  public sidebarItems$: Observable<Classroom[]>;

  constructor(private sidebarService: CoreService) {}

  /**
   * Lifecycle hook that is called after data-bound properties are initialized.
   * Fetches sidebar data from the service.
   */
  ngOnInit(): void {
    this.fetchSidebarData();
  }

  /**
   * Fetches sidebar data and handles any potential errors.
   */
  private fetchSidebarData(): void {
    this.sidebarItems$ = this.sidebarService.getSidebarData().pipe(
      catchError(err => {
        console.error('Failed to load sidebar data:', err);
        // Handle error appropriately here, such as returning an empty array or null
        return [];
      })
    );
  }

  /**
   * Generates a dynamic router link based on the provided IDs.
   * @param classroomId - The ID of the classroom.
   * @param testId - (Optional) The ID of the test.
   * @param questionId - (Optional) The ID of the question.
   * @returns The generated router link as a string.
   */
  generateRouterLink(classroomId: number, testId?: number, questionId?: number): string {
    let link = `/classroom/${classroomId}`;
    if (testId) {
      link += `/test/${testId}`;
    }
    if (questionId) {
      link += `/question/${questionId}`;
    }
    return link;
  }
}
