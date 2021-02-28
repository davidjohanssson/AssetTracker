import { Injectable } from "@angular/core";
import { MatPaginator } from "@angular/material/paginator";
import { BehaviorSubject } from "rxjs";
import { Asset } from "./asset";
import { AssetFilter } from "./asset.filter";

@Injectable({
    providedIn: 'root'
})
export class AssetState {
    store$ = new BehaviorSubject<[Asset[], number]>([[], 0]);
    loading$ = new BehaviorSubject<boolean>(true);
    filter$ = new BehaviorSubject<AssetFilter>(null);
    paginator: MatPaginator;
}