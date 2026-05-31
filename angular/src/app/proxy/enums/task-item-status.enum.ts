import { mapEnumToOptions } from '@abp/ng.core';

export enum TaskItemStatus {
  Todo = 0,
  InProgress = 1,
  Done = 2,
}

export const taskItemStatusOptions = mapEnumToOptions(TaskItemStatus);
