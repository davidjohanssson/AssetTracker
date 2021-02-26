import { Component, OnInit } from '@angular/core';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Currency } from 'src/app/shared/resources/currency/currency';
import { CurrencyStore } from 'src/app/shared/resources/currency/currency.store';

@UntilDestroy()
@Component({
  selector: 'app-currency-table',
  templateUrl: './currency-table.component.html',
  styleUrls: ['./currency-table.component.scss']
})
export class CurrencyTableComponent implements OnInit {
  loading: boolean;
  displayedColumns: string[];

  constructor(
    private currencyStore: CurrencyStore,
  ) { }

  ngOnInit(): void {
    this.displayedColumns = ['name', 'code', 'exchangeRateRelativeToDollar'];

    this.currencyStore.loading$
      .pipe(untilDestroyed(this))
      .subscribe((loading: boolean) => this.loading = loading);
  }

  connect(): Observable<Currency[]> {
    return this.currencyStore.latestSearch$
      .pipe(
        map((latestSearch: [Currency[], number]) => {
          const [currencies, count] = latestSearch;
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
}
