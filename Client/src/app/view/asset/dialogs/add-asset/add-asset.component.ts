import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { Asset } from 'src/app/shared/resources/asset/asset';
import { AssetHttp } from 'src/app/shared/resources/asset/asset.http';
import { Brand } from 'src/app/shared/resources/brand/brand';
import { BrandHttp } from 'src/app/shared/resources/brand/brand.http';
import { Office } from 'src/app/shared/resources/office/office';
import { OfficeHttp } from 'src/app/shared/resources/office/office.http';
import { Product } from 'src/app/shared/resources/product/product';
import { ProductHttp } from 'src/app/shared/resources/product/product.http';

@UntilDestroy()
@Component({
  selector: 'app-add-asset',
  templateUrl: './add-asset.component.html',
  styleUrls: ['./add-asset.component.scss']
})
export class AddAssetComponent implements OnInit {
  addAssetForm: FormGroup;
  brandsAndCount: [Brand[], number] = [[], 0];
  productsAndCount: [Product[], number] = [[], 0];
  officesAndCount: [Office[], number] = [[], 0];
  datePickerMinDate = new Date(2000, 1, 1);
  datePickerMaxDate = new Date();
  submitting: boolean;

  constructor(
    private dialogRef: MatDialogRef<AddAssetComponent>,
    private brandHttp: BrandHttp,
    private productHttp: ProductHttp,
    private officeHttp: OfficeHttp,
    private assetHttp: AssetHttp,
  ) { }

  ngOnInit(): void {
    this.addAssetForm = new FormGroup({
      brandName: new FormControl([Validators.required]),
      productName: new FormControl({ value: null, disabled: true }, [Validators.required]),
      officeCity: new FormControl([Validators.required]),
      purchaseDate: new FormControl([Validators.required]),
    });

    this.onBrandChange();
    this.getBrands();
    this.getOffices();
  }

  async onBrandChange() {
    this.addAssetForm.get('brandName').valueChanges
      .pipe(untilDestroyed(this))
      .subscribe(
        async (brandName: string) => {
          this.productsAndCount = await this.productHttp.search({
            brandFilter: {
              names: [brandName]
            },
            take: Number.MAX_SAFE_INTEGER,
          });
          this.addAssetForm.get('productName').enable();
        }
      );
  }

  async getBrands() {
    this.brandsAndCount = await this.brandHttp.search({ take: Number.MAX_SAFE_INTEGER });
  }

  async getOffices() {
    this.officesAndCount = await this.officeHttp.search({ take: Number.MAX_SAFE_INTEGER });
  }

  async submit() {
    this.submitting = true;

    const productName = this.addAssetForm.get('productName').value as string;
    const officeCity = this.addAssetForm.get('officeCity').value as string;
    const purchaseDate = this.addAssetForm.get('purchaseDate').value as Date;

    const dto = new Asset();
    dto.productId = this.productsAndCount[0].find(product => product.name === productName).id;
    dto.officeId = this.officesAndCount[0].find(office => office.city === officeCity).id;
    dto.purchaseDate = purchaseDate;

    try {
      await this.assetHttp.create(dto);
      this.dialogRef.close();
    } catch (error) {
      console.log(error);
    } finally {
      this.submitting = false;
    }
  }

  close() {
    this.dialogRef.close();
  }
}
