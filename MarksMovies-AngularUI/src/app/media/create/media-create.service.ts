import { Injectable, ChangeDetectionStrategy } from '@angular/core';
import { IMedia } from '../media';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, tap} from 'rxjs/operators';
import { IMovieDetails } from '../moviedetails';
import { ITVShowDetails } from '../tvdetails';
import { GenreType, IGenre } from '../genre';
import { isGeneratedFile } from '@angular/compiler/src/aot/util';
import { ISearchMovies } from '../searchmovies';
import { ISearchTV } from '../searchtv';


@Injectable({
  providedIn: 'root'
})
export class MediaCreateService {
  private mediaUrl = 'http://localhost:3000/api'

  constructor(private http: HttpClient) { }



  Create(media: IMedia): Observable<number>
  {
      let requestUrl = this.mediaUrl + "/media/item";
      
      console.log("Media to CREATE: " + JSON.stringify(media));

      return this.http.post<number>(requestUrl, media)
            .pipe(
              tap(data => console.log('All: ' + JSON.stringify(data))),
              catchError(this.handleError)
            );
  }

  FetchMovieAsync(searchTitle: string) : Observable<ISearchMovies>{
    var Url = this.mediaUrl + "/movie/fetch?title=" + searchTitle;
    console.log("Fetch Movie URL = [" + Url + "]");
    return this.http.get<ISearchMovies>(Url)
        .pipe(
          tap(data => console.log('All: ' + JSON.stringify(data))),
          catchError(this.handleError)
        );
  }

  FetchTVShowsAsync(searchTitle: string): Observable<ISearchTV>{
    var Url = this.mediaUrl + "/tvshow/fetch?title=" + searchTitle;
    console.log("Fetch Tv Show URL = [" + Url + "]");
    return this.http.get<ISearchTV>(Url)
        .pipe(
          tap(data => console.log('All: ' + JSON.stringify(data))),
          catchError(this.handleError)
        );
  }

  ImportMovieAsync(TMDB_ID: number): Observable<IMedia>
  {
      var requestUrl = this.mediaUrl + "/Movie/import?TMDB_ID=" + 
          TMDB_ID;
      console.log("Import Movie URL = [" + requestUrl + "]");
      return this.http.get<IMedia>(requestUrl)
          .pipe(
            tap(data => console.log('All: ' + JSON.stringify(data))),
            catchError(this.handleError)
          );
  }


  ImportTVShowAsync(TMDB_ID: number): Observable<IMedia>
  {
      var requestUrl = this.mediaUrl + "/tvshow/import?TMDB_ID=" + 
          TMDB_ID;
      console.log("Import TV Show URL = [" + requestUrl + "]");
      return this.http.get<IMedia>(requestUrl)
          .pipe(
            tap(data => console.log('All: ' + JSON.stringify(data))),
            catchError(this.handleError)
          );
  }



  private handleError(err: HttpErrorResponse){
    let errorMessage = '';
    if (err.error instanceof ErrorEvent){
        errorMessage = `An error occurred: ${err.error.message}`;
    } else {
        errorMessage = `Server returned code: ${err.status}, error message is: ${err.message}`;
    }
    console.error(errorMessage);
    return throwError(errorMessage);
}
}