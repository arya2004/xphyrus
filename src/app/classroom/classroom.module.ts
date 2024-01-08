import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashboardComponent } from './dashboard/dashboard.component';

import { DetailComponent } from './detail/detail.component';
import { ClassroomRoutingModule } from './classroom-routing.module';



@NgModule({
  declarations: [
    DashboardComponent,
  
    DetailComponent
  ],
  imports: [
    CommonModule,
    ClassroomRoutingModule
  ]
})
export class ClassroomModule { }
