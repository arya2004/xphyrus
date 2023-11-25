import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { StartComponent } from './start/start.component';
import { PageComponent } from './page/page.component';
import { StudentRoutingModule } from './student-routing.module';
import { MatInputModule } from '@angular/material/input';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatButtonModule } from '@angular/material/button';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MonacoEditorModule } from 'ngx-monaco-editor-v2';

import { AngularEditorModule } from '@kolkov/angular-editor';
import { MatIconModule } from '@angular/material/icon';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';

@NgModule({
  declarations: [
  
    StartComponent,
    PageComponent
  ],
  imports: [
    CommonModule,
    StudentRoutingModule,
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
    MatIconModule
  ],
  exports:[
    StartComponent,
    PageComponent
  ]
})
export class StudentModule { }
