import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "src/environments/environment";
import { ResourceChange } from "../resource.change";
import { Currency } from "./currency";
import { CurrencyFilter } from "./currency.filter";

@Injectable({
    providedIn: 'root'
})
export class CurrencyHttp {

    constructor(
        private http: HttpClient,
        private resourceChange: ResourceChange,
    ) {}

    async filter(filter?: CurrencyFilter) {
        return this.http.post<[Currency[], number]>(`${environment.api.baseUrl}/currency/filter`, filter ?? {}).toPromise();
    }
}