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
    MonacoEditorModule.forRoot(),
  ],
  exports:[
    StartComponent,
    PageComponent
  ]
})
export class StudentModule { }
