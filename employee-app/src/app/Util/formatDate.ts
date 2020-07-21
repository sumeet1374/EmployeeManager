import { NgbDate } from '@ng-bootstrap/ng-bootstrap';

export const dateForCalender = (d:Date):NgbDate | null => {
  if(d){

    return new NgbDate(d.getFullYear(),(d.getMonth() +1),d.getDate());
  }else{
    return null;

  }





  // if(d){
  //   let month =  (d.getMonth() + 1).toString();
  //   let day =  d.getDate().toString();
  //   let  year = d.getFullYear().toString();
  //     if (month.length < 2)
  //         month = '0' + month;
  //     if (day.length < 2)
  //         day = '0' + day;
  //     const dateToReturn = [year, month, day].join('-');
  //     console.log(dateToReturn);
  //     return dateToReturn;
  // }

  // return "";

}
