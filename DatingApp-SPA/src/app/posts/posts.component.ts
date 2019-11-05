import { Component, OnInit } from '@angular/core';
import { AlertifyService } from '../_services/alertify.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Post } from '../_models/post';
import { ProfileService } from '../_services/profile.service';

@Component({
  selector: 'app-posts',
  templateUrl: './posts.component.html',
  styleUrls: ['./posts.component.css']
})
export class PostsComponent implements OnInit {
  post: Post;

  constructor(private profileService: ProfileService, private alertify: AlertifyService,
              private route: ActivatedRoute, private router: Router) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      if (data.profile) {
        this.post = data.profile;
      } else {
        this.alertify.error('This post doesnt exist');
        this.router.navigate(['']);
      }
    });
  }

  click() {
    this.profileService.getPost(163).subscribe(res => {
      console.log(res);
    });
  }

}
