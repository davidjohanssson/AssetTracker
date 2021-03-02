import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
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
  selector: 'app-edit-asset',
  templateUrl: './edit-asset.component.html',
  styleUrls: ['./edit-asset.component.scss']
})
export class EditAssetComponent implements OnInit {
  editAssetForm: FormGroup;
  brandsAndCount: [Brand[], number] = [[], 0];
  productsAndCount: [Product[], number] = [[], 0];
  officesAndCount: [Office[], number] = [[], 0];
  datePickerMinDate = new Date(2000, 1, 1);
  datePickerMaxDate = new Date();
  submitting: boolean;

  constructor(
    private dialogRef: MatDialogRef<EditAssetComponent>,
    @Inject(MAT_DIALOG_DATA) public asset: Asset,
    private brandHttp: BrandHttp,
    private productHttp: ProductHttp,
    private officeHttp: OfficeHttp,
    private assetHttp: AssetHttp,
  ) { }

  ngOnInit(): void {
    this.editAssetForm = new FormGroup({
      brandName: new FormControl(Validators.required),
      productName: new FormControl(Validators.required),
      officeCity: new FormControl(Validators.required),
      purchaseDate: new FormControl(Validators.required),
    });

    this.editAssetForm.patchValue({
      brandName: this.asset.product.brand.name,
      productName: this.asset.product.name,
      officeCity: this.asset.office.city,
      purchaseDate: this.asset.purchaseDate,
    });

    this.onBrandChange();
    this.getBrands();
    this.getProducts();
    this.getOffices();
  }

  async onBrandChange() {
    this.editAssetForm.get('brandName').valueChanges
      .pipe(untilDestroyed(this))
      .subscribe(
        async (brandName: string) => {
          this.editAssetForm.get('productName').reset();
          this.productsAndCount = await this.productHttp.search({
            brandFilter: {
              names: [brandName]
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
    this.productsAndCount = await this.productHttp.search({
      brandFilter: {
        names: [this.asset.product.brand.name]
      },
      take: Number.MAX_SAFE_INTEGER,
    });
  }

  async getOffices() {
    this.officesAndCount = await this.officeHttp.search({ take: Number.MAX_SAFE_INTEGER });
  }

  async delete() {
    this.submitting = true;

    try {
      await this.assetHttp.delete(this.asset.id);
      this.dialogRef.close();
    } catch (error) {
      console.log(error);
    } finally {
      this.submitting = false;
    }
  }

  async submit() {
    this.submitting = true;

    const productName = this.editAssetForm.get('productName').value as string;
    const officeCity = this.editAssetForm.get('officeCity').value as string;
    const purchaseDate = this.editAssetForm.get('purchaseDate').value as Date;

    const dto = new Asset();
    dto.productId = this.productsAndCount[0].find(product => product.name === productName).id;
    dto.officeId = this.officesAndCount[0].find(office => office.city === officeCity).id;
    dto.purchaseDate = purchaseDate;

    try {
      await this.assetHttp.update(this.asset.id, dto);
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
