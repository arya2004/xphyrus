import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { FooterComponent } from './footer/footer.component';
import { RouterModule } from '@angular/router';
import { NotFoundComponent } from './not-found/not-found.component';
import { ToastrModule } from 'ngx-toastr';

import { TeacherSideBarComponent } from './teacher-side-bar/teacher-side-bar.component';
import { ExamSideBarComponent } from './exam-side-bar/exam-side-bar.component';




@NgModule({
  declarations: [
    NavBarComponent,
    FooterComponent,
    NotFoundComponent,
    TeacherSideBarComponent,
      ExamSideBarComponent
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
    TeacherSideBarComponent,
    ExamSideBarComponent
   
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA] 
})
export class CoreModule { }
