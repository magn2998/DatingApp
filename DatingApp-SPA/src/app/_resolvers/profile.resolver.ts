import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { AlertifyService } from '../_services/alertify.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { ProfileService } from '../_services/profile.service';
import { Profile } from '../_models/profile';

@Injectable()
export class ProfileResovler implements Resolve<Profile> {
constructor(private profileService: ProfileService, private router: Router, private alertify: AlertifyService) {}

resolve(route: ActivatedRouteSnapshot): Observable<Profile> {
    return this.profileService.getProfile(route.params.id).pipe(
        catchError(error => {
                this.alertify.error('Trouble getting the profile');
                this.router.navigate(['']);
                // console.log(error);
                return of(null);
            })
        );
    }
}
