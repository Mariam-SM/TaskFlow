import { PagedAndSortedResultRequestDto } from '@abp/ng.core';
import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { CommentDto, CreateCommentDto } from '@proxy/dtos/comments';
import { CommentService } from '@proxy/services/comment.service';
import { ChangeCommentComponent } from '../change-comment/change-comment.component';
import { AddCommentComponent } from '../add-comment/add-comment.component';

@Component({
  selector: 'app-list-comments',
  imports: [CommonModule , ChangeCommentComponent, AddCommentComponent],
  templateUrl: './list-comments.component.html',
  styleUrl: './list-comments.component.scss',
})
export class ListCommentsComponent implements OnInit {

  comments: CommentDto[] = [];
  selectedComment: CommentDto | null = null;
  showAddModal = false;

  input : PagedAndSortedResultRequestDto = {
    maxResultCount: 10,
    skipCount: 0,
    sorting: 'id asc',
  };

  constructor(private _commentService: CommentService) {
  }


  ngOnInit(): void {
    this._commentService.getList(this.input).subscribe((response) => {
      this.comments = response.items?? [];
      this.totalCount = response.totalCount ?? 0;
    });
  }

  onAddComment(): void {
    this.showAddModal = true;
  }

  onAddModalClosed(): void {
    this.showAddModal = false;
  }

  onCommentAdded(): void {
    this.ngOnInit();
  }

  onEditComment(comment: CommentDto): void {
    if (!comment.id) return;
    this.selectedComment = comment;
  }

  onModalClosed(): void {
    this.selectedComment = null;
  }

  onCommentUpdated(): void {
    this.ngOnInit();
  }

  OnDeleteComment(id: string): void {
    this._commentService.delete(id).subscribe(() => {
      this.ngOnInit();
    });
  }

  totalCount = 0;
  currentPage = 1;

  OnPageChange(page: number): void {
    if (page < 1 || page > Math.ceil(this.totalCount / this.input.maxResultCount)) return;
    this.currentPage = page;
    this.input.skipCount = (page - 1) * this.input.maxResultCount;
    this._commentService.getList(this.input).subscribe((response) => {
      this.comments = response.items ?? [];
      this.totalCount = response.totalCount ?? 0;
    });
  }

  getPages(): number[] {
    const totalPages = Math.ceil(this.totalCount / this.input.maxResultCount);
    return Array.from({ length: totalPages }, (_, i) => i + 1);
  }

  min(a: number, b: number): number {
    return Math.min(a, b);
  }
}
