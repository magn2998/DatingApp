import { Component, OnInit } from '@angular/core';
import { ProfauthService } from '../_services/profauth.service';
import { ProfileService } from '../_services/profile.service';
import { ActivatedRoute } from '@angular/router';
import { AlertifyService } from '../_services/alertify.service';
import { Profile } from '../_models/profile';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {

  profile: Profile;
  posts: any[];
  likedPosts = [];
  prePosts = [];

  likedPostIds = [];
  dislikedPostIds = [];

  constructor(private profauthService: ProfauthService, private profileService: ProfileService,
              private route: ActivatedRoute, private alertify: AlertifyService) { }

  ngOnInit() {
    this.route.data.subscribe(res => {
      this.profile = res.profile;
      console.log(this.profile);
      this.prePosts = this.profile.posts;
      if (this.profauthService.loggedIn()) {
        this.profileService.getLikedPosts(this.profauthService.decodedToken.nameid).subscribe((data: Array<any>) => {
          this.likedPosts = data;
          // Get All the Posts with a Reaction
          this.likedPosts.forEach(likedPost => {
            if (likedPost.reaction === 'dislike') {
              this.dislikedPostIds.push(likedPost.postId.toString());
              likedPost.post.profileAction = 'dislike';
            } else {
              this.likedPostIds.push(likedPost.postId.toString());
              likedPost.post.profileAction = 'like';
            }
          });
          // Add reaction to posts
           // Add the reaction to each post
          this.prePosts.forEach(profilePosts => {
            if ( this.likedPostIds.indexOf(profilePosts.id) > -1 ) {
              // tslint:disable-next-line:no-string-literal
              profilePosts.profileAction = 'like';
            } else if ( this.dislikedPostIds.indexOf(profilePosts.id) > -1) {
              profilePosts.profileAction = 'dislike';
            }
          });
        }, error => {
          this.alertify.error(error);
        });
      }
      this.posts = this.prePosts;
    });
  }

}
