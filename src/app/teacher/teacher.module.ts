import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashboardComponent } from './dashboard/dashboard.component';
import { AssesmentComponent } from './assesment/assesment.component';
import { NewComponent } from './new/new.component';
import { TeacherRoutingModule } from './teacher-routing.module';
import { MatCardModule } from '@angular/material/card';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    DashboardComponent,
    AssesmentComponent,
    NewComponent
  ],
  imports: [
    CommonModule,
    TeacherRoutingModule,
    MatButtonModule, 
    MatCheckboxModule,
    MatInputModule,
    MatCardModule,
    FormsModule, 
    ReactiveFormsModule,

  ],
  exports:
  [
    AssesmentComponent,
    DashboardComponent,
    NewComponent
  ]
})
export class TeacherModule { }
