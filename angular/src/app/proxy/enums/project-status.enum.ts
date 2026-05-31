import { mapEnumToOptions } from '@abp/ng.core';

export enum ProjectStatus {
  New = 0,
  Active = 1,
  Completed = 2,
}

export const projectStatusOptions = mapEnumToOptions(ProjectStatus);
