import { EmployeeAddress } from "./address.model";

export interface Employee {
  id:number,
  firstName:string,
  lastName:string,
  middleName:string,
  gender:string,
  email:string,
  dateOfBirth:string,
  joiningDate:string,
  separationDate:string,
  currentAddress:EmployeeAddress,
  permanentAddress:EmployeeAddress,
  departmentId:number
}
