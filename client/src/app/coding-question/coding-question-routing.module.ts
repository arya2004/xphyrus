import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { CodingQuestionCreateComponent } from './coding-question-create/coding-question-create.component';
import { CodingQuestionDashboardComponent } from './coding-question-dashboard/coding-question-dashboard.component';



const routes: Routes = [

  {path: 'new', component: CodingQuestionCreateComponent},
  {path: ':questionId', component: CodingQuestionDashboardComponent},

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
export class CodingQuestionRoutingModule { }
