import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { ClassroomComponent } from './classroom/classroom.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { TestCreateComponent } from './test-create/test-create.component';
import { TestDetailComponent } from './test-detail/test-detail.component';

const routes: Routes = [
  {path: 'classroom/:classroomId', component: ClassroomComponent},
  
  {path: 'dashboard', component: DashboardComponent},
  {path: 'classroom/:classroomId/test/create', component: TestCreateComponent},
  
  {path: 'classroom/:classroomId/test/:testId', component: TestDetailComponent}

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
