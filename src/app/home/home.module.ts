import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeComponent } from './home.component';
import { RouterModule } from '@angular/router';

import { DashboardComponent } from './dashboard/dashboard.component';



@NgModule({
  declarations: [
    HomeComponent,
  
    DashboardComponent
  ],
  imports: [
    CommonModule,
    RouterModule
  ],
  exports:[
    HomeComponent
  ]
})
export class HomeModule { }
