import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { CreateComponent } from './create/create.component';
import { EditorComponent } from './editor/editor.component';
import { IndexComponent } from './index/index.component';

const routes: Routes = [
  {path: '', component: IndexComponent},
  {path: 'create', component: CreateComponent},
  {path: 'potd', component: EditorComponent},
  {path: ':id/solve', component: EditorComponent}
 
]

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forChild(routes)
  ]
})
export class ProblemsRoutingModule { }
