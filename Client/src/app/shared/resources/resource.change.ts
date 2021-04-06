import { Injectable } from "@angular/core";
import { Subject } from "rxjs";

@Injectable({
    providedIn: 'root'
})
// These observables should emit if its entity has changed through one of the following HTTP operations, POST, PUT or DELETE.
// This is important because we want to notify any component that's currently presenting data about it, that the data has changed.
export class ResourceChange {
    currencies$ = new Subject<void>();
    assets$ = new Subject<void>();
}