import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ListTaskItemsComponent } from './list-task-items/list-task-items.component';
import { AddTaskItemComponent } from './add-task-item/add-task-item.component';
import { ChangeTaskItemComponent } from './change-task-item/change-task-item.component';
import { DeleteTaskItemComponent } from './delete-task-item/delete-task-item.component';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    component : ListTaskItemsComponent
  },
  {
    path: 'add',
    component: AddTaskItemComponent
  },
  {
    path: 'change/:id',
    component: ChangeTaskItemComponent  
  },
  {
    path: 'delete/:id',
    component: DeleteTaskItemComponent
  },
  {
    path: '**',
    redirectTo: '',
  }

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TaskItemsRoutingModule { }
