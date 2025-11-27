import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PostList } from './components/post-list/post-list';
import { PostDetail } from './components/post-detail/post-detail';
import { PostCreate } from './components/post-create/post-create';

const routes: Routes = [
  { path: '', component: PostList },
  { path: 'post/new', component: PostCreate },
  { path: 'post/:id', component: PostDetail },
  { path: '**', redirectTo: '' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
