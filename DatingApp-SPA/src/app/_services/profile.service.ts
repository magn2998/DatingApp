import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Profile } from '../_models/profile';
import { environment } from 'src/environments/environment';
import { Post } from '../_models/post';
import { ProfauthService } from './profauth.service';

@Injectable({
  providedIn: 'root'
})
export class ProfileService {
  likedPosts: Array<any>;
  baseUrl = environment.apiUrl;

constructor(private http: HttpClient, private profautService: ProfauthService) { }

getProfile(id): Observable<Profile> {
  return this.http.get<Profile>(this.baseUrl + 'profile/' + id);
}

createPost(post: Post) {
  return this.http.post(this.baseUrl + 'posts/' + this.profautService.decodedToken.nameid, post);
}

getPost(id: number) {
  return this.http.get(this.baseUrl + 'posts/' + id);
}

getPosts(section: string) {
  return this.http.get(this.baseUrl + 'posts/section/' + section);
}

likePost(action: number, profileId: number, postId: number) {
  return this.http.post(this.baseUrl + 'posts/action/' + profileId + '/' + action + '/' + postId, {});
}

getLikedPosts(profileId: number) {
  return this.http.get(this.baseUrl + 'posts/' + profileId + '/likedposts');
}

}
