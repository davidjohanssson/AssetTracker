import { LOCALE_ID, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MaterialModule } from './shared/modules/material/material.module';
import { ReactiveFormsModule } from '@angular/forms';
import { ViewComponent } from './view/view.component';
import { AssetComponent } from './view/asset/asset.component';
import { HttpClientModule } from '@angular/common/http';
import { SkeletonModule } from './shared/modules/skeleton/skeleton.module';
import { AssetSearchComponent } from './view/asset/asset-search/asset-search.component';
import { AssetTableComponent } from './view/asset/asset-table/asset-table.component';
import { registerLocaleData } from '@angular/common';
import localeSv from '@angular/common/locales/sv';
import { AddAssetComponent } from './view/asset/dialogs/add-asset/add-asset.component';
import { EditAssetComponent } from './view/asset/dialogs/edit-asset/edit-asset.component';

@NgModule({
  declarations: [
    AppComponent,
    ViewComponent,
    AssetComponent,
    AssetSearchComponent,
    AssetTableComponent,
    AddAssetComponent,
    EditAssetComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MaterialModule,
    ReactiveFormsModule,
    HttpClientModule,
    SkeletonModule,
  ],
  providers: [
    {
      provide: LOCALE_ID,
      useValue: 'sv-SE'
    },
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
  constructor() {
    registerLocaleData(localeSv);
  }
}
