import { Injectable } from "@angular/core";
import { BehaviorSubject } from "rxjs";
import { Currency } from "./currency";

@Injectable({
    providedIn: 'root'
})
export class CurrencyStore {
    latestSearch$ = new BehaviorSubject<[Currency[], number]>(null);
}