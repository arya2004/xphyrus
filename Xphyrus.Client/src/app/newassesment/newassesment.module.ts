import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NewassesmentComponent } from './newassesment.component';
import { ReactiveFormsModule } from '@angular/forms';



@NgModule({
  declarations: [
    NewassesmentComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule
  ],
  exports:[
    NewassesmentComponent
  ]
})
export class NewassesmentModule { }
