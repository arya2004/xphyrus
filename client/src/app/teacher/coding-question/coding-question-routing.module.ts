import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './dashboard/dashboard.component';
import { NewComponent } from './new/new.component';



const routes: Routes = [

  {path: 'new', component: NewComponent},
  {path: ':questionId', component: DashboardComponent},

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
export class CodingQuestionRoutingModule { }
