import { Injectable } from '@angular/core';
import { Employee } from './employee.model';
import { of, Observable, throwError, pipe, Subject } from 'rxjs';
import { map, tap } from 'rxjs/operators';
import {  environment } from '../environments/environment';
import {HttpClient, HttpParams} from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

  private apiAll:string = environment.serviceBastUrl + "api/Employee/All";
  private apiById:string = environment.serviceBastUrl + "api/Employee/";
  private saveUrl:string = environment.serviceBastUrl + "api/Employee/";
  private deleteUrl:string = environment.serviceBastUrl + "api/Employee/";
  employeeList:Employee[] = [];
  public employeeDataChanged:Subject<any> = new Subject();
  constructor(private httpClient:HttpClient) {
      // this.populateEmployees();
   }

   public saveEmployee(employee:Employee){
     const url = this.saveUrl;
      if(employee.id == 0) {
        return this.httpClient.post(url,employee);
      }else{
        return this.httpClient.put(url,employee);
      }
   }

   public findEmployees(pageNumber:number){
    // const url =

      let params = new HttpParams();
      params = params.append('pageNumber', pageNumber.toString());
      params = params.append('pageSize', environment.pageSize.toString());

      return this.httpClient.get<any>(this.apiAll,{params:params}).pipe(
        map((data) => {
          return {
            employees: data.result,
            totalRecords: data.rowCount,
            pageNumber: pageNumber,
          };
        })
      );
   }

   public findEmployeeById<Employee>(id:number){
    const url = this.apiById + id.toString();
    console.log(url);
    return this.httpClient.get<Employee>(url);
   }

   public deactivateEmployee(id:number){
     let httpParams = new HttpParams().set("id",id.toString());
     return this.httpClient.delete(this.deleteUrl,{ params:httpParams})
     .pipe(tap((data)=> {
        this.employeeDataChanged.next(null);
     }));
   }
}
