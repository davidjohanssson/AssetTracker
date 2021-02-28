import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "src/environments/environment";
import { Brand } from "./brand";
import { BrandFilter } from "./brand.filter";

@Injectable({
    providedIn: 'root'
})
export class BrandHttp {

    constructor(
        private http: HttpClient
    ) {}

    async search(filter?: BrandFilter) {
        return await this.http.post<[Brand[], number]>(`${environment.api.baseUrl}/brand/search`, filter ?? {}).toPromise();
    }
}