import { Injectable, ChangeDetectionStrategy } from '@angular/core';
import { IMedia } from '../media';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, tap} from 'rxjs/operators';
import { IMovieDetails } from '../moviedetails';
import { ITVShowDetails } from '../tvdetails';


@Injectable({
  providedIn: 'root'
})
export class MediaDetailsService {
  private mediaUrl = 'http://localhost:3000/api'

  constructor(private http: HttpClient) { }


  getMediaByID(mediaID: number): Observable<IMedia>{
    var Url = this.mediaUrl + "/media/item/";
    Url += mediaID;

    return this.http.get<IMedia>(Url)
        .pipe(
          tap(data => console.log('All: ' + JSON.stringify(data))),
          catchError(this.handleError)
        );
  }

  GetMovieDetailsAsync(TMDB_ID: number): Observable<IMovieDetails>{
    var Url = this.mediaUrl + "/movie/details/";
    Url += TMDB_ID;
    console.log("Get Movie Details URL = [" + Url + "]");
    return this.http.get<IMovieDetails>(Url)
        .pipe(
          tap(data => console.log('All: ' + JSON.stringify(data))),
          catchError(this.handleError)
        );
  }

  GetTVSHowDetailsAsync(TMDB_ID: number): Observable<ITVShowDetails>{
    var Url = this.mediaUrl + "/tvshow/details/";
    Url += TMDB_ID;

    return this.http.get<ITVShowDetails>(Url)
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