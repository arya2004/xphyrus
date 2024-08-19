import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashboardComponent } from './dashboard/dashboard.component';
import { TestRoutingModule } from '../test/test-routing.module';
import { CoreModule } from 'src/app/core/core.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { DataTablesModule } from 'angular-datatables';
import { CodingQuestionRoutingModule } from './coding-question-routing.module';
import { NewComponent } from './new/new.component';
import { AngularEditorModule } from '@kolkov/angular-editor';
import { MonacoEditorModule } from 'ngx-monaco-editor-v2';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatSelectModule } from '@angular/material/select';
import { MatIconModule } from '@angular/material/icon';
import { MatRadioModule } from '@angular/material/radio';
import { SharedModule } from 'src/app/shared/shared.module';
import { MatTableModule } from '@angular/material/table';



@NgModule({
  declarations: [
    DashboardComponent,
    NewComponent
  ],
  imports: [
    CommonModule,
    CodingQuestionRoutingModule,
    DataTablesModule,
    RouterModule,
    FormsModule,
    ReactiveFormsModule,

 

    AngularEditorModule,
    MonacoEditorModule.forRoot(),
    MatDatepickerModule,
    AngularEditorModule,
    MatSelectModule,
    MatIconModule,
    MatRadioModule,
   CoreModule,

  
    SharedModule,
    MatTableModule,

    DataTablesModule
  ]
})
export class CodingQuestionModule { }
