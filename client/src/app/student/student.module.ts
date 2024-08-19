import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StudentDashboardComponent } from './student-dashboard/student-dashboard.component';
import { ResultComponent } from './result/result.component';

import { StudentSideBarComponent } from './student-side-bar/student-side-bar.component';
import { DataTablesModule } from 'angular-datatables';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CoreModule } from '../core/core.module';
import { StudentRoutingModule } from './student-routing.module';



@NgModule({
  declarations: [
    StudentDashboardComponent,
    ResultComponent,
    StudentSideBarComponent
  ],
  imports: [
    CommonModule,
    StudentRoutingModule,
    DataTablesModule,
    RouterModule,
    FormsModule,
    ReactiveFormsModule,

    CoreModule
  ]
})
export class StudentModule { }
