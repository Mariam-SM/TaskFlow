import { CommonModule } from '@angular/common';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { TaskItemService } from '@proxy/iservices/task-item.service';
import { ProjectService } from '@proxy/services/project.service';
import { CreateTaskDto } from '@proxy/dtos/tasks/models';
import { ProjectDto } from '@proxy/dtos/projects/models';
import { taskPriorityOptions } from '@proxy/enums/task-priority.enum';
import { taskItemStatusOptions } from '@proxy/enums/task-item-status.enum';

@Component({
  selector: 'app-add-task-item',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './add-task-item.component.html',
  styleUrl: './add-task-item.component.scss',
})
export class AddTaskItemComponent implements OnInit {
  @Output() closed = new EventEmitter<void>();
  @Output() added = new EventEmitter<void>();

  isLoading = false;
  projects: ProjectDto[] = [];
  priorityOptions = taskPriorityOptions;
  statusOptions = taskItemStatusOptions;

  form = this.fb.group({
    title: ['', Validators.required],
    description: [''],
    projectId: ['', Validators.required],
    assignedToUserId: [null],
    taskPriority: [1],
    taskStatus: [0],
    dueDate: [null],
  });

  constructor(
    private fb: FormBuilder,
    private _taskService: TaskItemService,
    private _projectService: ProjectService,
  ) {}

  ngOnInit(): void {
    this._projectService.getList({ maxResultCount: 100, skipCount: 0 }).subscribe({
      next: (res) => this.projects = res.items ?? [],
    });
  }

  onSubmit(): void {
  if (this.form.invalid) return;

  this.isLoading = true;

  const rawValue = this.form.getRawValue();

  const dto: CreateTaskDto = {
    title: rawValue.title!,
    description: rawValue.description ?? '',
    projectId: rawValue.projectId!,
    assignedToUserId: rawValue.assignedToUserId ?? null,
    taskPriority: +rawValue.taskPriority!,  // ← + operator
    taskStatus: +rawValue.taskStatus!,       // ← + operator
    dueDate: rawValue.dueDate ?? null,
  };

  console.log('dto:', dto); // ← شوفي الـ values في الـ console

  this._taskService.create(dto).subscribe({
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