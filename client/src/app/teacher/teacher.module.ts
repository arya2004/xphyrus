import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashboardComponent } from './dashboard/dashboard.component';
import { ClassroomComponent } from './classroom/classroom.component';
import { TestCreateComponent } from './test-create/test-create.component';
import { TestDetailComponent } from './test-detail/test-detail.component';



@NgModule({
  declarations: [
    DashboardComponent,
    ClassroomComponent,
    TestCreateComponent,
    TestDetailComponent
  ],
  imports: [
    CommonModule
  ]
})
export class TeacherModule { }
