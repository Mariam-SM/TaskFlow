import type { PagedAndSortedResultRequestDto } from '@abp/ng.core';
import type { TaskItemStatus } from '../../enums/task-item-status.enum';
import type { TaskPriority } from '../../enums/task-priority.enum';

export interface TaskListFilterDto extends PagedAndSortedResultRequestDto {
  projectId?: string | null;
  status?: TaskItemStatus | null;
  priority?: TaskPriority | null;
}
