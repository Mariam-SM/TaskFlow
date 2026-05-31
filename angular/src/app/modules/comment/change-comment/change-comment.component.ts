import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { CommentDto, CreateCommentDto } from '@proxy/dtos/comments';
import { CommentService } from '@proxy/services/comment.service';

@Component({
  selector: 'app-change-comment',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './change-comment.component.html',
  styleUrl: './change-comment.component.scss',
})
export class ChangeCommentComponent implements OnInit {
  @Input() comment!: CommentDto;
  @Output() closed = new EventEmitter<void>();
  @Output() updated = new EventEmitter<void>();

  form: CreateCommentDto = {
    taskId: '',
    content: '',
  };

  isLoading = false;

  constructor(private _commentService: CommentService) {}

  ngOnInit(): void {
    this.form = {
      taskId: this.comment.taskId ?? '',
      content: this.comment.content ?? '',
    };
  }

  onSubmit(): void {
    if (!this.form.content.trim() || !this.comment.id) return;

    this.isLoading = true;
    this._commentService.update(this.comment.id, this.form).subscribe({
      next: () => {
        this.isLoading = false;
        this.updated.emit();
        this.closed.emit();
      },
      error: () => {
        this.isLoading = false;
      },
    });
  }

  onClose(): void {
    this.closed.emit();
  }
}