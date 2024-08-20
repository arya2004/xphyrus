import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { AssessmentDashboardComponent } from './assessment-dashboard/assessment-dashboard.component';



const routes: Routes = [

  {path: ':testId', component: AssessmentDashboardComponent},
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
export class AssessmentRoutingModule { }
