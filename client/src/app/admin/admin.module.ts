import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UsersComponent } from './users/users.component';
import { SettingsComponent } from './settings/settings.component';



@NgModule({
  declarations: [
    UsersComponent,
    SettingsComponent
  ],
  imports: [
    CommonModule
  ]
})
export class AdminModule { }
