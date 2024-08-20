import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CodeEditorComponent } from './code-editor/code-editor.component';
import { ExamRoutingModule } from './exam-routing.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AngularEditorModule } from '@kolkov/angular-editor';
import { MonacoEditorModule } from 'ngx-monaco-editor-v2';
import { SharedModule } from '../shared/shared.module';
import { ExamDashboardComponent } from './exam-dashboard/exam-dashboard.component';
import { MarkdownModule } from 'ngx-markdown';
import { CoreModule } from "../core/core.module";



@NgModule({
    declarations: [
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
        MarkdownModule.forRoot(),
        CoreModule
    ]
})
export class ExamModule { }
