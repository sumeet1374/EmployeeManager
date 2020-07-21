import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Employee } from '../employee.model';
import { dateForCalender} from '../Util/formatDate'
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { EmployeeAddress } from '../address.model';
import { ngbDateToString} from '../Util/ngbDateUtil'
import { EmployeeService } from '../employee.service';
import { NgbDate, NgbDatepicker } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-employee-edit',
  templateUrl: './employee-edit.component.html',
  styleUrls: ['./employee-edit.component.scss']
})
export class EmployeeEditComponent implements OnInit {
  formLoaded:boolean = false;
  public newRecord:boolean = false;
  public departments:{id:number,name:string}[] = [];
  public selectedEmployee:Employee;
  public employeeEditForm:FormGroup = new FormGroup({});
  @ViewChild("bd") birthDatePicker:NgbDatepicker;

  public hideGeneral = false;
  public hideAddress = true;
  public activeGeneral = true;
  public activeAddress = false;
  constructor(private route:ActivatedRoute,
              private employeeService:EmployeeService,
              private router:Router) {
   }

  toggleTab(tab:string){
    if(tab === "general" && this.activeGeneral)
        return;

    if(tab === "address" && this.activeAddress)
      return;

      this.hideGeneral = !this.hideGeneral;
      this.hideAddress = !this.hideAddress;
      this.activeGeneral = !this.activeGeneral;
      this.activeAddress = !this.activeAddress;

  }


  private populateAddres(addressType:string):EmployeeAddress
  {
      const formGrp = this.employeeEditForm.get(addressType);
      const address:EmployeeAddress = {
        id:0,
        employeeId:this.selectedEmployee.id,
        addressLine1:formGrp.value.addressLine1,
        addressLine2:formGrp.value.addressLine2,
        city:formGrp.value.city,
        state:formGrp.value.state,
        pinCode:formGrp.value.pinCode,

      };

      if(addressType === "currentAddress"){
        address.id = this.selectedEmployee.currentAddress.id;
      }

      if(addressType === "permanentAddress"){
        address.id = this.selectedEmployee.permanentAddress.id;
      }

      return address;

  }
  private populateModel():Employee
  {
    const emp:Employee = {
      id:this.selectedEmployee.id,
      firstName:this.employeeEditForm.value.firstName,
      middleName:this.employeeEditForm.value.middleName,
      lastName:this.employeeEditForm.value.lastName,
      email:this.employeeEditForm.value.email,
      gender:this.employeeEditForm.value.gender,
      departmentId: this.employeeEditForm.value.department,
      dateOfBirth:ngbDateToString(this.employeeEditForm.value.dateOfBirth),
      joiningDate:ngbDateToString(this.employeeEditForm.value.joiningDate),
      separationDate:ngbDateToString(this.employeeEditForm.value.separationDate),
      currentAddress:this.populateAddres("currentAddress"),
      permanentAddress:this.populateAddres("permanentAddress")

    }
    return emp;
  }

