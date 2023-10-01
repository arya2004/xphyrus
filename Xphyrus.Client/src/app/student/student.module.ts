import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { StartComponent } from './start/start.component';
import { PageComponent } from './page/page.component';



@NgModule({
  declarations: [
  
    StartComponent,
    PageComponent
  ],
  imports: [
    CommonModule
  ],
  exports:[
    StartComponent,
    PageComponent
  ]
})
export class StudentModule { }
