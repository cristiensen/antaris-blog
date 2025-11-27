import { Routes } from '@angular/router';
import { PostList } from './components/post-list/post-list';
import { PostDetail } from './components/post-detail/post-detail';
import { PostCreate } from './components/post-create/post-create';

export const routes: Routes = [
  { path: '', component: PostList },
  { path: 'post/new', component: PostCreate },
  { path: 'post/:id', component: PostDetail },
  { path: '**', redirectTo: '' },
];
