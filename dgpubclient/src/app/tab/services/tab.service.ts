import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { BaseService } from "../../services/base.service";
import { Observable } from "rxjs";
import { Tab } from "../models/tab";
import { map, catchError } from 'rxjs/operators';
import { ItemTab } from "../models/itemTab";
import { CreateTab } from "../models/createTab";
import { EditTab } from "../models/editTab";
import { ClosedTab } from "../models/closedTab";

@Injectable()
export class TabService extends BaseService {
    constructor(private http: HttpClient) { super(); }

    create(create: CreateTab): Observable<Tab> {

        let response = this.http
            .post(this.UrlServiceV1 + "api/tab", create, super.getAuthHeaderJson())
            .pipe(map(super.extractData))
            .pipe(catchError(super.serviceError));

        return response;
    };

    get(id: String): Observable<Tab> {

        let response = this.http
            .get(this.UrlServiceV1 + "api/tab/" + id, super.getAuthHeaderJson())
            .pipe(map(super.extractData))
            .pipe(catchError(super.serviceError));

        return response;
    };

    addItem(item: ItemTab): Observable<Tab> {

        let response = this.http
            .put(this.UrlServiceV1 + "api/tab/addItem", item, super.getAuthHeaderJson())
            .pipe(map(super.extractData))
            .pipe(catchError(super.serviceError));

        return response;
    };

    close(tab: EditTab): Observable<ClosedTab> {

        let response = this.http
            .put(this.UrlServiceV1 + "api/tab/close", tab, super.getAuthHeaderJson())
            .pipe(map(super.extractData))
            .pipe(catchError(super.serviceError));

        return response;
    };

    reset(tab: EditTab): Observable<Tab> {

        let response = this.http
            .put(this.UrlServiceV1 + "api/tab/reset", tab, super.getAuthHeaderJson())
            .pipe(map(super.extractData))
            .pipe(catchError(super.serviceError));

        return response;
    };
}