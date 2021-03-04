import { Component, OnInit } from '@angular/core';
import { Currency } from '../shared/resources/currency/currency';
import { CurrencyHttp } from '../shared/resources/currency/currency.http';
import { CurrencyState } from '../shared/resources/currency/currency.state';

@Component({
  selector: 'app-view',
  templateUrl: './view.component.html',
  styleUrls: ['./view.component.scss']
})
export class ViewComponent implements OnInit {
  currencies: Currency[];

  constructor(
    private currencyHttp: CurrencyHttp,
    private currencyState: CurrencyState,
  ) { }

  async ngOnInit() {
    const filtered = await this.currencyHttp.search({ take: Number.MAX_SAFE_INTEGER });
    this.currencies = filtered[0];

    const savedCurrencyStringified = localStorage.getItem('currency');

    if (savedCurrencyStringified) {
      this.currencyState.selectedCurrency$.next(JSON.parse(savedCurrencyStringified));
    } else {
      const usd = this.currencies.find(currency => currency.code === 'USD');
      this.setCurrency(usd);
    }
  }

  getCurrency() {
    return JSON.parse(localStorage.getItem('currency')) as Currency;
  }

  setCurrency(currency: Currency) {
    localStorage.setItem('currency', JSON.stringify(currency));
    this.currencyState.selectedCurrency$.next(currency);
  }
}
