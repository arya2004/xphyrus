import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './dashboard/dashboard.component';

import { WriterComponent } from './writer/writer.component';
import { ReadComponent } from './read/read.component';

const routes: Routes = [
  {path: '', component: DashboardComponent},
  {path: 'write', component: WriterComponent},
  {path: ':id', component: ReadComponent},

 
]

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forChild(routes)
  ]
})
export class BlogRoutingModule { }
