import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashboardComponent } from './dashboard/dashboard.component';
import { TestRoutingModule } from './test-routing.module';
import { DataTablesModule } from 'angular-datatables';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CoreModule } from 'src/app/core/core.module';



@NgModule({
  declarations: [
    DashboardComponent
  ],
  imports: [
    CommonModule,
    TestRoutingModule,
    DataTablesModule,
    RouterModule,
    FormsModule,
    ReactiveFormsModule,

    CoreModule
  ]
})
export class TestModule { }
