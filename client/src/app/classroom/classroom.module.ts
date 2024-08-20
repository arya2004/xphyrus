import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ClassroomDashboardComponent } from './classroom-dashboard/classroom-dashboard.component';
import { DataTablesModule } from 'angular-datatables';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CoreModule } from '../core/core.module';

import { SharedModule } from '../shared/shared.module';
import { ClassroomRoutingModule } from './classroom-routing.module';
import { ClassroomDetailComponent } from './classroom-detail/classroom-detail.component';



@NgModule({
  declarations: [
    ClassroomDashboardComponent,
    ClassroomDetailComponent
  ],
  imports: [
    CommonModule,
    ClassroomRoutingModule,
    DataTablesModule,
    RouterModule,
    FormsModule,
    ReactiveFormsModule,
    CoreModule,
    SharedModule,
  
  ]
})
export class ClassroomModule { }
