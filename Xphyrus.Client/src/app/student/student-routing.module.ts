import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { PageComponent } from './page/page.component';
import { StartComponent } from './start/start.component';

const routes: Routes = [
  {path: '', component: PageComponent},
  {path: ':id', component: StartComponent}
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
export class StudentRoutingModule { }
