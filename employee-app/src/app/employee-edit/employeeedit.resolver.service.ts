import { Resolve, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { Injectable } from '@angular/core';
import { Employee} from '../employee.model';
import {EmployeeService} from '../employee.service';
import { map, catchError } from 'rxjs/operators';
import { of, throwError } from 'rxjs';


@Injectable({ providedIn:'root' })
export class EmployeeEditResolver implements Resolve<Employee> {

  constructor(private employeeService:EmployeeService,private router:Router){

  }
  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Employee | import("rxjs").Observable<Employee> | Promise<Employee> {
    let id= +route.paramMap.get("id");

    if(!Number.isNaN(id)){
      return this.employeeService.findEmployeeById(id);
    }
    else{
      this.router.navigate(['error']);
      return of(null);
    }

  }




}
