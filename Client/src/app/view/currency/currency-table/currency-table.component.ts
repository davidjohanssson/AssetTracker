import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Currency } from 'src/app/shared/resources/currency/currency';
import { CurrencyHttp } from 'src/app/shared/resources/currency/currency.http';
import { CurrencyState } from 'src/app/shared/resources/currency/currency.state';

@UntilDestroy()
@Component({
  selector: 'app-currency-table',
  templateUrl: './currency-table.component.html',
  styleUrls: ['./currency-table.component.scss']
})
export class CurrencyTableComponent implements OnInit, AfterViewInit {
  @ViewChild(MatPaginator) paginator: MatPaginator;
  count: number = 0;
  loading: boolean;
  displayedColumns: string[];

  constructor(
    private currencyState: CurrencyState,
    private currencyHttp: CurrencyHttp,
  ) { }

  ngOnInit(): void {
    this.displayedColumns = [
      'name',
      'code',
      'exchangeRateRelativeToDollar',
    ];

    this.currencyState.loading$
      .pipe(untilDestroyed(this))
      .subscribe((loading: boolean) => this.loading = loading);
  }

  connect(): Observable<Currency[]> {
    return this.currencyState.store$
      .pipe(
        map((latestSearch: [Currency[], number]) => {
          const [currencies, count] = latestSearch;
          this.count = count;
          if (currencies.length < 10) {
            const rowsToAdd = 10 - currencies.length;
            const emptyRows = new Array<Currency>(rowsToAdd);
            return currencies.concat(emptyRows);
          }
          return currencies;
        })
      );
  }

  disconnect(): void {}

  getRowClass(row: Currency) {
    if (row) {
      return 'row';
    } else if (!row && !this.loading) {
      return 'row-empty';
    }
  }

  async openCurrencyDialog() {

  }

  ngAfterViewInit() {
    this.currencyState.paginator = this.paginator;
    this.currencyState.paginator.page
      .pipe(untilDestroyed(this))
      .subscribe(
        async (pageEvent: PageEvent) => {
          const filter = this.currencyState.filter$.getValue();
          filter.skip = pageEvent.pageIndex * pageEvent.pageSize;
          filter.take = pageEvent.pageSize;
          await this.currencyHttp.search(filter);
        }
      );
  }
}
