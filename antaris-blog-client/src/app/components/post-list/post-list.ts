import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Router } from '@angular/router';
import { PostService } from '../../services/post';
import { Post } from '../../models/post.model';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-post-list',
  imports: [CommonModule, RouterModule],
  templateUrl: './post-list.html',
  styleUrl: './post-list.css',
})
export class PostList {
  private postService = inject(PostService);
  private router = inject(Router);

  posts$: Observable<Post[]> = this.postService.getPosts();

  goToPost(post: Post): void {
    this.router.navigate(['/post', post.id]);
  }

  createPost(): void {
    this.router.navigate(['/post/new']);
  }
}
