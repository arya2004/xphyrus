import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AssesmentComponent } from './assesment.component';
import { RouterModule } from '@angular/router';



@NgModule({
  declarations: [
    AssesmentComponent
  ],
  imports: [
    CommonModule,
    RouterModule
  ],
  exports:[
    AssesmentComponent
  ]
})
export class AssesmentModule { }
