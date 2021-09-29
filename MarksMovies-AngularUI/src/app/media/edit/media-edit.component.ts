import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IMedia } from '../media';
import { MediaEditService } from './media-edit.service';

@Component({
  templateUrl: './media-edit.component.html',
  styleUrls: ['./media-edit.component.css']
})
export class MediaEditComponent implements OnInit {

  constructor(private route: ActivatedRoute,
              private editService: MediaEditService) { }

  media: IMedia;
  id: number;
  errorMessage: string;

  ngOnInit() {
    this.id = +this.route.snapshot.paramMap.get('id');

    this.editService.getMediaByID(this.id).subscribe({
      next: media => {
        this.media = media;
      },
      error: err => this.errorMessage = err
    });
  }

}
