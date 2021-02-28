import { Injectable } from "@angular/core";
import { Subject } from "rxjs";

@Injectable({
    providedIn: 'root'
})
export class ResourceChange {
    currencies$ = new Subject<void>();
    assets$ = new Subject<void>();
}