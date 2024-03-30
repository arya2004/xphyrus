import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './dashboard/dashboard.component';
import { EditorComponent } from './editor/editor.component';
import { CreateComponent } from './create/create.component';


const routes: Routes = [
  {path: 'new', component: CreateComponent},
  
  {path: ':id', component: DashboardComponent},
 
  {path: ':id/editor', component: CreateComponent},


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
