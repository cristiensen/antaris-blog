import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule, Router } from '@angular/router';
import { PostService } from '../../services/post';

@Component({
  selector: 'app-post-create',
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './post-create.html',
  styleUrl: './post-create.css',
})
export class PostCreate {
  private postService = inject(PostService);
  private router = inject(Router);

  title = '';
  slug = '';
  content = '';
  isPublished = false;
  saving = false;

  generateSlug(): void {
    this.slug = this.title
      .toLowerCase()
      .replace(/[^a-z0-9]+/g, '-')
      .replace(/(^-|-$)+/g, '');
  }

  save(): void {
    if (!this.title || !this.content) return;

    this.saving = true;

    const post = {
      title: this.title,
      slug: this.slug || this.title,
      content: this.content,
      isPublished: this.isPublished,
    };

    this.postService.createPost(post).subscribe({
      next: (newPost) => {
        this.saving = false;
        this.router.navigate(['/post', newPost.id]);
      },
      error: (err) => {
        console.error(err);
        this.saving = false;
      },
    });
  }

  cancel(): void {
    this.router.navigateByUrl('/');
  }
}
