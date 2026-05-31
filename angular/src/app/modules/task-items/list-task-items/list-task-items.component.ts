import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { TaskDto } from '@proxy/dtos/tasks/models';
import { TaskListFilterDto } from '@proxy/dtos/task-items/models';
import { TaskItemService } from '@proxy/iservices/task-item.service';
import { TaskPriority, taskPriorityOptions } from '@proxy/enums/task-priority.enum';
import { TaskItemStatus, taskItemStatusOptions } from '@proxy/enums/task-item-status.enum';
import { AddTaskItemComponent } from '../add-task-item/add-task-item.component';

@Component({
  selector: 'app-list-task-items',
  standalone: true,
  imports: [CommonModule, FormsModule, AddTaskItemComponent],
  templateUrl: './list-task-items.component.html',
  styleUrl: './list-task-items.component.scss',
})
export class ListTaskItemsComponent implements OnInit {

  tasks: TaskDto[] = [];
  totalCount = 0;
  isLoading = false;

  priorityOptions = taskPriorityOptions;
  statusOptions = taskItemStatusOptions;

  TaskPriority = TaskPriority;
  TaskItemStatus = TaskItemStatus;
  showAddModal = false;

  filter: TaskListFilterDto = {
    maxResultCount: 10,
    skipCount: 0,
    sorting: 'dueDate asc',
    projectId: null,
    status: null,
    priority: null,
  };

  constructor(private _taskService: TaskItemService) {}

  ngOnInit(): void {
    this.loadTasks();
  }

  loadTasks(): void {
    this.isLoading = true;
    this._taskService.getList(this.filter).subscribe({
      next: (response) => {
        this.tasks = response.items ?? [];
        this.totalCount = response.totalCount ??0;
        this.isLoading = false;
      },
      error: () => {
        this.isLoading = false;
      },
    });
  }

  onFilterChange(): void {
    this.filter.skipCount = 0;
    this.loadTasks();
  }

  currentPage = 1;

  onPageChange(page: number): void {
    if (page < 1 || page > Math.ceil(this.totalCount / this.filter.maxResultCount)) return;
    this.currentPage = page;
    this.filter.skipCount = (page - 1) * this.filter.maxResultCount;
    this.loadTasks();
  }

  getPages(): number[] {
    const totalPages = Math.ceil(this.totalCount / this.filter.maxResultCount);
    return Array.from({ length: totalPages }, (_, i) => i + 1);
  }

  min(a: number, b: number): number {
    return Math.min(a, b);
  }

  onAddTask(): void {
    this.showAddModal = true;
  }

  onAddModalClosed(): void {
    this.showAddModal = false;
  }

  onTaskAdded(): void {
    this.loadTasks();
  }
  onDeleteTask(id: string | undefined): void {
    if (!id) return;
    if (!confirm('Are you sure you want to delete this task?')) return;
    this._taskService.delete(id).subscribe(() => this.loadTasks());
  }

  onCompleteTask(id: string | undefined): void {
    if (!id) return;
    this._taskService.complete(id).subscribe(() => this.loadTasks());
  }

  getPriorityClass(priority: TaskPriority | undefined): string {
    switch (priority) {
      case TaskPriority.High: return 'danger';
      case TaskPriority.Medium: return 'warning';
      case TaskPriority.Low: return 'success';
      default: return 'secondary';
    }
  }

  getStatusClass(status: TaskItemStatus | undefined): string {
    switch (status) {
      case TaskItemStatus.Done: return 'success';
      case TaskItemStatus.InProgress: return 'primary';
      case TaskItemStatus.Todo: return 'secondary';
      default: return 'secondary';
    }
  }
}