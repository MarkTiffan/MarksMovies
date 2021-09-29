import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MediaDeleteGuard implements CanActivate {
  constructor(private router: Router){}
  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    let id = +next.url[2].path;  // /media/delete/:id
    if(isNaN(id) || id < 1){
      alert("Invalid media ID");
      this.router.navigate(['/media'], { fragment: id.toString() });
      return false;
    }

    return true;
  }
  
}
