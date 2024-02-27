import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashboardComponent } from './dashboard/dashboard.component';

import { DetailComponent } from './detail/detail.component';
import { SyndicateRoutingModule } from './syndicate-routing.module';



@NgModule({
  declarations: [
    DashboardComponent,
  
    DetailComponent
  ],
  imports: [
    CommonModule,
    SyndicateRoutingModule
  ]
})
export class SyndicateModule { }
