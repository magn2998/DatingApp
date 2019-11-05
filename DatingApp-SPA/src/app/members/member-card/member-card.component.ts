import { Component, OnInit, Input } from '@angular/core';
import { User } from 'src/app/_models/user';
import { AuthService } from 'src/app/_services/auth.service';
import { UserService } from 'src/app/_services/user.service';
import { AlertifyService } from 'src/app/_services/alertify.service';

@Component({
  selector: 'app-member-card',
  templateUrl: './member-card.component.html',
  styleUrls: ['./member-card.component.css']
})
export class MemberCardComponent implements OnInit {
  @Input() user: User;
  likedUsers: number[] = [];

  constructor(private authService: AuthService, private userService: UserService, private alertify: AlertifyService) { }

  ngOnInit() {
    this.userService.getLikers(this.authService.decodedToken.nameid).subscribe((data: number[]) => {
      this.likedUsers = data;
      this.likedUsers.forEach(elem => {
        if (elem === this.user.id) {
          this.user.isLiked = true;
        }
      });
    }, error => {
      this.alertify.error(error);
    });
  }

  sendLike(id: number) {
    this.userService.sendLike(this.authService.decodedToken.nameid, id).subscribe(data => {
      if (!data) {
        this.alertify.success('You Have Liked ' + this.user.knownAs); this.user.isLiked = true; } else {
        this.alertify.error('You Have Disliked ' + this.user.knownAs); this.user.isLiked = false; }
    }, error => {
      this.alertify.error(error);
    });
  }

  getLikers() {
    this.userService.getLikers(this.authService.decodedToken.nameid).subscribe((data: number[]) => {
      this.likedUsers = data;
    }, error => {
      this.alertify.error(error);
    });
  }

}
