import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "src/environments/environment";
import { ResourceChanges } from "../resource.changes";
import { Currency } from "./currency";
import { CurrencyFilter } from "./currency.filter";

@Injectable({
    providedIn: 'root'
})
export class CurrencyHttp {

    constructor(
        private http: HttpClient,
        private resourceChanges: ResourceChanges,
    ) {}

    async filter(filter?: CurrencyFilter) {
        return this.http.post<[Currency[], number]>(`${environment.api.baseUrl}/currency`, filter ?? {}).toPromise();
    }
}