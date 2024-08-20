import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CodingQuestionDashboardComponent } from './coding-question-dashboard/coding-question-dashboard.component';
import { CodingQuestionCreateComponent } from './coding-question-create/coding-question-create.component';
import { CodingQuestionRoutingModule } from './coding-question-routing.module';
import { DataTablesModule } from 'angular-datatables';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AngularEditorModule } from '@kolkov/angular-editor';
import { CoreModule } from '../core/core.module';
import { SharedModule } from '../shared/shared.module';



@NgModule({
  declarations: [
    CodingQuestionDashboardComponent,
    CodingQuestionCreateComponent
  ],
  imports: [
    CommonModule,
    CodingQuestionRoutingModule,
    DataTablesModule,
    RouterModule,
    FormsModule,
    ReactiveFormsModule,
    AngularEditorModule,
    AngularEditorModule,
    CoreModule,
    SharedModule,
    DataTablesModule
  ]
})
export class CodingQuestionModule { }
