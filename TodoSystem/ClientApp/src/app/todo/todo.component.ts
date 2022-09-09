import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { StatusEnum, TodoItem } from './Models/todoItem';
import { TodoView } from './Models/todoView';
import { TodoService } from './todo.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { formatDate } from '@angular/common';

@Component({
  selector: 'app-todo',
  templateUrl: './todo.component.html',
  styleUrls: ['./todo.component.css']
})
export class TodoComponent implements OnInit  {
  public todoView: TodoView

  public showModal = false;
  public submitted = false;

  public todoForm: FormGroup;
  public editForm: FormGroup;

  get eForm() { return this.editForm.controls; }

  get tForm() { return this.todoForm.controls; }

  constructor(private todoService: TodoService, private changeDetection: ChangeDetectorRef) {

    this.todoView = new TodoView();
    
    this.todoForm = new FormGroup({
      name: new FormControl('', Validators.required),
      date: new FormControl('', Validators.required)
    });

    this.showModal = false;

    this.editForm = new FormGroup({
      id: new FormControl(''),
      name: new FormControl('', Validators.required),
      date: new FormControl('', Validators.required),
      completed: new FormControl('', Validators.required)
    });
  }

  ngOnInit(): void {

    this.todoService.GetTodoList().subscribe((data) => {

      let view = data as any;

      view.overdueTasks.forEach((t: TodoItem) => {
        var todo = new TodoItem(t.id, t.name, t.date, StatusEnum.Overdue);
        this.todoView.OverdueTasks.push(todo);
      });

      view.pendingTasks.forEach((t: TodoItem) => {
        var todo = new TodoItem(t.id, t.name, t.date, StatusEnum.Pending);
        this.todoView.PendingTasks.push(todo);
      });

    }); 
  }

  createTodo(todo: TodoItem): void {
    this.todoService.CreateTodo(todo).subscribe((data: TodoItem) => {
      if (data.status == StatusEnum.Pending) {
        this.todoView.PendingTasks.push(new TodoItem(data.id, data.name, data.date, data.status))
      }
      else {
        this.todoView.OverdueTasks.push(new TodoItem(data.id, data.name, data.date, data.status))
      }
    })       
  }

  updateTodo(todo: TodoItem): void {
   this.todoService.UpdateTodo(todo).subscribe((data: TodoItem) => {
    if (data.status == StatusEnum.Pending) {

      var existingTodo = this.todoView.PendingTasks.find(this.findIndexToUpdate, data.id) as TodoItem;

      if (existingTodo != undefined) {  // Move Todo to other table  and clear from existing one
        
        var index = this.todoView.PendingTasks.findIndex(item => item.id == data.id);

        existingTodo.name = data.name;
        existingTodo.date = data.date;
        existingTodo.status = data.status;

        if (index > -1) {
          this.todoView.PendingTasks[index] = existingTodo;
        }
      }
      else {
        // Update existing table
        existingTodo = new TodoItem(data.id, data.name, data.date, data.status);

        this.todoView.PendingTasks.push(existingTodo);

        // Remove from old table
        var index = this.todoView.OverdueTasks.findIndex(item => item.id == data.id);
        if (index > -1) {
          this.todoView.OverdueTasks.splice(index, 1);
        }
      }
    }
    else if (data.status == StatusEnum.Overdue) {
      var existingTodo = this.todoView.OverdueTasks.find(this.findIndexToUpdate, data.id) as TodoItem;

      if (existingTodo != undefined) {  // Update current table
        
        var index = this.todoView.OverdueTasks.findIndex(item => item.id == data.id);

        existingTodo.name = data.name;
        existingTodo.date = data.date;
        existingTodo.status = data.status;

        if (index > -1) {
          this.todoView.OverdueTasks[index] = existingTodo;
        }
      }
      else {
        // Update existing table
        existingTodo = new TodoItem(data.id, data.name, data.date, data.status);

        this.todoView.OverdueTasks.push(existingTodo);

        // Remove from old table
        var index = this.todoView.PendingTasks.findIndex(item => item.id == data.id);
        if (index > -1) {
          this.todoView.PendingTasks.splice(index, 1);
        }
      }
    }
    else if (data.status == StatusEnum.Done) { // Find record and remove it from tables
      
      var overdueIndex = this.todoView.OverdueTasks.findIndex(item => item.id == data.id);
      if (overdueIndex > -1) {
        this.todoView.OverdueTasks.splice(overdueIndex, 1 );
      }

      var pendingIndex = this.todoView.PendingTasks.findIndex(item => item.id == data.id);
      if (pendingIndex > -1) {
        this.todoView.PendingTasks.splice(pendingIndex, 1);
      }
        
    }

    this.changeDetection.detectChanges();
    }) 
  }

  onCreate(form: FormGroup) {

    if (form.valid) {

      var status = StatusEnum.Pending;

      if (new Date() > new Date(form.value.date)) {
        status = StatusEnum.Overdue
      }

      var todo = new TodoItem(-1, form.value.name, form.value.date, status);

      this.createTodo(todo);
    }
  }

  onEdit(form: FormGroup) {

    if (this.editForm.valid) {

      this.submitted = true;

      var status = StatusEnum.Pending;

      if (new Date() > new Date(form.value.date)) {
        status = StatusEnum.Overdue
      }

      if (form.value.completed == true) {
        status = StatusEnum.Done
      }

      var todo = new TodoItem(form.value.id, form.value.name, form.value.date, status);

      this.updateTodo(todo);
    }
    if (this.submitted) {
      this.showModal = false;
      this.submitted = false;
    }
  }

  editPendingTodo(id: number) {
    var todo = this.todoView.PendingTasks.find(this.findIndexToUpdate, id) as TodoItem;

    if (todo != null) {

      this.showModal = true;

      this.editForm = new FormGroup({
        id: new FormControl(todo.id),
        name: new FormControl(todo.name, Validators.required),
        date: new FormControl(formatDate(todo.date, 'yyyy-MM-dd', 'en'), Validators.required),
        completed: new FormControl('')
      });
    }
  }

  editOverdueTodo(id: number) {
    var todo = this.todoView.OverdueTasks.find(this.findIndexToUpdate, id) as TodoItem;

    if (todo != null) {

      this.showModal = true;

      this.editForm = new FormGroup({
        id: new FormControl(todo.id),
        name: new FormControl(todo.name, Validators.required),
        date: new FormControl(formatDate(todo.date, 'yyyy-MM-dd', 'en'), Validators.required),
        completed: new FormControl('')
      });
    }
  }

  show() {
    this.showModal = true;
  }

  hide() {
    this.showModal = false;
  }

  findIndexToUpdate(newItem: any) {
    return newItem.id === this;
  }
}

