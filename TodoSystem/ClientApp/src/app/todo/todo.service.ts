import { Inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { TodoView } from './Models/todoView';
import { TodoItem } from './Models/todoItem';
import { Observable } from 'rxjs';

@Injectable()
export class TodoService {
  private baseUrl: string;
  private http: HttpClient;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
    this.http = http;
  }

  public GetTodoList(): Observable<TodoView> {
    return this.http.get<TodoView>(this.baseUrl + 'todo');
  }

  public CreateTodo(todo: TodoItem): Observable<any> {
    return this.http.post<TodoItem>(this.baseUrl + 'todo', todo);
  }

  public UpdateTodo(todo: TodoItem): Observable<any> {
    return this.http.put<TodoItem>(this.baseUrl + 'todo', todo);
  }
}

