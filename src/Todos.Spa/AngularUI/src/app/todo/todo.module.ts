import { NgModule } from "@angular/core";
import { TodoCardComponent } from "./todo-card/todo-card.component";
import { TodoService } from "./todo.service";
import { TodoListComponent } from "./todo-list/todo-list.component";
import { CommonModule, DatePipe } from "@angular/common";
import { RouterModule, Routes } from "@angular/router";
import { TodoFormComponent } from "./todo-form/todo-form.component";
import { ReactiveFormsModule } from "@angular/forms";
import { TodoHub } from "./todo.hub";

const routes: Routes = [
  { path: 'todos', component: TodoListComponent }
]

@NgModule({
  declarations: [TodoCardComponent, TodoListComponent, TodoFormComponent],
  imports: [CommonModule, RouterModule.forChild(routes), ReactiveFormsModule],
  providers: [TodoService, TodoHub, DatePipe]
})
export class TodoModule {
}
