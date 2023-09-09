import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MyassesmentComponent } from './myassesment.component';
import { RouterModule } from '@angular/router';



@NgModule({
  declarations: [
    MyassesmentComponent
  ],
  imports: [
    CommonModule,
    RouterModule
  ],
  exports:[
    MyassesmentComponent
  ]
})
export class MyassesmentModule { }
