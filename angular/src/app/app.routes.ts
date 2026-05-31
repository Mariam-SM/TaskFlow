import { Routes } from '@angular/router';

export const appRoutes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    loadChildren: () => import('./home/home.routes').then(m => m.homeRoutes),
  },
  {
    path: 'projects',
    loadChildren: () =>
      import('./modules/project/project-routing.module').then(m => m.ProjectRoutingModule),
  },
  {
    path: 'comments',
    loadChildren: () =>
      import('./modules/comment/comment-routing.module').then(m => m.CommentRoutingModule),
  },
  {
    path: 'taskItems',
    loadChildren: () =>
      import('./modules/task-items/task-items-routing.module').then(m => m.TaskItemsRoutingModule),
  },
  {
    path: 'account',
    loadChildren: () => import('@abp/ng.account').then(m => m.createRoutes()),
  },
  {
    path: 'identity',
    loadChildren: () => import('@abp/ng.identity').then(m => m.createRoutes()),
  },
  {
    path: 'tenant-management',
    loadChildren: () =>
      import('@abp/ng.tenant-management').then(m => m.createRoutes()),
  },
  {
    path: 'setting-management',
    loadChildren: () =>
      import('@abp/ng.setting-management').then(m => m.createRoutes()),
  },
];