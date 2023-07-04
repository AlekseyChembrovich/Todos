import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { mapTo, Observable } from "rxjs";
import { ITodo } from "./todo.interface";
import { environment } from "../../environments/environment";

@Injectable()
export class TodoService {
  constructor(private httpClient: HttpClient) { }

  public getTodos(): Observable<ITodo[]> {
    return this.httpClient.get<ITodo[]>(`${environment.resourceApi}/api/notes`);
  }

  public getTodoById(id: string): Observable<ITodo> {
    return this.httpClient.get<ITodo>(`${environment.resourceApi}/api/notes/${id}`)
  }

  public removeTodo(id: string): Observable<any> {
    return this.httpClient.delete(`${environment.resourceApi}/api/notes/${id}`)
  }

  public updateTodo(todo: ITodo): Observable<ITodo> {
    return this.httpClient.put(`${environment.resourceApi}/api/notes`, todo).pipe(
      mapTo(todo)
    )
  }

  public createTodo(todo: ITodo): Observable<ITodo> {
    return this.httpClient.post<ITodo>(`${environment.resourceApi}/api/notes`, todo)
  }
}
