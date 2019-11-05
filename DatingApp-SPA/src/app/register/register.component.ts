import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { fromEventPattern } from 'rxjs';
import { BsDatepickerConfig } from 'ngx-bootstrap';
import { User } from '../_models/user';
import { Router } from '@angular/router';
import { datepickerAnimation } from 'ngx-bootstrap/datepicker/datepicker-animations';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @Output() cancelRegister = new EventEmitter();

  user: User;
  registerForm: FormGroup;
  bsConfig: Partial<BsDatepickerConfig>;

  constructor(private authService: AuthService, private aleritfy: AlertifyService, private fb: FormBuilder, private router: Router) { }

  ngOnInit() {
    const date: Date = new Date();
    const allowedYear = date.getFullYear() - 8;
    const dateAllowed = allowedYear + '-' + date.getMonth() + '-' + date.getDate();
    this.bsConfig = {
      containerClass: 'theme-red',
      minDate: new Date('1900-1-1'),
      maxDate: new Date(dateAllowed)
    };
    this.createRegisterForm();
  }

  createRegisterForm() {
    this.registerForm = this.fb.group({
      gender: ['male'],
      username: ['', Validators.required],
      knownAs: ['', Validators.required],
      dateOfBirth: [null, Validators.required],
      city: ['', Validators.required],
      country: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(16)]],
      confirmPassword: ['', Validators.required]
    }, {validator: [this.isOlderThanThirteen, this.passwordMatchValidator]});
  }

  passwordMatchValidator(g: FormGroup) {
    return g.get('password').value === g.get('confirmPassword').value ? null : {mismatch: true};
  }

  isOlderThanThirteen(g: FormGroup) {

    if (g.get('dateOfBirth').value !== null) {
      const date: Date = new Date();

      if (g.get('dateOfBirth').value.valueOf() >= (date.valueOf() - 410240038000)) {
          return {mismatchTwo: true};
      }
    }
    return null;
  }


  register()  {
    if (this.registerForm.valid) {
      this.user = Object.assign({}, this.registerForm.value);
      this.authService.register(this.user).subscribe(() => {
        this.aleritfy.success('Registration Succesful');
      }, error => {
        this.aleritfy.error(error);
      }, () => {
        this.authService.login(this.user).subscribe(() => {
          this.router.navigate(['/members']);
        });
      });
    } else { this.aleritfy.message('You should atleast be 13 years old to create account'); }
  }

  cancel()  {
    this.cancelRegister.emit(false);
  }
}
