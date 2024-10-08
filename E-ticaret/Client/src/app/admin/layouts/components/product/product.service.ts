import { HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Product } from 'src/app/models/product';
import { HttpClientService } from 'src/app/service/http-client.service';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

constructor(private httpClient: HttpClientService) { }


add(product: Product, successCallBack?: ()=> void, errorCallBack?: (errorMessage: string) => void )
{
  this.httpClient.post({
    controller: "Product",
    action:"Add"
  },product).subscribe(result =>{
    successCallBack();
  }, (errorResponse: HttpErrorResponse)=>{
    const _error: Array<{key: string, value: Array<string>}> = errorResponse.error;
    let message ="";
    _error.forEach((v,index)=>{
      v.value.forEach((_v, index)=>{
        message += `${_v}<br>`;
      });
    });
    errorCallBack(message);
  });
}


}
