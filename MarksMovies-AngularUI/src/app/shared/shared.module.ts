import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EnumSelectPipe } from './enumselectpipe';
import { FormsModule } from '@angular/forms';


@NgModule({
  declarations: [EnumSelectPipe],
  imports: [
    CommonModule,
    FormsModule
  ],
  exports: [EnumSelectPipe]
})
export class SharedModule { }
