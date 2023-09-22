import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './account/login/login.component';
import { RegisterComponent } from './account/register/register.component';
import { NewassesmentComponent } from './newassesment/newassesment.component';
import { AssesmentComponent } from './assesment/assesment.component';
import { MyassesmentComponent } from './myassesment/myassesment.component';
import { AuthGuard } from './core/guards/auth.guard';
import { StartComponent } from './student/start/start.component';
import { PageComponent } from './student/page/page.component';

const routes: Routes = [
  {path: '', component:HomeComponent},
  {path: 'account', loadChildren: () => import('./account/account.module').then(m => m.AccountModule)},


  {path: 'assesment/new' ,component: NewassesmentComponent},
  {path: 'assesment/start', component: AssesmentComponent},
  {path: 'assesment/my', component: MyassesmentComponent},
  {path: 'student/joined' ,component: PageComponent},
  {path: 'student/start', component: StartComponent}
  

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
