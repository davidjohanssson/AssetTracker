import { Injectable } from "@angular/core";
import { MatPaginator } from "@angular/material/paginator";
import { BehaviorSubject } from "rxjs";
import { Currency } from "./currency";
import { CurrencyFilter } from "./currency.filter";

@Injectable({
    providedIn: 'root'
})
export class CurrencyState {
    store$ = new BehaviorSubject<[Currency[], number]>([[], 0]);
    loading$ = new BehaviorSubject<boolean>(true);
    filter$ = new BehaviorSubject<CurrencyFilter>(null);
    paginator: MatPaginator;
    selectedCurrency$ = new BehaviorSubject<Currency>(null);
}