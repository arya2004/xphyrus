import { Component, OnInit } from '@angular/core';
import { TeacherService } from '../teacher.service';
import { IAssesmentHome } from 'src/app/shared/models/IAssesmentAdminHome';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
  
  constructor(private teacherService: TeacherService){}
  
  myAssingments: IAssesmentHome[] = []

  ngOnInit(): void {
    this.viewDashboard()
  }

  viewDashboard(){
    this.teacherService.getCreated().subscribe({
      next: res => {
        this.myAssingments = res.result
        console.log(this.myAssingments);
        
      }
    })
  }

}
