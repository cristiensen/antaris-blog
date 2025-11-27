import { Comment } from './comment.model';

export interface Post {
  id: number;
  title: string;
  slug: string;
  content: string;
  createdAt: string;
  updatedAt?: string | null;
  isPublished: boolean;
  comments?: Comment[];
}
