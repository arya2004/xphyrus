import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CreateComponent } from './create/create.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { AssessmentRoutingModule } from './assessment-routing.module';


import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatInputModule } from '@angular/material/input';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MonacoEditorModule } from 'ngx-monaco-editor-v2';
import { AngularEditorModule } from '@kolkov/angular-editor';
import { MatIconModule } from '@angular/material/icon';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatRadioModule } from '@angular/material/radio';


import { SharedModule } from '../shared/shared.module';
import { MatTableModule } from '@angular/material/table';
import { MatSortModule } from '@angular/material/sort';
import { CoreModule } from '../core/core.module';


@NgModule({
  declarations: [
    CreateComponent,
    DashboardComponent
  ],
  imports: [
    CommonModule,
    AssessmentRoutingModule,
    MatButtonModule, 
    MatCheckboxModule,
    MatInputModule,
    MatCardModule,
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
    MatSortModule
  ]
})
export class AssessmentModule { }
