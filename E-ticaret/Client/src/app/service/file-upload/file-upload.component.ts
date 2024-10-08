import { Component, Input } from '@angular/core';
import { NgxFileDropEntry } from 'ngx-file-drop';
import { HttpClientService } from '../http-client.service';
import { HttpErrorResponse, HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-file-upload',
  templateUrl: './file-upload.component.html',
  styleUrls: ['./file-upload.component.css']
})
export class FileUploadComponent {

  constructor(
    private httpClient:HttpClientService
  ){}

  public files: NgxFileDropEntry[];
  @Input() options: Partial<FileUploadOptions>;

  public dropped(files: NgxFileDropEntry[]){
    this.files = files;
    
    for(const droppedFile of files){
      if(droppedFile.fileEntry.isFile){
        const fileEntry = droppedFile.fileEntry as FileSystemFileEntry;
        const fileData: FormData = new FormData();
        fileEntry.file((file: File) =>{
          fileData.append(file.name, file, droppedFile.relativePath)
          console.log(droppedFile.relativePath, file);
        });

        this.httpClient.post({
          controller: this.options.controller,
          action: this.options.action,
          queryString: this.options.queryString,
          headers: new HttpHeaders({"responseType": "blob"})
        }, fileData).subscribe(data =>{

        }, (errorResponse: HttpErrorResponse)=>{
          console.log(errorResponse.message);
        });

      } 
      else{
        const fileEntry = droppedFile.fileEntry as FileSystemDirectoryEntry;
        console.log(droppedFile.relativePath, fileEntry);
      }
    }
  }

  public fileOver(event){
    console.log(event);
  }

  public fileLeave(event){
    console.log(event);
  }


}

export class FileUploadOptions
{
  controller?: string;
  action?: string;
  queryString?: string;
  explanation?: string;
  accept?: string;
}
