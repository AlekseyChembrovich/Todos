import { Component, EventEmitter, Input, OnInit, Output } from "@angular/core";
import { ITodo } from "../todo.interface";
import { FormControl, FormGroup, Validators } from "@angular/forms";
import { DatePipe } from "@angular/common";

@Component({
  selector: 'app-todo-card',
  templateUrl: 'todo-card.component.html'
})
export class TodoCardComponent implements OnInit {
  @Input()
  todo: ITodo | null = null

  @Output()
  deleteEvent: EventEmitter<string> = new EventEmitter<string>()

  @Output()
  changeEvent: EventEmitter<ITodo> = new EventEmitter<ITodo>()

  isDisableTask: boolean = true;
  isDisableExpirationDate: boolean = true;

  todoForm: FormGroup = new FormGroup({});

  constructor(private datePipe: DatePipe) { }

  ngOnInit() {
    let createdDateFormat = this.datePipe.transform(this.todo?.CreatedDate, 'yyyy-MM-dd')
    let expirationDateFormat = this.datePipe.transform(this.todo?.ExpirationDate, 'yyyy-MM-dd')
    this.todoForm = new FormGroup({
      task: new FormControl(this.todo?.Task, Validators.required),
      createdDate: new FormControl(createdDateFormat, Validators.required),
      expirationDate: new FormControl(expirationDateFormat, Validators.required)
    })
  }

  delete(): void {
    if (this.todo === null) {
      console.error('error: can`t execute deleting of todo. id is undefined.')
      return
    }
    console.log('call delete event.')
    this.deleteEvent.emit(this.todo.Id)
  }

  change(): void {
    if (this.todo === null) {
      console.error('error: can`t execute changing of todo. id is undefined.')
      return
    }
    let changedTodo: ITodo = {
      Id: this.todo.Id,
      Task: this.todoForm.value.task,
      CreatedDate: this.todoForm.value.createdDate,
      ExpirationDate: this.todoForm.value.expirationDate
    }
    this.changeEvent.emit(changedTodo)
    this.isDisableTask = true;
    this.isDisableExpirationDate = true;
  }
}
