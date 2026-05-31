import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AddProjectComponent } from './add-project/add-project.component';
import { ListProjectsComponent } from './list-projects/list-projects.component';
import { ChangeProjectComponent } from './change-project/change-project.component';
import { ProjectDetailsComponent } from './project-details/project-details.component';
import { DeleteProjectComponent } from './delete-project/delete-project.component';

const routes: Routes = [

  // GET ALL Projects
  {
    path: '',
    pathMatch: 'full',
    component: ListProjectsComponent,
  },

  // CREATE Project
  {
    path: 'add',
    component: AddProjectComponent,
  },

  // GET BY ID / Details
  {
    path: ':id',
    component: ProjectDetailsComponent,
  },

  // UPDATE Project
  {
    path: 'change/:id',
    component: ChangeProjectComponent,
  },
   {
      path: 'delete/:id',
      component: DeleteProjectComponent,
    },

  // fallback
  {
    path: '**',
    redirectTo: '',
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ProjectRoutingModule {}