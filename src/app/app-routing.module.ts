import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './account/login/login.component';
import { RegisterComponent } from './account/register/register.component';

import { AuthGuard } from './core/guards/auth.guard';

import { HasRoleGuard } from './core/guards/has-role.guard';

import { NotFoundComponent } from './core/not-found/not-found.component';
import { DashboardComponent } from './home/dashboard/dashboard.component';


const routes: Routes = [
  {path: '', component:HomeComponent},
  {path: 'dashboard', component:DashboardComponent},
  {path: 'account', loadChildren: () => import('./account/account.module').then(m => m.AccountModule)},
  {path: 'classroom', loadChildren: () => import('./classroom/classroom.module').then(m => m.ClassroomModule)},
  {path: 'problems', loadChildren: () => import('./problems/problems.module').then(m => m.ProblemsModule)},
  {path: 'blogs', loadChildren: () => import('./blog/blog.module').then(m => m.BlogModule)},
  {path: 'prot',canActivate:[AuthGuard], loadChildren: () => import('./classroom/classroom.module').then(m => m.ClassroomModule)},
  {path: 'cat',canActivate:[ HasRoleGuard],loadChildren: () => import('./classroom/classroom.module').then(m => m.ClassroomModule)},
  { path: '**', component: NotFoundComponent }


];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
