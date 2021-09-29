import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MediaDeleteService } from './media-delete.service';
import { IMedia } from '../media';
import { CommonFunctions } from 'src/app/shared/common';


@Component({
  selector: 'app-media-delete',
  templateUrl: './media-delete.component.html',
  styleUrls: ['./media-delete.component.css']
})
export class MediaDeleteComponent implements OnInit {
  id: number;
  media: IMedia;
  errorMessage: string;
  genres: string;

  constructor(private route: ActivatedRoute,
          private deleteService: MediaDeleteService,
          private CF: CommonFunctions,
          private router: Router) { }

  ngOnInit() {
    this.id = +this.route.snapshot.paramMap.get('id');
    console.log("Media ID=" + this.id);
    this.deleteService.getMediaByID(this.id).subscribe({
      next: media => {
        this.media = media;
        this.genres = this.CF.GetGenresAsString(this.media.genres);
      },
      error: err => this.errorMessage = err
    });
  }

  onDelete(id: number){
    console.log("Deleting ID=" + id);
    this.deleteService.deleteMedia(id).subscribe(
        result => {console.log("Item deleted=" + result);
        this.router.navigate(["/media"]);
    });
    
    
  }

}
