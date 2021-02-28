import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "src/environments/environment";
import { Office } from "./office";
import { OfficeFilter } from "./office.filter";

@Injectable({
    providedIn: 'root'
})
export class OfficeHttp {

    constructor(
        private http: HttpClient,
    ) {}

    async search(filter?: OfficeFilter) {
        return await this.http.post<[Office[], number]>(`${environment.api.baseUrl}/office/search`, filter ?? {}).toPromise();
    }
}