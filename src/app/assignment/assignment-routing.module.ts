import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './dashboard/dashboard.component';
import { CreateComponent } from './create/create.component';
import { EditorComponent } from 'ngx-monaco-editor-v2';

const routes: Routes = [

  {path: 'create', component: CreateComponent},
  {path: 'editor', component: EditorComponent},
  {path: ':id', component: DashboardComponent}

]

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forChild(routes)
  ],
  exports :[
    RouterModule
  ]
})
export class AssignmentRoutingModule { }
