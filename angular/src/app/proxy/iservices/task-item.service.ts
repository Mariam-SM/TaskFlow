import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable, inject } from '@angular/core';
import type { TaskListFilterDto } from '../dtos/task-items/models';
import type { CreateTaskDto, TaskDto, UpdateTaskDto } from '../dtos/tasks/models';

@Injectable({
  providedIn: 'root',
})
export class TaskItemService {
  private restService = inject(RestService);
  apiName = 'Default';
  

  complete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, TaskDto>({
      method: 'POST',
      url: `/api/app/task-item/${id}/complete`,
    },
    { apiName: this.apiName,...config });
  

  create = (input: CreateTaskDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, TaskDto>({
      method: 'POST',
      url: '/api/app/task-item',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/task-item/${id}`,
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, TaskDto>({
      method: 'GET',
      url: `/api/app/task-item/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getList = (input: TaskListFilterDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<TaskDto>>({
      method: 'GET',
      url: '/api/app/task-item',
      params: { projectId: input.projectId, status: input.status, priority: input.priority, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  update = (id: string, input: UpdateTaskDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, TaskDto>({
      method: 'PUT',
      url: `/api/app/task-item/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });
}