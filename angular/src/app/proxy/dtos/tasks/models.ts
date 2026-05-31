import type { TaskPriority } from '../../enums/task-priority.enum';
import type { TaskItemStatus } from '../../enums/task-item-status.enum';
import type { FullAuditedEntityDto } from '@abp/ng.core';

export interface CreateTaskDto {
  title: string;
  description?: string;
  projectId: string;
  assignedToUserId?: string | null;
  taskPriority?: TaskPriority;
  taskStatus?: TaskItemStatus;
  dueDate?: string | null;
}

export interface TaskDto extends FullAuditedEntityDto<string> {
  title?: string;
  description?: string;
  projectId?: string;
  projectName?: string;
  assignedToUserId?: string;
  assignedToUserName?: string;
  taskPriority?: TaskPriority;
  taskStatus?: TaskItemStatus;
  dueDate?: string;
  isCompleted?: boolean;
}

export interface UpdateTaskDto {
  title: string;
  description?: string;
  assignedToUserId?: string;
  taskPriority?: TaskPriority;
  taskStatus?: TaskItemStatus;
  dueDate?: string;
  isCompleted?: boolean;
}
