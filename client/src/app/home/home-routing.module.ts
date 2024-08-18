import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { HomePageComponent } from './home-page/home-page.component';
import { HelpPageComponent } from './help-page/help-page.component';


const routes: Routes = [
 
  {path: '', component: HomePageComponent},
  {path: 'help', component: HelpPageComponent},


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
export class HomeRoutingModule { }
