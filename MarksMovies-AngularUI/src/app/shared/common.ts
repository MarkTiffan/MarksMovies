import { IGenre } from '../media/genre';
import { Injectable } from '@angular/core';
import {UrlSerializer, UrlTree, DefaultUrlSerializer} from '@angular/router';


@Injectable({
  providedIn: 'root'
})
export class CommonFunctions {

  GetGenresAsString(Genres: IGenre[]): string{
    var genreText: string = "";
    if(Genres != null && Genres.length > 0){
      for(var i = 0; i < Genres.length; i++){
          genreText += Genres[i].genre.toString();
          if(i < Genres.length - 1){
              genreText += ", ";
          }
      }
    }
    return genreText;
  }
}



export class CustomUrlSerializer implements UrlSerializer {
    parse(url: any): UrlTree {
        let dus = new DefaultUrlSerializer();
        return dus.parse(url);
    }

    serialize(tree: UrlTree): any {
        let dus = new DefaultUrlSerializer(),
            path = dus.serialize(tree);
        // use your regex to replace as per your requirement.
        return path.replace(/%23/g,'#');
    }
}