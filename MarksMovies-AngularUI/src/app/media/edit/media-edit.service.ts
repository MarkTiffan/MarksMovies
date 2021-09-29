import { Injectable, ChangeDetectionStrategy } from '@angular/core';
import { IMedia } from '../media';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, tap} from 'rxjs/operators';


@Injectable({
  providedIn: 'root'
})
export class MediaEditService {
  private mediaUrl = 'http://localhost:3000/api/media/item/'

  constructor(private http: HttpClient) { }


  getMediaByID(mediaID: number): Observable<IMedia>{
    var Url = this.mediaUrl;
    Url += mediaID;

    return this.http.get<IMedia>(Url)
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
