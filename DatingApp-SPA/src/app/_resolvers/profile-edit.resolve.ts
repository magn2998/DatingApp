import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { UserService } from '../_services/user.service';
import { AlertifyService } from '../_services/alertify.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { AuthService } from '../_services/auth.service';
import { Profile } from '../_models/profile';
import { ProfileService } from '../_services/profile.service';

@Injectable()
export class ProfileEditResolver implements Resolve<Profile> {
constructor(private profileService: ProfileService,
            private router: Router, private alertify: AlertifyService) {}

resolve(route: ActivatedRouteSnapshot): Observable<Profile> {
    return this.profileService.getProfileEdit().pipe(
        catchError(error => {
                this.alertify.error('Problem Retrieving Your Data');
                this.router.navigate(['/members']);
                return of(null);
            })
        );
    }
}
