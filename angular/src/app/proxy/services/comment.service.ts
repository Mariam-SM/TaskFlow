import { RestService, Rest } from '@abp/ng.core';
import type { PagedAndSortedResultRequestDto, PagedResultDto } from '@abp/ng.core';
import { Injectable, inject } from '@angular/core';
import type { CommentDto, CreateCommentDto } from '../dtos/comments/models';

@Injectable({
  providedIn: 'root',
})
export class CommentService {
  private restService = inject(RestService);
  apiName = 'Default';
  

  create = (input: CreateCommentDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, CommentDto>({
      method: 'POST',
      url: '/api/app/comment',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/comment/${id}`,
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, CommentDto>({
      method: 'GET',
      url: `/api/app/comment/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getList = (input: PagedAndSortedResultRequestDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<CommentDto>>({
      method: 'GET',
      url: '/api/app/comment',
      params: { sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  update = (id: string, input: CreateCommentDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, CommentDto>({
      method: 'PUT',
      url: `/api/app/comment/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });
}