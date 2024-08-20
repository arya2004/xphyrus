import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { ExamService } from 'src/app/exam/exam.service';
import { Exam } from 'src/app/shared/examSidebar';
import { CoreService } from '../core.service';

@Component({
  selector: 'app-exam-side-bar',
  templateUrl: './exam-side-bar.component.html',
  styleUrls: ['./exam-side-bar.component.scss']
})
export class ExamSideBarComponent implements OnInit {

  exam$: Observable<Exam>;

  constructor(private examService: CoreService) { }

  ngOnInit(): void {
    this.exam$ = this.examService.getExamData();
  }

  generateRouterLink(examId: number, questionId: number): string {
    return `/exam/${examId}/q/${questionId}`;
  }
}
