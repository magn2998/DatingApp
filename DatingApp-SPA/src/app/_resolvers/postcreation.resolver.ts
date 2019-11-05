import { Injectable } from '@angular/core';
import { User } from '../_models/user';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { AlertifyService } from '../_services/alertify.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Profile } from '../_models/profile';
import { ProfauthService } from '../_services/profauth.service';
import { ProfileService } from '../_services/profile.service';

@Injectable()
export class PostCreationResolver implements Resolve<Profile> {
constructor(private profileService: ProfileService, private profauthService: ProfauthService,
            private router: Router, private alertify: AlertifyService) {}

resolve(route: ActivatedRouteSnapshot): Observable<Profile> {
    return this.profileService.getProfile(this.profauthService.decodedToken.nameid).pipe(
        catchError(error => {
                this.alertify.error('Problem Retrieving Your Data');
                this.router.navigate(['/members']);
                return of(null);
            })
        );
    }
}
