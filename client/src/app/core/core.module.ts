import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { FooterComponent } from './footer/footer.component';
import { RouterModule } from '@angular/router';
import { NotFoundComponent } from './not-found/not-found.component';
import { ToastrModule } from 'ngx-toastr';
import { SideBarComponent } from './side-bar/side-bar.component';

import { TeacherSideBarComponent } from './teacher-side-bar/teacher-side-bar.component';




@NgModule({
  declarations: [
    NavBarComponent,
    FooterComponent,
    NotFoundComponent,
    SideBarComponent,

    TeacherSideBarComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    ToastrModule.forRoot({
      positionClass: 'toast-top-full-width',
      preventDuplicates: true,
      progressBar: true,
      closeButton: true,
      tapToDismiss: false,
      countDuplicates: true,
    
    })
    
  ],
  exports:[
    NavBarComponent,
    FooterComponent,
    SideBarComponent,
    TeacherSideBarComponent
   
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA] 
})
export class CoreModule { }
