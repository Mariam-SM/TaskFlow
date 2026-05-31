import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ListCommentsComponent } from './list-comments/list-comments.component';
import { AddCommentComponent } from './add-comment/add-comment.component';
import { DeleteCommentComponent } from './delete-comment/delete-comment.component';
import { ChangeCommentComponent } from './change-comment/change-comment.component';
import { CommentDetailsComponent } from './comment-details/comment-details.component';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    component: ListCommentsComponent
  },
  {
    path: 'add',
    component: AddCommentComponent
  },
  {
    path: ':id',
    component: CommentDetailsComponent
  },
  {
    path: 'change/:id',
    component: ChangeCommentComponent
  },
  {
    path: 'delete/:id',
    component: DeleteCommentComponent
  },
  {
    path: '**',
    redirectTo: '',
  }, 
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CommentRoutingModule { }
