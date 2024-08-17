import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

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
import { CodeEditorComponent } from './code-editor/code-editor.component';
import { ProfileComponent } from './profile/profile.component';
import { ExamineeRoutingModule } from './examinee-routing.module';

@NgModule({
  declarations: [
    CodeEditorComponent,
    ProfileComponent
  ],
  imports: [
    CommonModule,
    ExamineeRoutingModule,
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
    SharedModule,
    MatTableModule,
    MatSortModule
  ]
})
export class ExamineeModule { }
