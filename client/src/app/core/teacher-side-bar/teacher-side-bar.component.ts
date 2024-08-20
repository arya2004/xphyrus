import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Classroom } from 'src/app/shared/teacherSidebar';
import { CoreService } from '../core.service';

@Component({
  selector: 'app-teacher-side-bar',
  templateUrl: './teacher-side-bar.component.html',
  styleUrls: ['./teacher-side-bar.component.scss']
})
export class TeacherSideBarComponent implements OnInit {
  public sidebarItems$: Observable<Classroom[]>;

  constructor(private sidebarService: CoreService) { }

  ngOnInit(): void {
    this.sidebarItems$ = this.sidebarService.getSidebarData();
  }

  // Function to generate routerLink dynamically
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
