import { Component, OnInit } from "@angular/core";
import { TodoService } from "../todo.service";
import { ITodo } from "../todo.interface";
import { BehaviorSubject } from "rxjs";

@Component({
  selector: 'app-todo-list',
  templateUrl: 'todo-list.component.html'
})
export class TodoListComponent implements OnInit {
  todos$: BehaviorSubject<ITodo[]> = new BehaviorSubject<ITodo[]>([])
  isLoaded: boolean = false

  constructor(private todoService: TodoService) { }

  ngOnInit(): void {
    this.todoService.getTodos().subscribe(data => {
      console.log('Retrieve todos', data)
      this.todos$.next(data)
      this.isLoaded = true
    })
  }

  create(event: ITodo) {
    this.todoService.createTodo(event).subscribe(createdTodo => {
      console.log('Execute creation', createdTodo)
      let filledTodos = this.todos$.getValue().concat([ createdTodo ])
      console.log('Filled todos: ', filledTodos)
      this.todos$.next(filledTodos)
    })
  }

  delete(event: string): void {
    let id: string = event
    this.todoService.removeTodo(id).subscribe(data => {
      console.log('Execute removing', data)
      let todos = this.todos$.getValue().filter(todo => todo.Id !== id)
      this.todos$.next(todos)
    })
  }

  change(event: ITodo): void {
    this.todoService.updateTodo(event).subscribe(changedTodo => {
      console.log('Execute change', changedTodo)
      let todos = this.todos$.getValue().map((todo) => {
        if (todo.Id === changedTodo.Id) {
          todo.Task = changedTodo.Task
          todo.CreatedDate = changedTodo.CreatedDate
          todo.ExpirationDate = changedTodo.ExpirationDate
        }
        return todo
      })
      this.todos$.next(todos)
    })
  }
}
