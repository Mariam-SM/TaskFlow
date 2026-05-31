import type { CreationAuditedEntityDto } from '@abp/ng.core';

export interface CommentDto extends CreationAuditedEntityDto<string> {
  taskId?: string;
  content?: string;
  creatorUserName?: string;
}

export interface CreateCommentDto {
  taskId: string;
  content: string;
}
