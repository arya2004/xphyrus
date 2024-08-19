import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [

  {path: 'dashboard', loadChildren: () => import('./dashboard/dashboard.module').then(m => m.DashboardModule)},
  {path: 'classroom', loadChildren: () => import('./classroom/classroom.module').then(m => m.ClassroomModule)},
  {path: 'classroom/:classroomId/test', loadChildren: () => import('./test/test.module').then(m => m.TestModule)},
  {path: 'classroom/:classroomId/test/:testId/question', loadChildren: () => import('./coding-question/coding-question.module').then(m => m.CodingQuestionModule)},

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
export class TeacherRoutingModule { }
