import { Component, OnInit, ViewChild, HostListener } from '@angular/core';
import { User } from 'src/app/_models/user';
import { ActivatedRoute, Router } from '@angular/router';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { NgForm } from '@angular/forms';
import { UserService } from 'src/app/_services/user.service';
import { AuthService } from 'src/app/_services/auth.service';

@Component({
  selector: 'app-member-edit',
  templateUrl: './member-edit.component.html',
  styleUrls: ['./member-edit.component.css']
})
export class MemberEditComponent implements OnInit {

  constructor(private route: ActivatedRoute, private aleritfy: AlertifyService,
              private userService: UserService, private authService: AuthService, private router: Router) { }

  @ViewChild('editForm', {static: true}) editForm: NgForm;
  user: User;
  photoUrl: string;
  @HostListener('window:beforeunload', ['$event'])
  unloadNotification($event: any) {
    if (this.editForm.dirty) {
      $event.returnValue = true;
    }
  }
  ngOnInit() {
    this.route.data.subscribe(data => {
      this.user = data.user;
    });
    this.authService.currentPhotoUrl.subscribe(photoUrl => this.photoUrl = photoUrl);
  }

  updateUser() {
    this.userService.updateUser(this.authService.decodedToken.nameid, this.user).subscribe(next => {
      this.aleritfy.success('Profile Updated Succesfully');
      this.editForm.reset(this.user);
    }, error => {
      this.aleritfy.error(error);
    });

  }

  updateMainPhoto(photoUrl) {
    this.user.photoUrl = photoUrl;
  }

  deleteProfile() {
    // tslint:disable-next-line:max-line-length
    this.aleritfy.confirm('Are you sure you want to delete your profile? This includes every message you have ever sent or recieved.', () => {
      this.userService.deleteUser(this.authService.decodedToken.nameid).subscribe(next => {
      this.aleritfy.success('Succesfully Deleted Your Profile');
      localStorage.removeItem('token');
      localStorage.removeItem('user');
      this.authService.decodedToken = null;
      this.authService.currentUser = null;
      this.router.navigate(['/home']);
      }, () => {
        this.aleritfy.error('Failed to delete profile');
      });
    });
  }


}
