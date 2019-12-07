import { Component, OnInit } from '@angular/core';
import { ProfauthService } from '../_services/profauth.service';
import { AlertifyService } from '../_services/alertify.service';
import { Router } from '@angular/router';

@Component({
  // tslint:disable-next-line:component-selector
  selector: 'app-topNav',
  templateUrl: './topNav.component.html',
  styleUrls: ['./topNav.component.css']
})
export class TopNavComponent implements OnInit {

  constructor(public profauthService: ProfauthService, private alertify: AlertifyService, private router: Router) { }

loginDisplay: boolean;
model: any = {};

  ngOnInit() {
    this.loginDisplay = false;
  }

  showLogin() {
    this.loginDisplay = true;
  }

  login() {
    console.log('Logging In');
    this.profauthService.login(this.model).subscribe(next => {
      this.alertify.success('Logged in succesfully');
    }, error => {
      this.alertify.error(error);
    }, () => {
      window.location.reload();
    });
  }

  cancelLogin() {
    this.loginDisplay = false;
  }

  createAccount() {
    this.router.navigate(['/register']);
  }

  loggedIn()  {
    return this.profauthService.loggedIn();
  }

  logout()  {
    this.loginDisplay = false;
    localStorage.removeItem('token');
    this.profauthService.decodedToken = null;
    this.alertify.message('logged out');
    window.location.reload();
  }

  newPost() {
    this.router.navigate(['/createPost']);
  }

  titleClick() {
    this.router.navigate(['']);
  }

  goToEditProfile() {
    this.router.navigate(['/editprofile']);
  }

}
