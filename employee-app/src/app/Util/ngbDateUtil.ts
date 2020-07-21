import { NgbDate } from '@ng-bootstrap/ng-bootstrap';

export const ngbDateToString = (d:NgbDate):string | null => {
      if(d) {
        let year:string  = d.year.toString();
        let month:string = d.month > 9?d.month.toString():("0"+ d.month.toString());
        let day:string = d.day > 9?d.day.toString():("0"+ d.day.toString());
        return year + "-" + month + "-" + day;
       // return new Date(d.year,(d.month -1),d.day).toString();
      }
  return null;
}
