import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './dashboard/dashboard.component';
import { AssesmentComponent } from './assesment/assesment.component';
import { NewComponent } from './new/new.component';

const routes: Routes = [
  {path: '', component: DashboardComponent},
  {path: 'new', component: NewComponent},
  {path: ':id', component: AssesmentComponent}
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
