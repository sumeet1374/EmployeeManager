import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DepartmentService {
  private departments:{ id:number, name:string}[] = [];
  constructor() {
    this.departments.push({ id:1,name:"Finance"});
    this.departments.push({ id:2,name:"HR"});
    this.departments.push({ id:3,name:"Engineering"});
    this.departments.push({ id:4,name:"Quality"});
  }

  findDepartments():Observable<{id:number,name:string}[]>{
    return of([...this.departments]);
  }

}
