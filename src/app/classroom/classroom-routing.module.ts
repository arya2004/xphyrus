import { NgModule, createComponent } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashboardComponent } from './dashboard/dashboard.component';
import { CreateComponent } from './create/create.component';
import { DetailComponent } from './detail/detail.component';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {path: '', component: DashboardComponent},
  {path: 'create', component: CreateComponent},
  {path: ':id', component: DetailComponent},

  {path: ':id/assignment', loadChildren: () => import('../assignment/assignment.module').then(m => m.AssignmentModule)},
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
export class ClassroomRoutingModule { }
