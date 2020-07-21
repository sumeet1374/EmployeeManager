import { Injectable } from '@angular/core';
import { HttpClient, HttpParams} from '@angular/common/http';
import { FileUpload} from './fileUpload.model';
import { environment} from '../environments/environment';
import { Subject } from 'rxjs';
import { tap } from 'rxjs/operators'

@Injectable({
  providedIn: 'root'
})
export class UploadfileService {

  public documentsUploaded:Subject<any> = new Subject();
  private uploadUrl:string = environment.serviceBastUrl + "api/Employee/UploadDoc";
  private documentListUrl:string = environment.serviceBastUrl + "api/Employee/Documents/";
  private documentRemoveUrl:string = environment.serviceBastUrl + "api/Employee/Documents/Remove";
  private documentDownloadUrl:string = environment.serviceBastUrl + "api/Employee/Documents/Download";

  constructor(private httpClient:HttpClient) { }

  uploadDocument(file:FileUpload){
   return this.httpClient.post(this.uploadUrl,file).pipe(tap(data=>{
      this.documentsUploaded.next(null);
   }));
  }

  findDocuments(id:number){
    const url = this.documentListUrl + id.toString();
    return this.httpClient.get(url);
  }

  downloadDocument(empId:number,documentId:number){
    const url = this.documentDownloadUrl;
    let httpParams = new HttpParams().set("empId",empId.toString());
        httpParams = httpParams.set("documentId",documentId.toString());
    return this.httpClient.get(url,{ params:httpParams});
  }

  removedDocument(empId:number,documentId:number){
    const url = this.documentRemoveUrl;
    let httpParams = new HttpParams().set("empId",empId.toString());
        httpParams = httpParams.set("documentId",documentId.toString());
    return this.httpClient.get(url,{ params:httpParams}).pipe(tap((data)=> {
      this.documentsUploaded.next(null);
    }));
  }
}
