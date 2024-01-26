import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashboardComponent } from './dashboard/dashboard.component';
  
import { WriterComponent } from './writer/writer.component';
import { BlogRoutingModule } from './blog-routing.module';
import { ReadComponent } from './read/read.component';



@NgModule({
  declarations: [
    DashboardComponent,
 
    WriterComponent,
    ReadComponent
  ],
  imports: [
    CommonModule,
    BlogRoutingModule
  ]
})
export class BlogModule { }
