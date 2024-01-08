import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './dashboard/dashboard.component';
import { CreateComponent } from './create/create.component';
import { EditorComponent } from './editor/editor.component';


const routes: Routes = [

  {path: 'create', component: CreateComponent},

  {path: ':id', component: DashboardComponent},
  {path: ':id/editor', component: EditorComponent},

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
