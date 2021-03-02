import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatSort, Sort } from '@angular/material/sort';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Asset } from 'src/app/shared/resources/asset/asset';
import { AssetHttp } from 'src/app/shared/resources/asset/asset.http';
import { AssetState } from 'src/app/shared/resources/asset/asset.state';
import { Currency } from 'src/app/shared/resources/currency/currency';
import { CurrencyState } from 'src/app/shared/resources/currency/currency.state';
import { ResourceChange } from 'src/app/shared/resources/resource.change';
import { EditAssetComponent } from '../dialogs/edit-asset/edit-asset.component';

@UntilDestroy()
@Component({
  selector: 'app-asset-table',
  templateUrl: './asset-table.component.html',
  styleUrls: ['./asset-table.component.scss']
})
export class AssetTableComponent implements OnInit, AfterViewInit {
  currency: Currency;
  displayedColumns: string[];
  loading: boolean;
  count: number = 0;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(
    private currencyState: CurrencyState,
    private assetState: AssetState,
    private assetHttp: AssetHttp,
    private resourceChange: ResourceChange,
    private editAssetDialog: MatDialog,
  ) { }

  ngOnInit(): void {
    this.currencyState.selectedCurrency$
      .pipe(untilDestroyed(this))
      .subscribe(
        (currency: Currency) => {
          this.currency = currency;
        }
      );

    this.displayedColumns = [
      'brand',
      'product',
      'formFactor',
      'office',
      'purchaseDate',
      'price',
    ];

    this.assetState.loading$
      .pipe(untilDestroyed(this))
      .subscribe((loading: boolean) => this.loading = loading);

    this.onAssetChange();
  }

  onAssetChange() {
    this.resourceChange.assets$
      .pipe(untilDestroyed(this))
      .subscribe(
        async () => {
          this.assetState.paginator.firstPage();
          const filter = this.assetState.filter$.getValue();
          await this.assetHttp.search(filter);
        }
      );
  }

  connect(): Observable<Asset[]> {
    return this.assetState.store$
      .pipe(
        map((assetsAndCount: [Asset[], number]) => {
          const [assets, count] = assetsAndCount;
          this.count = count;
          if (assets.length < 10) {
            const rowsToAdd = 10 - assets.length;
            const emptyRows = new Array<Asset>(rowsToAdd);
            return assets.concat(emptyRows);
          }
          return assets;
        })
      );
  }

  disconnect(): void { }

  ngAfterViewInit() {
    this.assetState.paginator = this.paginator;
    this.assetState.paginator.page
      .pipe(untilDestroyed(this))
      .subscribe(
        async (pageEvent: PageEvent) => {
          const filter = this.assetState.filter$.getValue();
          filter.skip = pageEvent.pageIndex * pageEvent.pageSize;
          filter.take = pageEvent.pageSize;
          await this.assetHttp.search(filter);
        }
      );
  }

  async sortData(sort: Sort) {
    const filter = this.assetState.filter$.getValue();
    delete filter.orderByAsc;
    delete filter.orderByDesc;

    if (sort.direction === 'asc') {
      filter.orderByAsc = sort.active;
    } else {
      filter.orderByDesc = sort.active;
    }

    await this.assetHttp.search(filter);
  }

  getRowClass(row: Asset) {
    if (row) {
      return 'row';
    } else if (!row && !this.loading) {
      return 'row-empty';
    }
  }

  async openEditAssetDialog(asset: Asset) {
    if (asset) {
      const dialogConfig = new MatDialogConfig();
      dialogConfig.autoFocus = true;
      dialogConfig.disableClose = true;
      dialogConfig.width = '48rem';
      dialogConfig.maxWidth = '90vw';
      dialogConfig.maxHeight = '90vh';
      dialogConfig.data = asset;

      this.editAssetDialog.open(EditAssetComponent, dialogConfig);
    }
  }
}
