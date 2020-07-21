import { Resolve, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { DepartmentService } from '../department.service';
import { Injectable } from '@angular/core';

@Injectable({ providedIn:'root' })
export class DepartmentResolver implements Resolve<{ id:number,name:string}[]> {

  constructor(private departmentService:DepartmentService){

  }
  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): { id: number; name: string; }[] | import("rxjs").Observable<{ id: number; name: string; }[]> | Promise<{ id: number; name: string; }[]> {
    return this.departmentService.findDepartments();
  }



}
