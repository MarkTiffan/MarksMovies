import { Injectable, ChangeDetectionStrategy } from '@angular/core';
import { IMedia } from './media';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, tap} from 'rxjs/operators';
import { GenreType } from './genre';

@Injectable({
  providedIn: 'root'
})
export class MediaService {
  //private mediaUrl = '/assets/movies.json'
  private mediaUrl = 'http://localhost:3000/api/media/items'

  constructor(private http: HttpClient) { }


  getMedia(genreFilter: string, titleFilter: string): Observable<IMedia[]>{
    var Url = this.mediaUrl;
    if(genreFilter != "0" && genreFilter != "All"){
      Url += "?Genre=" + genreFilter;
      if(titleFilter != ""){
        Url += "&Title=" + titleFilter;
      }
    } else if (titleFilter != ""){
      Url += "?Title=" + titleFilter;
    }

    return this.http.get<IMedia[]>(Url)
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
