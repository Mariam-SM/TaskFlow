import { Component, EventEmitter, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommentService } from '@proxy/services';
import { CreateCommentDto } from '@proxy/dtos/comments';

@Component({
  selector: 'app-add-comment',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './add-comment.component.html',
  styleUrl: './add-comment.component.scss',
})
export class AddCommentComponent {
  @Output() closed = new EventEmitter<void>();
  @Output() added = new EventEmitter<void>();

  isLoading = false;

  commentForm = this.fb.group({
    taskId: ['', Validators.required],
    content: ['', Validators.required],
  });

  constructor(
    private _commentService: CommentService,
    private fb: FormBuilder
  ) {}

  onSubmit(): void {
    if (this.commentForm.invalid) return;

    this.isLoading = true;
    const dto: CreateCommentDto = {
      taskId: this.commentForm.value.taskId!,
      content: this.commentForm.value.content!,
    };

    this._commentService.create(dto).subscribe({
      next: () => {
        this.isLoading = false;
        this.added.emit();
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