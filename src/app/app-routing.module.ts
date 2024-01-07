import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './account/login/login.component';
import { RegisterComponent } from './account/register/register.component';

import { AuthGuard } from './core/guards/auth.guard';
import { StartComponent } from './student/start/start.component';
import { PageComponent } from './student/page/page.component';


const routes: Routes = [
  {path: '', component:HomeComponent},
  {path: 'account', loadChildren: () => import('./account/account.module').then(m => m.AccountModule)},
  {path: 'teacher', loadChildren: () => import('./teacher/teacher.module').then(m => m.TeacherModule)},
  {path: 'student', loadChildren: () => import('./student/student.module').then(m => m.StudentModule)},
  {path: 'classroom', loadChildren: () => import('./classroom/classroom.module').then(m => m.ClassroomModule)},



];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
