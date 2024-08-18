import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashboardComponent } from './dashboard/dashboard.component';
import { TestComponent } from './test/test.component';
import { ResultComponent } from './result/result.component';



@NgModule({
  declarations: [
    DashboardComponent,
    TestComponent,
    ResultComponent
  ],
  imports: [
    CommonModule
  ]
})
export class StudentModule { }
