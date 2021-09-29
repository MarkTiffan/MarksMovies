import { Component, OnInit } from '@angular/core';
import { MediaService } from './media.service';
import { IMedia } from './media';
import { IGenre, GenreType } from './genre';
import { CommonFunctions } from '../shared/common';
import {Router, Scroll, NavigationEnd} from '@angular/router'; 
import { ViewportScroller } from '@angular/common';
import { filter } from 'rxjs/operators';

@Component({
  templateUrl: './media-list.component.html',
  styleUrls: ['./media-list.component.css'],
  providers: [MediaService]
})
export class MediaListComponent implements OnInit {
  errorMessage: string;
 
  constructor(private mediaService: MediaService,
        private CF: CommonFunctions,
        private router: Router,
        private viewportScroller: ViewportScroller) {

        }
  genreFilter: string = "";
  titleFilter: string = "";
  

  mediaList: IMedia[] = [];
  public genreList = GenreType;

  ngOnInit(): void {
    console.log('In OnInit');

    if(this.genreFilter == ""){
      this.genreFilter = "0"
    }
    this.FilterMediaList();
  }



  FilterMediaList(){
    this.mediaService.getMedia(this.genreFilter,this.titleFilter).subscribe({
        next: media => {
          this.mediaList = media;
          this.router.events.pipe(filter(e => e instanceof Scroll)).subscribe((e: any) => {
            console.log(e);
      
            // this is fix for dynamic generated(loaded..?) content
            setTimeout(() => {
              if (e.position) {
                console.log("scrolling to position");
                this.viewportScroller.scrollToPosition(e.position);
              } else if (e.anchor) {
                console.log("scrolling to anchor [" + e.anchor + "]");
                this.viewportScroller.scrollToAnchor(e.anchor);
              } else {
                console.log("scrolling to 0, 0");
                this.viewportScroller.scrollToPosition([0, 0]);
              }
            },1500);
          });
        },
        error: err => this.errorMessage = err
    });
  }
}
