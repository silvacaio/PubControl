import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";

import { User } from "../user/models/user";
import { BaseService } from "./base.service";
import { Observable } from "rxjs";
import { map, catchError } from 'rxjs/operators';

@Injectable()
export class UserService extends BaseService {

  constructor(private http: HttpClient){ super() }    

    register(user: User) : Observable<User> {

        let response = this.http
            .post(this.UrlServiceV1 + "new", user, super.getHeaderJson())
            .pipe(map(super.extractData))
            .pipe(catchError(super.serviceError));   

        return response;
    }
    
    login(user: User) : Observable<User> {
        let response = this.http
            .post(this.UrlServiceV1 + "/login", user, super.getHeaderJson())
            .pipe(map(super.extractData))
            .pipe(catchError(super.serviceError));   

        return response;
    }
}
