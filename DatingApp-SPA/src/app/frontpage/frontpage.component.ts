import { Component, OnInit } from '@angular/core';
import { ProfileService } from '../_services/profile.service';
import { AlertifyService } from '../_services/alertify.service';
import { Post } from '../_models/post';
import { ProfauthService } from '../_services/profauth.service';

@Component({
  selector: 'app-frontpage',
  templateUrl: './frontpage.component.html',
  styleUrls: ['./frontpage.component.css']
})
export class FrontpageComponent implements OnInit {
  // tslint:disable-next-line:max-line-length
  content = 'Somebody once told me the world is gonna roll me. I aint the sharpest tool in the shed. she was looking kinda dumb with a finger and a thump';
  actualContent: string;
  prePosts: Post[];
  posts: Post[];
  likedPosts: Array<any>;

  constructor(private profileService: ProfileService, private alertify: AlertifyService, private profauthService: ProfauthService) {}

  ngOnInit() {
    this.profileService.getPosts('forum').subscribe((res: Post[]) => {
      this.prePosts = res;
      console.log(this.posts);
    }, null, () => {
      if (this.profauthService.loggedIn()) {
        this.profileService.getLikedPosts(this.profauthService.decodedToken.nameid).subscribe((res: Array<any>) => {
          this.likedPosts = res;
          console.log(this.likedPosts);

          this.prePosts.forEach(x => {
            this.likedPosts.forEach(y => {
              if (x.id === y.postId) {
                // tslint:disable-next-line:no-string-literal
                x['profileAction'] = y.reaction;
              }
            });
          });
        });
      }
      this.posts = this.prePosts;
    });


  }
  /*
  console.log(this.posts);
      this.posts.forEach( x => {
        if (x.content.length > 55) {
          x.content = x.content.substring(0, 55);
          x.content = x.content + '(...)';
        }
      });
    this.actualContent = this.content.substring(0, 62);
    this.actualContent = this.actualContent + '(...)'; */
  clickOp() {
    console.log('Upvote');
  }

  clickDown() {
    console.log('Downvote');
  }

}
