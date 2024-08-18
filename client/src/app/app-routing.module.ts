import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';


import { AuthGuard } from './core/guards/auth.guard';

import { HasRoleGuard } from './core/guards/has-role.guard';

import { NotFoundComponent } from './core/not-found/not-found.component';


const routes: Routes = [
  {path: '', loadChildren: () => import('./home/home.module').then(m => m.HomeModule)},
 
  {path: 'account', loadChildren: () => import('./account/account.module').then(m => m.AccountModule)},
  {path: 'a', loadChildren: () => import('./examinee/examinee.module').then(m => m.ExamineeModule)},
  {path: 'teacher', loadChildren: () => import('./teacher/teacher.module').then(m => m.TeacherModule)},
  {path: 'Syndicate', loadChildren: () => import('./nexus/nexus.module').then(m => m.NexusModule)},
  {path: 'prot',canActivate:[AuthGuard], loadChildren: () => import('./nexus/nexus.module').then(m => m.NexusModule)},
  {path: 'cat',canActivate:[ HasRoleGuard],loadChildren: () => import('./nexus/nexus.module').then(m => m.NexusModule)},
  { path: '**', component: NotFoundComponent },
  

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
