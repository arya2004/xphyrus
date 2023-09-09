import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './account/login/login.component';
import { RegisterComponent } from './account/register/register.component';
import { NewassesmentComponent } from './newassesment/newassesment.component';
import { AssesmentComponent } from './assesment/assesment.component';
import { MyassesmentComponent } from './myassesment/myassesment.component';

const routes: Routes = [
  {path: '', component:HomeComponent},
  {path: 'account/login', component:LoginComponent},
  {path: 'account/register', component: RegisterComponent},
  {path: 'account/register', component: RegisterComponent},
  {path: 'assesment/new', component: NewassesmentComponent},
  {path: 'assesment/start', component: AssesmentComponent},
  {path: 'assesment/my', component: MyassesmentComponent},

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
