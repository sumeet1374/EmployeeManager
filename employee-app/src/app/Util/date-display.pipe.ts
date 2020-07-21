import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'dateDisplay'
})
export class DateDisplayPipe implements PipeTransform {

  transform(value: string, ...args: unknown[]): string {
    let returnValue = "";
    if(value){

      try{
        returnValue = new Date(value).toDateString();
      }
      catch{

      }
    }
     return returnValue;
  }

}
