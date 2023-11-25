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
import {MatDatepickerModule} from '@angular/material/datepicker';
import { AngularEditorModule } from '@kolkov/angular-editor';
import { MatSelectModule} from '@angular/material/select';
import { MatIconModule } from '@angular/material/icon';
import { SharedModule } from '../shared/shared.module';
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
    MatDatepickerModule,
    AngularEditorModule,
    MatSelectModule,
    MatIconModule,
    SharedModule

  ],
  exports:
  [
    AssesmentComponent,
    DashboardComponent,
    NewComponent
  ]
})
export class TeacherModule { }
