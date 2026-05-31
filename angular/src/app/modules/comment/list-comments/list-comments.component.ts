import { PagedAndSortedResultRequestDto } from '@abp/ng.core';
import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { CommentDto, CreateCommentDto } from '@proxy/dtos/comments';
import { CommentService } from '@proxy/services/comment.service';

@Component({
  selector: 'app-list-comments',
  imports: [CommonModule],
  templateUrl: './list-comments.component.html',
  styleUrl: './list-comments.component.scss',
})
export class ListCommentsComponent implements OnInit {

  comments: CommentDto[] = [];
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
    });
  }

  OnPageChange(page: number): void {
    this.input.skipCount = (page - 1) * this.input.maxResultCount;
    this._commentService.getList(this.input).subscribe((response) => {
      this.comments = response.items?? [];
    });
  }

  OnChangeComment(id: string, comment: CommentDto): void {
    const updatedComment: CreateCommentDto = {
      content: comment.content ?? '',
      taskId: comment.taskId ?? ''
    };
    this._commentService.update(id, updatedComment)
      .subscribe(() => {
        this.ngOnInit();
      });
  }

  OnDeleteComment(id: string): void {
    this._commentService.delete(id).subscribe(() => {
      this.ngOnInit();
    });
  }
}
