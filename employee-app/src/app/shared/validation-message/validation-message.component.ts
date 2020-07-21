import { Component, OnInit, Input } from '@angular/core';
import { FormControl, FormGroup, FormArray } from '@angular/forms';

@Component({
  selector: 'app-validation-message',
  templateUrl: './validation-message.component.html',
  styleUrls: ['./validation-message.component.scss']
})
export class ValidationMessageComponent implements OnInit {

  @Input() control:FormControl | FormGroup | FormArray;
  @Input() validators:any;

  keys:string[];

  constructor() { }

  ngOnInit(): void {

    this.keys = Object.keys(this.validators);
  }

}
