import { Component, OnInit } from '@angular/core';
import { ProfauthService } from '../_services/profauth.service';
import { AlertifyService } from '../_services/alertify.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Profile } from '../_models/profile';

@Component({
  selector: 'app-profregister',
  templateUrl: './profregister.component.html',
  styleUrls: ['./profregister.component.css']
})
export class ProfregisterComponent implements OnInit {

  user: Profile;
  registerForm: FormGroup;

  constructor(private profauthService: ProfauthService, private aleritfy: AlertifyService,
              private fb: FormBuilder, private router: Router) { }

  ngOnInit() {
    if (this.profauthService.decodedToken) { this.aleritfy.error('Youre already logged in'); this.router.navigate(['']); }
    this.createRegisterForm();
  }

  createRegisterForm() {
    this.registerForm = this.fb.group({
      username: ['', Validators.required],
      profileName: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(12)]],
      password: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(16)]],
      confirmPassword: ['', Validators.required]
    }, {validator: this.passwordMatchValidator});
  }

  passwordMatchValidator(g: FormGroup) {
    return g.get('password').value === g.get('confirmPassword').value ? null : {mismatch: true};
  }

  register()  {
    if (this.registerForm.valid) {
      this.user = Object.assign({}, this.registerForm.value);
      this.profauthService.register(this.user).subscribe(() => {
        this.aleritfy.success('Registration Succesful');
      }, error => {
        this.aleritfy.error(error);
      }, () => {
        this.profauthService.login(this.user).subscribe(() => {
          this.router.navigate(['']);
        });
      });
    }
  }

  cancel() {
    this.router.navigate(['']);
  }

}
