import { Component, OnInit, ViewChild, ElementRef,OnDestroy ,AfterViewInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FileUpload} from '../fileUpload.model';
import { UploadfileService} from '../uploadfile.service'
import {Base64utilService} from '../Util/base64util.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-upload-file',
  templateUrl: './upload-file.component.html',
  styleUrls: ['./upload-file.component.scss']
})
export class UploadFileComponent implements OnInit,AfterViewInit,OnDestroy  {

   @ViewChild("empdoc") uploadElement:ElementRef;
   fileReader:FileReader = new FileReader();
   file:any;
   private empId:number;
   private filesChanged:Subscription;
   public documents:any;
   public documentsLoaded:boolean = false;


  constructor(private activatedRoute:ActivatedRoute,
    private router:Router,
    private uploadFileService:UploadfileService,
    private base64utilService:Base64utilService) { }


  ngOnDestroy(): void {
    this.filesChanged.unsubscribe();
  }
  ngAfterViewInit(): void {
    this.uploadElement.nativeElement.onchange = ()=> {
        this.file = this.uploadElement.nativeElement.files[0];
        console.log(this.file);

    }

    this.fileReader.onload = (event)=> {
      const result = event.target.result as ArrayBuffer;
      const fileData = this.base64utilService.arrayBufferToBase64(result);

      // Construct object
      const dataToUpload:FileUpload = {
        id:0,
        employeeId:this.empId,
        fileName:this.file.name,
        type:this.file.type,
        fileStream:fileData
      }
      this.file = null;
      this.uploadFileService.uploadDocument(dataToUpload).subscribe(
        (data)=> {
         // this.router.navigate(['employees']);
        },
        (error)=> {
          console.log(error);
          this.router.navigate(['error']);
        }
      )

      console.log(dataToUpload);
    }
  }

  private getUploadedFiles(){
    this.uploadFileService.findDocuments(this.empId).subscribe((data)=> {
      this.documents = data;
      this.documentsLoaded = true;
    })


  }

  ngOnInit(): void {
    const id =  +this.activatedRoute.snapshot.paramMap.get("id");
    if(Number.isNaN(id)){

      this.router.navigate(['error']);
    }
    this.empId = id;
    this.getUploadedFiles();
    this.filesChanged = this.uploadFileService
      .documentsUploaded.subscribe((data)=> {
        this.getUploadedFiles();
    })

  }

  onUpload(){
    if(this.file){
      this.fileReader.readAsArrayBuffer(this.file);
    }
  }

  onDowload(empId:number,docId:number){
      this.uploadFileService.downloadDocument(empId,docId).subscribe((data:any)=> {
        console.log(data.fileName);
        console.log(data.type);
        const blob = this.base64utilService.converBase64toBlob(data.fileContent,data.type);
        var blobUrl = URL.createObjectURL(blob);
        window.open(blobUrl);
      })
  }

  onDelete(empId:number,docId:number){
    this.uploadFileService.removedDocument(empId,docId).subscribe(()=> {
      console.log("Document Removed");
    });
  }

}
