import { Component, OnInit } from '@angular/core';
import { MediaCreateService } from './media-create.service';
import { IMedia, MovieOrTVShow, DiscType, Rating } from '../media';
import { IGenre, GenreType } from '../genre';
import { ISearchMovies } from '../searchmovies';
import { ISearchTV } from '../searchtv';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { EnumSelectPipe } from 'src/app/shared/enumselectpipe';
import { Router } from '@angular/router';


@Component({
  selector: 'app-media-create',
  templateUrl: './media-create.component.html',
  styleUrls: ['./media-create.component.css']
})
export class MediaCreateComponent implements OnInit {

  constructor(private createService: MediaCreateService,
          private router: Router) {
      this.media = {
        'title': "",
        'id': 0,
        'imdB_id': null,
        'comments': null,
        'genres': [],
        'mediaType': DiscType.DVD,
        'movieOrTVShow': MovieOrTVShow.Movie,
        'rank': 0,
        'rating': Rating.G,
        'season': 0,
        'tmdB_ID': 0,
        'year': 1900
      };

  }
  
  media: IMedia;
  SelectedGenres: IGenre[];
  SearchMovies: ISearchMovies;
  SearchTV: ISearchTV;
  mediaOptions = MovieOrTVShow;
  genreList = GenreType;
  discOptions = DiscType;
  ratingOptions = Rating;
  MovieOrTVShowSelection: MovieOrTVShow;
  RatingSelection: Rating;
  SelectedDiscType: DiscType;
  
  ngOnInit() {

    this.SelectedGenres = this.media.genres.slice(0);
    this.MovieOrTVShowSelection = this.media.movieOrTVShow;
    this.RatingSelection = this.media.rating;
    this.SelectedDiscType = this.media.mediaType;
  }

  onCreate(){
      var newMediaID: number;
      this.media.movieOrTVShow = this.MovieOrTVShowSelection;
      this.media.rating = this.RatingSelection;
      this.media.mediaType = this.SelectedDiscType;
      this.media.genres = this.SelectedGenres.slice(0);
      if(!this.media.imdB_id || this.media.imdB_id.length > 0){
        this.media.imdB_id = null;
      }
      this.createService.Create(this.media)
            .subscribe(result => {
              newMediaID = result;
              this.router.navigate(['/media'],{ fragment: newMediaID.toString()});
            }

      );
  }

  onFetch(){
    console.log("Inside onFetch(), selection = [" + this.MovieOrTVShowSelection + "]");
    if(this.MovieOrTVShowSelection == MovieOrTVShow.Movie){
      console.log("Fetching the movie = [" + this.media.title + "]");
      this.createService.FetchMovieAsync(this.media.title)
            .subscribe(result => {
              this.SearchMovies = result;
            });
    } else if(this.MovieOrTVShowSelection == MovieOrTVShow.TV){
        console.log("Fetching the TV Show = [" + this.media.title + "]");
        this.createService.FetchTVShowsAsync(this.media.title)
            .subscribe(result => {
              this.SearchTV = result;
            });
    }
  }

  onImport(TMDB_ID: number){
    console.log("Inside onImport(), TMDB_ID = [" + TMDB_ID + "]");
    if(TMDB_ID > 0){
      if(this.MovieOrTVShowSelection == MovieOrTVShow.Movie){
        this.createService.ImportMovieAsync(TMDB_ID)
            .subscribe(result => {
                this.media = result;
                this.SelectedGenres = this.media.genres.slice(0);
                this.RatingSelection = this.media.rating;
                this.SelectedDiscType = this.media.mediaType;
            });
      } else if(this.MovieOrTVShowSelection == MovieOrTVShow.TV){
        this.createService.ImportTVShowAsync(TMDB_ID)
            .subscribe(result => {
                this.media = result;
                this.SelectedGenres = this.media.genres.slice(0);
                this.RatingSelection = this.media.rating;
                this.SelectedDiscType = this.media.mediaType;
            });
      }
    }
  }

  compareWithFn(item1: any, item2: IGenre) {
    //console.log("Item1=" + item1 + ", Item2=" + item2.genre)
    return item1 && item2 ? item1 === item2.genre : item1 === item2;
  }
}
