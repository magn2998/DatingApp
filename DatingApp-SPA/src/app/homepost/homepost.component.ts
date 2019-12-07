import { Component, OnInit, Input } from '@angular/core';
import { Post } from '../_models/post';
import { ProfileService } from '../_services/profile.service';
import { AlertifyService } from '../_services/alertify.service';
import { ProfauthService } from '../_services/profauth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-homepost',
  templateUrl: './homepost.component.html',
  styleUrls: ['./homepost.component.css']
})
export class HomepostComponent implements OnInit {
  @Input() post: Post;
  @Input() likedPosts: Array<any>;
  score = 0;

  constructor(private profileService: ProfileService, private profauthService: ProfauthService,
              private alerifyService: AlertifyService, private router: Router) { }

  ngOnInit() {
    // console.log(this.post);
    console.log('profileBruh' + this.post.id);
    if (this.post.content.length > 55) {
      this.post.content = this.post.content.substring(0, 55) + '(...)';
    }

    if (this.post.postLikers.length > 0) {
      this.post.postLikers.forEach(x => {
        if (x.reaction === 'dislike') { this.score--; } else if (x.reaction === 'like') { this.score++; }
      });
    }
  }

  clickOp() {
    if (this.profauthService.loggedIn()) {
      this.profileService.likePost(1, this.profauthService.decodedToken.nameid, this.post.id).subscribe(() => {
        if (this.post.profileAction === 'like') {
          this.post.profileAction = null;
          this.score--;
        } else if (this.post.profileAction === 'dislike') {
          this.post.profileAction = 'like';
          this.score += 2;
        } else { this.post.profileAction = 'like'; this.score++; }
      }, error => {
        this.alerifyService.error(error);
      });
    } else {
      this.alerifyService.error('Log in or create an account to like posts');
    }
  }

  clickDown() {
    if (this.profauthService.loggedIn()) {
      this.profileService.likePost(0, this.profauthService.decodedToken.nameid, this.post.id).subscribe(data => {
        if (this.post.profileAction === 'dislike') {
          this.post.profileAction = null;
          this.score++;
        } else if (this.post.profileAction === 'like') {
          this.post.profileAction = 'dislike';
          this.score -= 2;
        } else { this.post.profileAction = 'dislike'; this.score--; }
      }, error => {
        this.alerifyService.error(error);
      });
    } else {
      this.alerifyService.error('Log in or create an account to dislike posts');
    }
  }

  clickProfile() {
    this.router.navigate(['profile/' + this.post.profileId]);
  }

  clickPost() {
    this.router.navigate(['post/' + this.post.id]);
  }
}


