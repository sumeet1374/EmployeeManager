import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class Base64utilService {
  constructor() {}

  public  arrayBufferToBase64(buffer:ArrayBuffer):string {
    let binary = '';
    let bytes = new Uint8Array(buffer);
    let len = bytes.byteLength;
    for (let i = 0; i < len; i++) {
        binary += String.fromCharCode(bytes[i]);
    }
    return window.btoa(binary);
  }
  public converBase64toBlob(content: string, contentType: string): Blob {
    let sliceSize = 512;
    let byteCharacters = window.atob(content); //method which converts base64 to binary
    let byteArrays = [];
    for (var offset = 0; offset < byteCharacters.length; offset += sliceSize) {
      var slice = byteCharacters.slice(offset, offset + sliceSize);
      var byteNumbers = new Array(slice.length);
      for (var i = 0; i < slice.length; i++) {
        byteNumbers[i] = slice.charCodeAt(i);
      }
      var byteArray = new Uint8Array(byteNumbers);
      byteArrays.push(byteArray);

    }
    var blob = new Blob(byteArrays, {
      type: contentType,
    }); //statement which creates the blob
    return blob;
  }
}
