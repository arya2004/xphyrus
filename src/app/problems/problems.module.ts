import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IndexComponent } from './index/index.component';
import { CreateComponent } from './create/create.component';
import { PotdComponent } from './potd/potd.component';
import { EditorComponent } from './editor/editor.component';
import { ProblemsRoutingModule } from './problems-routing.module';



@NgModule({
  declarations: [
    IndexComponent,
    CreateComponent,
    PotdComponent,
    EditorComponent
  ],
  imports: [
    CommonModule,
    ProblemsRoutingModule
  ]
})
export class ProblemsModule { }
