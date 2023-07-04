import { Component, EventEmitter, Output } from "@angular/core";
import { FormControl, FormGroup, Validators } from "@angular/forms";
import { ITodo } from "../todo.interface";

@Component({
  selector: 'app-todo-form',
  templateUrl: './todo-form.component.html'
})
export class TodoFormComponent {
  @Output()
  createEvent: EventEmitter<ITodo> = new EventEmitter<ITodo>();

  todoForm = new FormGroup({
    title: new FormControl('', Validators.required),
    expirationDate: new FormControl('', Validators.required),
  });

  create() {
    let todo: ITodo = {
      title: this.todoForm.value.title,
      createdAt: new Date(),
      expiryDate: this.todoForm.value.expirationDate
    }

    this.createEvent.emit(todo)
    this.todoForm.reset();
  }
}
