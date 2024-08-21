import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { StudentDashboardComponent } from './student-dashboard/student-dashboard.component';
import { ResultComponent } from './result/result.component';



const routes: Routes = [
  
  {path: '', component: StudentDashboardComponent},
  {path: 'result/:testId', component: ResultComponent},

]

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forChild(routes)
  ],
  exports :[
    RouterModule
  ]
})
export class StudentRoutingModule { }
