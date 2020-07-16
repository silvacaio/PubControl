import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { BaseService } from "../../services/base.service";
import { Observable } from "rxjs";
import { map, catchError } from 'rxjs/operators';
import { Item } from '../models/item';

@Injectable()
export class ItemService extends BaseService {
    constructor(private http: HttpClient) { super(); }

    getAll(): Observable<Item[]> {
      
        let response = this.http
            .get(this.UrlServiceV1 + "api/items")
            .pipe(map(super.extractData))
            .pipe(catchError(super.serviceError));
  
        return response;
    };
}