import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { debounceTime } from 'rxjs/operators';
import { AssetFilter } from 'src/app/shared/resources/asset/asset.filter';
import { AssetHttp } from 'src/app/shared/resources/asset/asset.http';
import { AssetState } from 'src/app/shared/resources/asset/asset.state';
import { Brand } from 'src/app/shared/resources/brand/brand';
import { BrandHttp } from 'src/app/shared/resources/brand/brand.http';
import { FormFactor } from 'src/app/shared/resources/form-factor/form-factor';
import { FormFactorHttp } from 'src/app/shared/resources/form-factor/form-factor.http';
import { Office } from 'src/app/shared/resources/office/office';
import { OfficeHttp } from 'src/app/shared/resources/office/office.http';
import { Product } from 'src/app/shared/resources/product/product';
import { ProductHttp } from 'src/app/shared/resources/product/product.http';

@UntilDestroy()
@Component({
  selector: 'app-asset-search',
  templateUrl: './asset-search.component.html',
  styleUrls: ['./asset-search.component.scss']
})
export class AssetSearchComponent implements OnInit {
  assetForm: FormGroup;
  brandsAndCount: [Brand[], number] = [[], 0];
  productsAndCount: [Product[], number] = [[], 0];
  formFactorsAndCount: [FormFactor[], number] = [[], 0];
  officesAndCount: [Office[], number] = [[], 0];
  dateRangePickerStartDate = new Date(2017, 0, 1);

  constructor(
    private assetState: AssetState,
    private brandHttp: BrandHttp,
    private productHttp: ProductHttp,
    private formFactorHttp: FormFactorHttp,
    private officeHttp: OfficeHttp,
    private assetHttp: AssetHttp,
  ) { }

  ngOnInit(): void {
    this.assetForm = new FormGroup({
      brandNames: new FormControl(),
      productNames: new FormControl(),
      formFactorNames: new FormControl(),
      officeCities: new FormControl(),
      purchaseDateMin: new FormControl(),
      purchaseDateMax: new FormControl(),
    });

    this.onAssetFormChange();
    this.onBrandChange();
    this.getBrands();
    this.getProducts();
    this.getFormFactors();
    this.getOffices();
    this.search();
  }

  async onAssetFormChange() {
    this.assetForm.valueChanges
      .pipe(untilDestroyed(this), debounceTime(125))
      .subscribe(() => {
        //this.assetState.paginator.firstPage();
        this.search();
      });
  }

  async onBrandChange() {
    this.assetForm.get('brandNames').valueChanges
      .pipe(untilDestroyed(this))
      .subscribe(
        async (brandNames: string[]) => {
          this.productsAndCount = await this.productHttp.search({
            brandFilter: {
              names: brandNames,
            },
            take: Number.MAX_SAFE_INTEGER,
          });
        }
      );
  }

  async getBrands() {
    this.brandsAndCount = await this.brandHttp.search({ take: Number.MAX_SAFE_INTEGER });
  }

  async getProducts() {
    this.productsAndCount = await this.productHttp.search({ take: Number.MAX_SAFE_INTEGER });
  }

  async getFormFactors() {
    this.formFactorsAndCount = await this.formFactorHttp.search({ take: Number.MAX_SAFE_INTEGER });
  }

  async getOffices() {
    this.officesAndCount = await this.officeHttp.search({ take: Number.MAX_SAFE_INTEGER });
  }

  async search() {
    const brandNames = this.assetForm.get('brandNames').value as string[];
    const productNames = this.assetForm.get('productNames').value as string[];
    const formFactorNames = this.assetForm.get('formFactorNames').value as string[];
    const officeCities = this.assetForm.get('officeCities').value as string[];

    const assetFilter = new AssetFilter();

    if (brandNames && brandNames.length) {
      assetFilter.productFilter.brandFilter.names = brandNames;
    }

    if (productNames && productNames.length) {
      assetFilter.productFilter.names = productNames;
    }

    if (formFactorNames && formFactorNames.length) {
      assetFilter.productFilter.formFactorFilter.names = formFactorNames;
    }

    if (officeCities && officeCities.length) {
      assetFilter.officeFilter.cities = officeCities;
    }

    await this.assetHttp.search(assetFilter);
  }
}
