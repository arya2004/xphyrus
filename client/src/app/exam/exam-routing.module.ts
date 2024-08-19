import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { CodeEditorComponent } from './code-editor/code-editor.component';
import { ExamDashboardComponent } from './exam-dashboard/exam-dashboard.component';



const routes: Routes = [
  
  {path: ':testId', component: ExamDashboardComponent},
  {path: ':testId/q/:questionId', component: CodeEditorComponent},

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
export class ExamRoutingModule { }
