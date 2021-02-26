import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MaterialModule } from './shared/modules/material/material.module';
import { MAT_FORM_FIELD_DEFAULT_OPTIONS } from '@angular/material/form-field';
import { ReactiveFormsModule } from '@angular/forms';
import { ViewComponent } from './view/view.component';
import { AssetComponent } from './view/asset/asset.component';
import { ProductComponent } from './view/product/product.component';
import { HttpClientModule } from '@angular/common/http';
import { CurrencyComponent } from './view/currency/currency.component';
import { CurrencySearchComponent } from './view/currency/currency-search/currency-search.component';
import { CurrencyTableComponent } from './view/currency/currency-table/currency-table.component';
import { SkeletonModule } from './shared/modules/skeleton/skeleton.module';

@NgModule({
  declarations: [
    AppComponent,
    ViewComponent,
    AssetComponent,
    ProductComponent,
    CurrencyComponent,
    CurrencySearchComponent,
    CurrencyTableComponent
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
      provide: MAT_FORM_FIELD_DEFAULT_OPTIONS,
      useValue: { appearance: 'outline' },
    },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
