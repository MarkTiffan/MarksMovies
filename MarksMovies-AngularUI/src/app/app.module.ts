import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { AboutComponent } from './about/about.component';
import { MediaModule } from './media/media.module';
import {UrlSerializer} from '@angular/router';
import {CustomUrlSerializer} from '../app/shared/common';


@NgModule({
  declarations: [
    AppComponent,
    AboutComponent
  ],
  providers: [{ provide: UrlSerializer, useClass: CustomUrlSerializer }],
  imports: [
    BrowserModule,
    HttpClientModule,
    RouterModule.forRoot([
      { path: 'about', component: AboutComponent },
      { path: '', redirectTo: 'about', pathMatch: 'full'},
      { path: '**', redirectTo: 'about', pathMatch: 'full'}
    ],
    {
      scrollPositionRestoration: 'enabled',
      anchorScrolling: 'enabled'
    }),
    MediaModule
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
