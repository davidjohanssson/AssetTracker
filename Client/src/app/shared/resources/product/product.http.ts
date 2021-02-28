import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "src/environments/environment";
import { Product } from "./product";
import { ProductFilter } from "./product.filter";

@Injectable({
    providedIn: 'root'
})
export class ProductHttp {

    constructor(
        private http: HttpClient
    ) {}

    async search(filter?: ProductFilter) {
        return await this.http.post<[Product[], number]>(`${environment.api.baseUrl}/product/search`, filter ?? {}).toPromise();
    }
}