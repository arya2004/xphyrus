import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AssessmentRoutingModule } from './assessment-routing.module';
import { DataTablesModule } from 'angular-datatables';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CoreModule } from '../core/core.module';
import { SharedModule } from '../shared/shared.module';



@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    AssessmentRoutingModule,
    DataTablesModule,
    RouterModule,
    FormsModule,
    ReactiveFormsModule,
    CoreModule,
    SharedModule,
  
  ]
})
export class AssessmentModule { }
