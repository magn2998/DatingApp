import {Routes} from '@angular/router';
import { HomeComponent } from './home/home.component';
import { MemberListComponent } from './members/member-list/member-list.component';
import { MessagesComponent } from './messages/messages.component';
import { ListsComponent } from './lists/lists.component';
import { AuthGuard } from './_guards/auth.guard';
import { MemberDetailComponent } from './members/member-detail/member-detail.component';
import { MemberDetailResolver } from './_resolvers/member-detail.resolve';
import { MemberListResolver } from './_resolvers/member-list.resolve';
import { MemberEditComponent } from './members/member-edit/member-edit.component';
import { MemberEditResolver } from './_resolvers/member-edit.resolve';
import { PreventUnsavedChanges } from './_guards/prevent-unsafed-changes.guard';
import { ListsResolver } from './_resolvers/lists.resolver';
import { MessagesResolver } from './_resolvers/messages.resolver';
import { ProfregisterComponent } from './profregister/profregister.component';
import { FrontpageComponent } from './frontpage/frontpage.component';
import { PostCreationComponent } from './postCreation/postCreation.component';
import { PostCreationResolver } from './_resolvers/postcreation.resolver';
import { PostResolver } from './_resolvers/post.resolver';
import { PostsComponent } from './posts/posts.component';

export const appRoutes: Routes = [
    {path: '', component: FrontpageComponent},
    {path: 'register', component: ProfregisterComponent},
    {
        path: '',
        runGuardsAndResolvers: 'always',
        canActivate: [AuthGuard],
        children: [
            {path: 'createPost', component: PostCreationComponent,
                resolve: {profile: PostCreationResolver}},
            {path: 'post/:id', component: PostsComponent, resolve: {profile: PostResolver}},
            {path: 'members', component: MemberListComponent, resolve: {users: MemberListResolver}},
            {path: 'member/edit', component: MemberEditComponent,
                resolve: {user: MemberEditResolver}, canDeactivate: [PreventUnsavedChanges]},
            {path: 'members/:id', component: MemberDetailComponent, resolve: {user: MemberDetailResolver}},
            {path: 'messages', component: MessagesComponent, resolve: {messages: MessagesResolver}},
            {path: 'lists', component: ListsComponent, resolve: {users: ListsResolver}},
        ]
    },
    {path: '**', redirectTo: '', pathMatch: 'full' }
];
