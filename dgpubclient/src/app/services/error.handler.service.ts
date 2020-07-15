import { Injectable } from "@angular/core";
import { HttpInterceptor, HttpHandler, HttpEvent, HttpRequest, HttpErrorResponse } from "@angular/common/http";
import { Router } from "@angular/router";

import { map, catchError } from 'rxjs/operators';
import { Observable, throwError } from 'rxjs';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

    constructor(private router: Router){}

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        
        return next.handle(req).pipe(
            catchError(
                err => {
            
            if(err instanceof HttpErrorResponse) {
                if(err.status === 401){
                    localStorage.removeItem('dgpub.token');
                    localStorage.removeItem('dgpub.user');                  
                }
                this.router.navigate(['/register']);
            }

            return throwError(err);
        }));
    }    
}