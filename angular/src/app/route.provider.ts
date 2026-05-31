import { RoutesService, eLayoutType } from '@abp/ng.core';
import { APP_INITIALIZER } from '@angular/core';

export const APP_ROUTE_PROVIDER = [
  { provide: APP_INITIALIZER, useFactory: configureRoutes, deps: [RoutesService], multi: true },
];

function configureRoutes(routesService: RoutesService) {
  return () => {
    routesService.add([
      {
        path: '/',
        name: '::Menu:Home',
        iconClass: 'fas fa-home',
        order: 1,
        layout: eLayoutType.application,
      },
      {
        path: '/projects',
        name: '::Menu:Projects',
        iconClass: 'fas fa-tasks',
        order: 2,
        layout: eLayoutType.application,
      },
      {
        path: '/comments',
        name: '::Menu:Comments',
        iconClass: 'fas fa-comments',
        order: 3,
        layout: eLayoutType.application,
      },
      {
        path: '/taskItems',
        name: '::Menu:TaskItems',
        iconClass: 'fas fa-tasks',
        order: 4,
        layout: eLayoutType.application,
      }
    ]);
  };
}
