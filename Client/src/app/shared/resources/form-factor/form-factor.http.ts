import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "src/environments/environment";
import { FormFactor } from "./form-factor";
import { FormFactorFilter } from "./form-factor.filter";

@Injectable({
    providedIn: 'root'
})
export class FormFactorHttp {

    constructor(
        private http: HttpClient
    ) {}

    async search(filter?: FormFactorFilter) {
        return await this.http.post<[FormFactor[], number]>(`${environment.api.baseUrl}/formfactor/search`, filter ?? {}).toPromise();
    }
}