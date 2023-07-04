import { Component, OnInit } from "@angular/core";
import { TodoService } from "../todo.service";
import { ITodo } from "../todo.interface";
import { BehaviorSubject } from "rxjs";
import { TodoHub } from "../todo.hub";

@Component({
  selector: 'app-todo-list',
  templateUrl: 'todo-list.component.html'
})
export class TodoListComponent implements OnInit {
  todos$: BehaviorSubject<ITodo[]> = new BehaviorSubject<ITodo[]>([])
  isLoaded: boolean = false

  constructor(private todoService: TodoService, private todoHub: TodoHub)
  {
    todoHub.addMessageListener(this.todoChangesListener);
  }

  ngOnInit(): void {
    this.todoService.getTodos().subscribe(data => {
      console.log('Retrieve todos', data)
      this.todos$.next(data)
      this.isLoaded = true
    })
  }

  todoChangesListener(message: ITodo) {
    console.log('Execute change todo by SignalR', message)
    let todos = this.todos$.getValue().map((todo) => {
      if (todo.id === message.id) {
        todo.title = message.title
        todo.createdAt = message.createdAt
        todo.expiryDate = message.expiryDate
      }
      return todo
    })
    this.todos$.next(todos)
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
      let todos = this.todos$.getValue().filter(todo => todo.id !== id)
      this.todos$.next(todos)
    })
  }

  change(event: ITodo): void {
    this.todoService.updateTodo(event).subscribe(changedTodo => {
      console.log('Execute change', changedTodo)
      let todos = this.todos$.getValue().map((todo) => {
        if (todo.id === changedTodo.id) {
          todo.title = changedTodo.title
          todo.createdAt = changedTodo.createdAt
          todo.expiryDate = changedTodo.expiryDate
        }
        return todo
      })
      this.todos$.next(todos)
      this.todoHub.sendMessage(event)
    })
  }
}