  private BuildForm(employee:Employee){
      this.employeeEditForm = new FormGroup({
        firstName:new FormControl(employee?employee.firstName:"",[Validators.required]),
        middleName:new FormControl(employee?employee.middleName:""),
        lastName:new FormControl(employee?employee.lastName:"",[Validators.required]),
        gender:new FormControl(employee?employee.gender:"M",[Validators.required]),
        email:new FormControl(employee?employee.email:"",[Validators.email]),
        department: new FormControl(employee && employee.departmentId > 0?employee.departmentId:"",[Validators.required]),
        dateOfBirth: new FormControl(employee && employee.dateOfBirth?dateForCalender(new Date(employee.dateOfBirth)):null,[Validators.required]),
        joiningDate: new FormControl(employee && employee.joiningDate?dateForCalender(new Date(employee.joiningDate)):null,[Validators.required]),
        separationDate: new FormControl(employee && employee.separationDate?dateForCalender(new Date(employee.separationDate)):null),
        currentAddress:new FormGroup({
          addressLine1:new FormControl(employee && employee.currentAddress? employee.currentAddress.addressLine1:"",[Validators.required]),
          addressLine2:new FormControl(employee && employee.currentAddress? employee.currentAddress.addressLine2:""),
          city:new FormControl(employee && employee.currentAddress? employee.currentAddress.city:"",[Validators.required]),
          state:new FormControl(employee && employee.currentAddress? employee.currentAddress.state:"",[Validators.required]),
          pinCode:new FormControl(employee && employee.currentAddress? employee.currentAddress.pinCode:"",[Validators.required,Validators.pattern(/^[1-9][0-9]+$/)]),
        }),
        permanentAddress:new FormGroup({
          addressLine1:new FormControl(employee && employee.permanentAddress? employee.permanentAddress.addressLine1:"",[Validators.required]),
          addressLine2:new FormControl(employee && employee.permanentAddress? employee.permanentAddress.addressLine2:""),
          city:new FormControl(employee && employee.permanentAddress? employee.permanentAddress.city:"",[Validators.required]),
          state:new FormControl(employee && employee.permanentAddress? employee.permanentAddress.state:"",[Validators.required]),
          pinCode:new FormControl(employee && employee.permanentAddress? employee.permanentAddress.pinCode:"",[Validators.required,Validators.pattern(/^[1-9][0-9]+$/)]),
        })
      })
  }
  ngOnInit(): void {
    this.formLoaded = false;
    this.route.data.subscribe((data:{ departments:{id:number,name:string}[],
      selectedEmployee:Employee})=> {
      this.departments = data.departments;
      const id = data.selectedEmployee.id;
      this.newRecord = (id===0);
      const selectedEmployee = data.selectedEmployee;
      this.selectedEmployee = selectedEmployee;
      this.BuildForm(selectedEmployee);
      this.formLoaded = true;
    });
    this.birthDatePicker.minDate = this.minDate;
  }

  onSubmit(){
    const model = this.populateModel();
    console.log(model);
    this.employeeService.saveEmployee(model).subscribe((data)=> {
      this.router.navigate(['employees']);
    },(error)=>{
      this.router.navigate(['error']);
    })

  }

  // Datepicker Properties

  get minDate():NgbDate{
    return new NgbDate(1900,1,1);
  }

  get maxBirthDate():NgbDate {

    let dt = new Date();

    return new NgbDate(dt.getFullYear(),(dt.getMonth() + 1),dt.getDate());
  }

  // Form Properties
  get firstName(){
    return this.employeeEditForm.get("firstName");
  }
  get lastName(){
    return this.employeeEditForm.get("lastName");
  }

  get middleName(){
    return this.employeeEditForm.get("middleName");
  }

  get gender(){
    return this.employeeEditForm.get("gender");
  }

  get email(){
    return this.employeeEditForm.get("email");
  }

  get department(){
    return this.employeeEditForm.get("department");
  }

  get dateOfBirth(){
    return this.employeeEditForm.get("dateOfBirth");


  }

  get joiningDate(){
    return this.employeeEditForm.get("joiningDate");
  }

  get perAddressLine1(){
   return  this.employeeEditForm.get("permanentAddress").get("addressLine1");

  }

  get perCity(){
    return  this.employeeEditForm.get("permanentAddress").get("city");
   }

   get perState(){
    return  this.employeeEditForm.get("permanentAddress").get("state");
   }

   get perPinCode(){
    return  this.employeeEditForm.get("permanentAddress").get("pinCode");
   }

   get curAddressLine1(){
    return  this.employeeEditForm.get("currentAddress").get("addressLine1");
   }

   get curCity(){
     return  this.employeeEditForm.get("currentAddress").get("city");
    }

    get curState(){
     return  this.employeeEditForm.get("currentAddress").get("state");
    }

    get curPinCode(){
     return  this.employeeEditForm.get("currentAddress").get("pinCode");
    }




}
