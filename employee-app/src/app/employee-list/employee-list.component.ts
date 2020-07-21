import { Component, OnInit, OnDestroy } from '@angular/core';
import {Employee} from '../employee.model';
import { ActivatedRoute, Router } from '@angular/router';
import { environment } from '../../environments/environment';
import { Subscription } from 'rxjs';
import { EmployeeService } from '../employee.service';

@Component({
  selector: 'app-employee-list',
  templateUrl: './employee-list.component.html',
  styleUrls: ['./employee-list.component.scss']
})
export class EmployeeListComponent implements OnInit,OnDestroy {

  employeeList:Employee[] = [];
  p: number = 1;
  totalRecords:number;
  pageSize:number;
  employeeChanged:Subscription;

  constructor(private route:ActivatedRoute,private router:Router,
    private employeeService:EmployeeService
    ) { }
  ngOnDestroy(): void {
    this.employeeChanged.unsubscribe();
  }

  ngOnInit(): void {
    this.employeeChanged = this.employeeService.employeeDataChanged.subscribe((data)=> {
      this.employeeService.findEmployees(1).subscribe((data)=> {
        this.employeeList = data.employees;
        this.totalRecords = data.totalRecords;
        this.p = data.pageNumber;
      })
    });

    this.pageSize =environment.pageSize;
      this.route.data.subscribe((data:{ employees:{ employees:Employee[],totalRecords:number,pageNumber:number}})=>{
        this.employeeList = data.employees.employees;
        this.totalRecords = data.employees.totalRecords;
        this.p = data.employees.pageNumber;

      })
  }

  onPageChange(page){
    this.router.navigate(["employees",page]);
  }

  onDeactivateEmployee(id:number){
    this.employeeService.deactivateEmployee(id).subscribe(()=> {
        console.log("Employee deactivated");
    });
  }

}
