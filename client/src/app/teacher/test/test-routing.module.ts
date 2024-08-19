import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './dashboard/dashboard.component';



const routes: Routes = [

  {path: ':testId', component: DashboardComponent},

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
export class TestRoutingModule { }
