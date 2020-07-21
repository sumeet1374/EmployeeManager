import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { EmployeeListComponent } from './employee-list/employee-list.component';
import { EmployeeResolver} from './employee-list/employee.resolver.service' ;
import { EmployeeEditComponent } from './employee-edit/employee-edit.component';
import { DepartmentResolver} from './employee-edit/department.resolver.service';
import { EmployeeEditResolver} from './employee-edit/employeeedit.resolver.service'
import {ErrorComponent} from './error/error.component';
import { UploadFileComponent } from './upload-file/upload-file.component';

const routes: Routes = [
  {path:"",component:EmployeeListComponent, resolve:{ "employees":EmployeeResolver}},
  {path:"employees",component:EmployeeListComponent, resolve:{ "employees":EmployeeResolver}},
  {path:"employees/:pageNumber",component:EmployeeListComponent, resolve:{ "employees":EmployeeResolver}},
  {path:"employeeedit/:id",component:EmployeeEditComponent,resolve:{ "departments":DepartmentResolver, "selectedEmployee":EmployeeEditResolver}},
  {path:"uploaddoc/:id",component:UploadFileComponent},
  {path:"error",component:ErrorComponent}

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
