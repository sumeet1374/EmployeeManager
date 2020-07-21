import {  Injectable } from "@angular/core";
import { Employee } from "../employee.model";
import { Resolve, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { EmployeeService } from "../employee.service";

@Injectable({providedIn:'root'})
export class EmployeeResolver implements Resolve<{ employees:Employee[],totalRecords:number,pageNumber:number}>{

  constructor(private employeeService:EmployeeService){

  }
  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): { employees:Employee[],totalRecords:number,pageNumber:number} | import("rxjs").Observable<{ employees:Employee[],totalRecords:number,pageNumber:number}> | Promise<{ employees:Employee[],totalRecords:number,pageNumber:number}> {
    let  page =  +route.paramMap.get("pageNumber");
    if(!page){
      page = 1;
    }

    return this.employeeService.findEmployees(page);
  }

}
