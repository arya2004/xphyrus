import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { RouterModule, Routes } from '@angular/router';
import { DataTablesModule } from 'angular-datatables';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CoreModule } from 'src/app/core/core.module';
import { DashboardComponent } from './dashboard.component';

const routes: Routes = [

  {path: '', component: DashboardComponent},

]



@NgModule({
  declarations: [ 
    DashboardComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    DataTablesModule,
    RouterModule,
    FormsModule,
    ReactiveFormsModule,

    CoreModule
  ],
  exports :[
    RouterModule
  ]
})
export class DashboardModule { }
