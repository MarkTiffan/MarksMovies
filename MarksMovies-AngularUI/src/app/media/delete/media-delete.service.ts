import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { IMedia } from '../media';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { tap, catchError } from 'rxjs/operators';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class MediaDeleteService {
  private mediaUrl = 'http://localhost:3000/api';
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

  deleteMedia(mediaID: number): Observable<number>{
    var Url = this.mediaUrl + "/media/item/";
    Url += mediaID;
    console.log("Delete URL=" + Url);
    
    return this.http.delete<number>(Url)
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
