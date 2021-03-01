import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
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
  ) { }

  ngOnInit(): void {
    this.addAssetForm = new FormGroup({
      brandName: new FormControl([Validators.required]),
      productName: new FormControl({ value: null, disabled: true }, [Validators.required]),
      officeName: new FormControl([Validators.required]),
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

  submit() {
    this.submitting = true;
  }

  close() {
    this.dialogRef.close();
  }
}
