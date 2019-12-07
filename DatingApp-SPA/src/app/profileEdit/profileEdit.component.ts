import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ProfileService } from '../_services/profile.service';
import { AlertifyService } from '../_services/alertify.service';
import { NgxGalleryThumbnailsComponent } from 'ngx-gallery';
import { ProfauthService } from '../_services/profauth.service';

@Component({
  selector: 'app-profileEdit',
  templateUrl: './profileEdit.component.html',
  styleUrls: ['./profileEdit.component.css']
})
export class ProfileEditComponent implements OnInit {

  constructor( private route: ActivatedRoute, private profileService: ProfileService, private alertify: AlertifyService,
               private profauthService: ProfauthService) { }
  profile: any;
  textBoxHeight: number;
  newDescription: string;
  descriptionToAPI: any;
  postClicked = true;
  ownClicked = true;
  likedClicked = false;

  posts: any;
  likedPosts: any;
  dislikedPosts: any;

  prePosts = [];
  preLikedPosts = [];
  preDislikedPosts = [];

  likedPostIds = [];
  dislikedPostIds = [];

  ngOnInit() {
    this.profileService.getProfileEdit().subscribe(data => {
      this.profile = data;
      this.prePosts = this.profile.posts;
      console.log(this.profile);
      // console.log(this.prePosts);
      // console.log(this.profile.postLikes);


      // Get All the Posts with a Reaction
      this.profile.postLikes.forEach(likedPost => {
        if (likedPost.reaction === 'dislike') {
          this.dislikedPostIds.push(likedPost.postId.toString());
          likedPost.post.profileAction = 'dislike';
          this.preDislikedPosts.push(likedPost.post);
        } else {
          this.likedPostIds.push(likedPost.postId.toString());
          likedPost.post.profileAction = 'like';
          this.preLikedPosts.push(likedPost.post);
        }
      });

      // Add the reaction to each post
      this.prePosts.forEach(profilePosts => {
        if ( this.likedPostIds.indexOf(profilePosts.id) > -1 ) {
          // tslint:disable-next-line:no-string-literal
          profilePosts.profileAction = 'like';
        } else if ( this.dislikedPostIds.indexOf(profilePosts.id) > -1) {
          profilePosts.profileAction = 'dislike';
        }
      });
      this.posts = this.prePosts;
      this.likedPosts = this.preLikedPosts;
      this.dislikedPosts = this.preDislikedPosts;
      console.log(this.posts);
      console.log(this.likedPosts);
      console.log(this.dislikedPosts);
    });

  }

  updateDescription() {
    this.newDescription = document.getElementById('profileDescription').value;
    this.descriptionToAPI = {
      description: this.newDescription
    };
    console.log(this.descriptionToAPI);
    this.profileService.updateProfileDescription(this.descriptionToAPI).subscribe(res => {
      this.alertify.success('Successfully Updated Your Descriptino');
    }, error => {
      this.alertify.error('Error Accoured when updating your Description');
    });
  }

  clickShowPosts() {
    if (!this.postClicked) {
      this.postClicked = true;
      document.getElementById('commentButtonId').classList.remove('active');
      document.getElementById('postButtonId').classList.add('active');
      console.log('Clicked!');
    }
  }

  clickShowComments() {
    if (this.postClicked) {
      this.postClicked = false;
      document.getElementById('commentButtonId').classList.add('active');
      document.getElementById('postButtonId').classList.remove('active');
      console.log('Clicked!!');
    }
  }

  clickOwn() {
    if (!this.ownClicked) {
      this.ownClicked = true;
      this.likedClicked = false;
      document.getElementById('ownButton').classList.add('active');
      document.getElementById('likeButton').classList.remove('active');
      document.getElementById('dislikeButton').classList.remove('active');
    }
  }

  clickLiked() {
    if (!this.likedClicked) {
      this.likedClicked = true;
      this.ownClicked = false;
      document.getElementById('ownButton').classList.remove('active');
      document.getElementById('likeButton').classList.add('active');
      document.getElementById('dislikeButton').classList.remove('active');
    }
  }

  clickDisliked() {
    if (this.likedClicked || this.ownClicked) {
      this.likedClicked = false; 
      this.ownClicked = false;
      document.getElementById('ownButton').classList.remove('active');
      document.getElementById('likeButton').classList.remove('active');
      document.getElementById('dislikeButton').classList.add('active');
    }
  }

  clickDeletePost(postToDelete) {
    console.log(postToDelete);
    this.alertify.confirm('Are you SURE you want to delete your "' + postToDelete.title + '" post? This cannot be undone', () => {
      this.profileService.deletePost(this.profauthService.decodedToken.nameid, postToDelete.id).subscribe(e => {
        this.alertify.success('successfully deleted your post');
        document.getElementById('ownPost' + postToDelete.id).style.display = 'none';
      }, error => {
        this.alertify.error(error);
      });
    });
  }
}


