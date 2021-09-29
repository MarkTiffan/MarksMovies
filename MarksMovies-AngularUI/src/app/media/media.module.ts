import { NgModule } from '@angular/core';
import { MediaListComponent } from './media-list.component';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../shared/shared.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MediaEditComponent } from './edit/media-edit.component';
import { MediaEditGuard } from './edit/media-edit.guard';
import { MediaDetailsComponent } from './details/media-details.component';
import { MediaDetailsGuard } from './details/media-details.guard';
import { MediaCreateComponent } from './create/media-create.component';
import { MediaDeleteComponent } from './delete/media-delete.component';
import { MediaDeleteGuard } from './delete/media-delete.guard';


@NgModule({
  declarations: [
        MediaListComponent, 
        MediaEditComponent, 
        MediaDetailsComponent, 
        MediaCreateComponent, 
        MediaDeleteComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forChild([
      { path: 'media', component: MediaListComponent },
      { path: 'media/edit/:id',
          canActivate: [MediaEditGuard],
          component: MediaEditComponent },
      { path: 'media/details/:id',
          canActivate: [MediaDetailsGuard],
          component: MediaDetailsComponent },
      { path: 'media/create', component: MediaCreateComponent },
      { path: 'media/delete/:id',
          canActivate: [MediaDeleteGuard],
          component: MediaDeleteComponent}
    ])
  ]
})
export class MediaModule { 


}
