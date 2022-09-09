export class TodoItem {
  Id: number | undefined;
  Name: string | undefined;
  Date: Date | undefined;
  Status: StatusEnum | undefined;

  constructor(
    public id: number,
    public name: string,
    public date: Date,
    public status: StatusEnum
  ) { }

}

export enum StatusEnum {
  Done,
  Pending,
  Overdue
}
