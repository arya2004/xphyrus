import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ExamSideBarComponent } from './exam-side-bar/exam-side-bar.component';
import { CodeEditorComponent } from './code-editor/code-editor.component';
import { ExamRoutingModule } from './exam-routing.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AngularEditorModule } from '@kolkov/angular-editor';
import { MonacoEditorModule } from 'ngx-monaco-editor-v2';
import { SharedModule } from '../shared/shared.module';
import { ExamDashboardComponent } from './exam-dashboard/exam-dashboard.component';
import { MarkdownModule } from 'ngx-markdown';



@NgModule({
  declarations: [
    ExamSideBarComponent,
    CodeEditorComponent,
    ExamDashboardComponent
  ],
  imports: [
    CommonModule,
    ExamRoutingModule,
    FormsModule, 
    ReactiveFormsModule,
    AngularEditorModule,
    MonacoEditorModule.forRoot(),
    AngularEditorModule,
    SharedModule,
    MarkdownModule.forRoot()

  ]
})
export class ExamModule { }
