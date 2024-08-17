import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './dashboard/dashboard.component';
import { DetailComponent } from './detail/detail.component';


const routes: Routes = [
  {path: '', component: DashboardComponent},
  {path: ':id', component: DetailComponent},
  
  {path: ':id/assignment', loadChildren: () => import('../assessment/assessment.module').then(m => m.AssessmentModule)},


  
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
export class NexusRoutingModule { }
