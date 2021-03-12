import { Injectable } from '@angular/core';
import {
  HttpInterceptor,
  HttpRequest,
  HttpHandler,
  HttpEvent
} from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class HttpLogInterceptor implements HttpInterceptor {

  intercept( request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    console.log(request);

    return next.handle(request);
  }

}