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
    // Listen for changes of currently selected currency
    this.currencyState.selectedCurrency$
      .pipe(untilDestroyed(this))
      .subscribe(
        (currency: Currency) => {
          this.currency = currency;
        }
      );

    // Declare table columns
    this.displayedColumns = [
      'brand',
      'product',
      'formFactor',
      'office',
      'purchaseDate',
      'price',
    ];

    // Listen if assets are currently being loaded (request in-flight)
    this.assetState.loading$
      .pipe(untilDestroyed(this))
      .subscribe((loading: boolean) => this.loading = loading);

    
    this.onAssetChange();
  }

  // Listen for changes on any asset resource
  // This emits if a POST, PUT or DELETE operation has been made on an Asset entity
  onAssetChange() {
    this.resourceChange.assets$
      .pipe(untilDestroyed(this))
      .subscribe(
        async () => {
          // Jump back to first page if any change has been made to an Asset
          this.assetState.paginator.firstPage();
          // Get the current search filter, we want to preserve user inputed filter
          const filter = this.assetState.filter$.getValue();
          // Initiate a search with the preserved filter
          await this.assetHttp.search(filter);
        }
      );
  }

  // Table source
  connect(): Observable<Asset[]> {
    return this.assetState.store$
      .pipe(
        map((assetsAndCount: [Asset[], number]) => {
          // We map into the emission because we want to manipulate the data
          const [assets, count] = assetsAndCount;
          // Set the count in instance
          this.count = count;
          // If we receive less than 10 assets we want to fill the table with empty rows
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

  // Hook that runs after rendering
  ngAfterViewInit() {
    // Keep a reference to the paginator in AssetState
    // This is because we need to call methods on it from other components
    this.assetState.paginator = this.paginator;
    // Listen to page events
    this.assetState.paginator.page
      .pipe(untilDestroyed(this))
      .subscribe(
        async (pageEvent: PageEvent) => {
          // Grab the current filter
          const filter = this.assetState.filter$.getValue();
          // Calculate how many items to skip
          filter.skip = pageEvent.pageIndex * pageEvent.pageSize;
          // Get how many items to take
          filter.take = pageEvent.pageSize;
          // Initiate a search with currently selected filter
          await this.assetHttp.search(filter);
        }
      );
  }

  async sortData(sort: Sort) {
    const filter = this.assetState.filter$.getValue();
    // Delete any current orderBy filters before initiating a new
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
