import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashboardComponent } from './dashboard/dashboard.component';
import { DetailComponent } from './detail/detail.component';
import { DataTablesModule } from 'angular-datatables';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NexusRoutingModule } from './nexus-routing.module';
import { CoreModule } from '../core/core.module';



@NgModule({
  declarations: [
    DashboardComponent,
    DetailComponent
  ],
  imports: [
    CommonModule,
    DataTablesModule,
    RouterModule,
    FormsModule,
    ReactiveFormsModule,
    NexusRoutingModule,
    CoreModule
  
  ]
})
export class NexusModule { }
