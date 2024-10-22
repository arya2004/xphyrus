import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';


import { AuthGuard } from './core/guards/auth.guard';

import { HasRoleGuard } from './core/guards/has-role.guard';

import { NotFoundComponent } from './core/not-found/not-found.component';


const routes: Routes = [
  {path: '', loadChildren: () => import('./home/home.module').then(m => m.HomeModule)},
  {path: 'classroom', loadChildren: () => import('./classroom/classroom.module').then(m => m.ClassroomModule)},
  {path: 'classroom/:classroomId/test', loadChildren: () => import('./assessment/assessment.module').then(m => m.AssessmentModule)},
  {path: 'classroom/:classroomId/test/:testId/question', loadChildren: () => import('./coding-question/coding-question.module').then(m => m.CodingQuestionModule)},
  {path: 'account', loadChildren: () => import('./account/account.module').then(m => m.AccountModule)},
  {path: 'student', loadChildren: () => import('./student/student.module').then(m => m.StudentModule)},
  {path: 'exam', loadChildren: () => import('./exam/exam.module').then(m => m.ExamModule)},
 
  {path: 'prot',canActivate:[AuthGuard], loadChildren: () => import('./exam/exam.module').then(m => m.ExamModule)},
  {path: 'cat',canActivate:[ HasRoleGuard],loadChildren: () => import('./exam/exam.module').then(m => m.ExamModule)},
  { path: '**', component: NotFoundComponent },
  

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
