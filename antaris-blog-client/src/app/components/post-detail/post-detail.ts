import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { Observable, of, Subject, merge } from 'rxjs';
import { catchError, switchMap, shareReplay } from 'rxjs/operators';
import { PostService } from '../../services/post';
import { CommentService } from '../../services/comment';
import { Post } from '../../models/post.model';
import { Comment } from '../../models/comment.model';

@Component({
  selector: 'app-post-detail',
  imports: [CommonModule, RouterModule, FormsModule],
  templateUrl: './post-detail.html',
  styleUrl: './post-detail.css',
})
export class PostDetail {
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private postService = inject(PostService);
  private commentService = inject(CommentService);

  postId: number;

  // observables for async pipe
  post$: Observable<Post | null>;
  comments$: Observable<Comment[]>;

  // errors
  error = '';
  commentsError = '';

  // comment form fields (bound with ngModel)
  newCommentAuthor = '';
  newCommentText = '';

  // trigger stream for adding a comment
  private addCommentTrigger$ = new Subject<{ author: string; text: string }>();

  constructor() {
    this.postId = Number(this.route.snapshot.paramMap.get('id') ?? 0);
    console.log('PostDetail postId =', this.postId);

    if (!this.postId) {
      this.error = 'Invalid post id.';
      this.post$ = of(null);
      this.comments$ = of([]);
      return;
    }

    // POST observable (async pipe handles subscription)
    this.post$ = this.postService.getPost(this.postId).pipe(
      catchError((err) => {
        console.error('Error loading post', err);
        this.error = 'Failed to load post.';
        return of(null);
      })
    );

    // COMMENTS observable
    // - initial load
    // - + reload after each successful addCommentTrigger$
    const initialComments$ = this.commentService.getCommentsByPost(this.postId);

    const afterAddComments$ = this.addCommentTrigger$.pipe(
      switchMap((payload) =>
        this.commentService
          .createComment({
            authorName: payload.author,
            text: payload.text,
            postId: this.postId,
          })
          .pipe(
            // after creating, reload comments
            switchMap(() => this.commentService.getCommentsByPost(this.postId))
          )
      )
    );

    this.comments$ = merge(initialComments$, afterAddComments$).pipe(
      catchError((err) => {
        console.error('Error in comments stream', err);
        this.commentsError = 'Failed to load comments.';
        return of([] as Comment[]);
      }),
      shareReplay(1)
    );
  }

  onSubmitComment(): void {
    if (!this.newCommentAuthor || !this.newCommentText) return;

    const payload = {
      author: this.newCommentAuthor,
      text: this.newCommentText,
    };

    // optimistically clear input
    this.newCommentAuthor = '';
    this.newCommentText = '';

    // emit event into the stream (no subscribe)
    this.addCommentTrigger$.next(payload);
  }

  back(): void {
    this.router.navigateByUrl('/');
  }
}
