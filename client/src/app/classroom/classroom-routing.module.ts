import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { ClassroomDashboardComponent } from './classroom-dashboard/classroom-dashboard.component';
import { ClassroomDetailComponent } from './classroom-detail/classroom-detail.component';

const routes: Routes = [

  {path: 'dashboard', component: ClassroomDashboardComponent},
  {path: ':classroomId', component: ClassroomDetailComponent},
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
