import type { ProjectStatus } from '../../enums/project-status.enum';
import type { FullAuditedEntityDto } from '@abp/ng.core';

export interface CreateProjectDto {
  name: string;
  description?: string;
  status?: ProjectStatus;
  startDate: string | null;
  endDate?: string | null;
}

export interface ProjectDto extends FullAuditedEntityDto<string> {
  name?: string;
  description?: string;
  status?: ProjectStatus;
  startDate?: string | null;
  endDate?: string | null;
}

export interface UpdateProjectDto {
  name: string;
  description?: string;
  status?: ProjectStatus;
  startDate?: string | null;
  endDate?: string | null;
}
