import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "src/environments/environment";
import { ResourceChange } from "../resource.change";
import { Asset } from "./asset";
import { AssetFilter } from "./asset.filter";
import { AssetState } from "./asset.state";

@Injectable({
    providedIn: 'root'
})
export class AssetHttp {

    constructor(
        private http: HttpClient,
        private assetState: AssetState,
        private resourceChange: ResourceChange,
    ) {}

    async search(filter?: AssetFilter) {
        this.assetState.loading$.next(true);
        this.assetState.store$.next([[], 0]);
        this.assetState.filter$.next(filter);
        const filtered = await this.http.post<[Asset[], number]>(`${environment.api.baseUrl}/asset/search`, filter ?? {}).toPromise();
        this.assetState.store$.next(filtered);
        this.assetState.loading$.next(false);
        return filtered;
    }

    async create(dto: Asset) {
        const asset = await this.http.post<Asset>(`${environment.api.baseUrl}/asset`, dto).toPromise();
        this.resourceChange.assets$.next();
        return asset;
    }

    async update(id: number, dto: Asset) {
        const asset = await this.http.put<Asset>(`${environment.api.baseUrl}/asset/${id}`, dto).toPromise();
        this.resourceChange.assets$.next();
        return asset;
    }

    async delete(id: number) {
        await this.http.delete<void>(`${environment.api.baseUrl}/asset/${id}`).toPromise();
        this.resourceChange.assets$.next();
    }
}