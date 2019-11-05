import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { AlertifyService } from '../_services/alertify.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Post } from '../_models/post';
import { ProfileService } from '../_services/profile.service';

@Injectable()
export class PostResolver implements Resolve<Post> {
constructor(private profileService: ProfileService, private router: Router, private alertify: AlertifyService) {}

resolve(route: ActivatedRouteSnapshot): Observable<Post> {
    return this.profileService.getPost(route.params.id).pipe(
        catchError(error => {
                this.alertify.error('Trouble getting the post');
                this.router.navigate(['']);
                // console.log(error);
                return of(null);
            })
        );
    }
}
