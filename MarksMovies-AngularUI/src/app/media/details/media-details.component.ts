import { Component, OnInit } from '@angular/core';
import { IMedia, MovieOrTVShow, DiscType, Rating } from '../media';
import { ActivatedRoute } from '@angular/router';
import { MediaDetailsService } from './media-details.service';
import { IMovieDetails } from '../moviedetails';
import { ITVShowDetails } from '../tvdetails';
import { CommonFunctions } from 'src/app/shared/common';


@Component({
  selector: 'app-media-details',
  templateUrl: './media-details.component.html',
  styleUrls: ['./media-details.component.css']
})
export class MediaDetailsComponent implements OnInit {

  constructor(private route: ActivatedRoute,
    private detailsService: MediaDetailsService,
    private CF: CommonFunctions) { }

  media: IMedia;
  id: number;
  errorMessage: string;
  MovieDetails: IMovieDetails;
  TVDetails: ITVShowDetails;
  PosterURL: string;
  Overview: string;
  Tagline: string;
  Runtime: number;
  SeasonCount: number;
  Movie: MovieOrTVShow = MovieOrTVShow.Movie;

  ngOnInit() 
  {
    this.id = +this.route.snapshot.paramMap.get('id');

    this.detailsService.getMediaByID(this.id).subscribe({
      next: media => {
        this.media = media;
        
        // console.log("media.id = [" + this.media.id + "]");
        
        if (this.media != null && this.media.tmdB_ID > 0){
          
          // console.log("TMDB_ID = [" + this.media.tmdB_ID + "]");
          // console.log("movieOrTvShow = [" + this.media.movieOrTVShow + "]");
          if (this.media.movieOrTVShow == MovieOrTVShow.Movie)
          {
            // console.log("This is a movie");
            this.detailsService.GetMovieDetailsAsync(this.media.tmdB_ID)
                      .subscribe({
                        next: md => {
                            this.MovieDetails = md;
                            // console.log("MovieDetails = [" + this.MovieDetails + "]");
                            if (this.MovieDetails != null)
                            {
                              this.PosterURL = "https://image.tmdb.org/t/p/w185" + this.MovieDetails.poster_path;
                              this.Overview = this.MovieDetails.overview;
                              this.Tagline =this.MovieDetails.tagline;
                              this.Runtime = this.MovieDetails.runtime;
                            }
                        },
                        error: err => this.errorMessage = err
                      });
          } else if (this.media.movieOrTVShow == MovieOrTVShow.TV)
          {
              this.detailsService.GetTVSHowDetailsAsync(this.media.tmdB_ID)
                      .subscribe({
                        next: td => {
                          this.TVDetails = td;
                          // console.log("TVDetails = [" + this.TVDetails + "]");
                          if (this.TVDetails != null){              
                            this.Overview = this.TVDetails.overview;
                            this.PosterURL = "https://image.tmdb.org/t/p/w185" + this.TVDetails.poster_path;
                
                            for (let season of this.TVDetails.seasons)
                            {
                                if (season.season_number == this.media.season)
                                {
                                    if(season.poster_path != "")
                                        this.PosterURL = "https://image.tmdb.org/t/p/w185" + season.poster_path;
                                    if(season.overview != "")
                                        this.Overview = season.overview;
                                    break;
                                }
                            }
                
                            this.Tagline = "";
                
                            this.SeasonCount = this.TVDetails.number_of_seasons;
                
                            if (this.TVDetails.episode_run_time.length > 0)
                                this.Runtime = this.TVDetails.episode_run_time[0];
                
                          }
                        },
                        error: err => this.errorMessage = err
                      });
          }
          
        }
      },
      error: err => this.errorMessage = err
    });
    
    
  }
}
