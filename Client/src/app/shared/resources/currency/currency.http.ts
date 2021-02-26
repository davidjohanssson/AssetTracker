import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "src/environments/environment";
import { ResourceChange } from "../resource.change";
import { Currency } from "./currency";
import { CurrencyFilter } from "./currency.filter";
import { CurrencyState } from "./currency.state";

@Injectable({
    providedIn: 'root'
})
export class CurrencyHttp {

    constructor(
        private http: HttpClient,
        private currencyState: CurrencyState,
        private resourceChange: ResourceChange,
    ) {}

    async search(filter?: CurrencyFilter) {
        this.currencyState.loading$.next(true);
        this.currencyState.store$.next([[], 0]);
        this.currencyState.filter$.next(filter);
        const filtered = await this.http.post<[Currency[], number]>(`${environment.api.baseUrl}/currency/filter`, filter ?? {}).toPromise();
        this.currencyState.store$.next(filtered);
        this.currencyState.loading$.next(false);
        return filtered;
    }
}