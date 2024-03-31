import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { ProfileComponent } from './profile/profile.component';
import { CodeEditorComponent } from './code-editor/code-editor.component';


const routes: Routes = [
 
  
  {path: ':id', component: ProfileComponent},
 
  {path: ':id/editor', component: CodeEditorComponent},


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
export class ExamineeRoutingModule { }
