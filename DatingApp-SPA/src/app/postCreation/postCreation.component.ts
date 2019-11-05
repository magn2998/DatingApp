import { Component, OnInit } from '@angular/core';
import { Post } from '../_models/post';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ProfauthService } from '../_services/profauth.service';
import { AlertifyService } from '../_services/alertify.service';
import { Router } from '@angular/router';
import { ProfileService } from '../_services/profile.service';

@Component({
  // tslint:disable-next-line:component-selector
  selector: 'app-postCreation',
  templateUrl: './postCreation.component.html',
  styleUrls: ['./postCreation.component.css']
})
export class PostCreationComponent implements OnInit {
  post: Post;
  registerForm: FormGroup;

  constructor(private profauthService: ProfauthService, private aleritfy: AlertifyService,
              private fb: FormBuilder, private router: Router, private profileService: ProfileService) { }

  ngOnInit() {
    this.createRegisterForm();
  }

  createRegisterForm() {
    this.registerForm = this.fb.group({
      title: ['', [Validators.required, Validators.maxLength(36)]],
      content: ['', [Validators.required, Validators.maxLength(2400)]],
      section: ['', Validators.required],
    });
  }

  createPost()  {
    if (this.registerForm.valid) {
      this.post = Object.assign({}, this.registerForm.value);
      this.profileService.createPost(this.post).subscribe(res => {
        this.aleritfy.success('Post Succesfully created');
        // tslint:disable-next-line:no-string-literal
        this.router.navigate(['post/' + res['id']]);
      }, error => {
        this.aleritfy.error(error);
      });
    }
  }

  cancel() {
    if (this.registerForm.value.title || this.registerForm.value.content || this.registerForm.value.section) {
      this.aleritfy.confirm('Are you sure you want to leave? All progress will be lost', () => {
        this.router.navigate(['']);
      });
    } else { this.router.navigate(['']); }
  }

}
