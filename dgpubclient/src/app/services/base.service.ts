import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Observable, throwError } from "rxjs";

export abstract class BaseService {
  protected UrlServiceV1: string = "https://localhost:44343/";

    protected getHeaderJson(){
        return {
            headers: new HttpHeaders({
                'Content-Type': 'application/json'
            })
        };
    }

    protected getAuthHeaderJson(){
        return {
            headers: new HttpHeaders({
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${this.getUserToken()}`
            })
        };
    }

    public getUser() {
        return JSON.parse(localStorage.getItem('dgpub.user'));
    }

    protected getUserToken(): string {
        return localStorage.getItem('dgpub.token');
    }

    protected extractData(response: any){
        return response.data || {};
    }

    protected serviceError(response: Response | any){
        let errMsg: string;

        if (response instanceof Response) {

            errMsg = `${response.status} - ${response.statusText || ''}`;
        }
        else {
            errMsg = response.message ? response.message : response.error.errors.toString();
        }
        console.error(response);
        return throwError(response);
    }
}