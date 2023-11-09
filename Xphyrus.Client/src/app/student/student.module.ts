import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { StartComponent } from './start/start.component';
import { PageComponent } from './page/page.component';
import { StudentRoutingModule } from './student-routing.module';



@NgModule({
  declarations: [
  
    StartComponent,
    PageComponent
  ],
  imports: [
    CommonModule,
    StudentRoutingModule
  ],
  exports:[
    StartComponent,
    PageComponent
  ]
})
export class StudentModule { }
