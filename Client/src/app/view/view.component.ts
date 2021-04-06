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
    // Get all currencies
    const filtered = await this.currencyHttp.search({ take: Number.MAX_SAFE_INTEGER });

    // Assign currencies to instance
    this.currencies = filtered[0];

    // Get saved currency from local storage
    const savedCurrencyStringified = localStorage.getItem('currency');

    if (savedCurrencyStringified) {
      // If saved currency exist, update state and set it as the selected currency
      this.currencyState.selectedCurrency$.next(JSON.parse(savedCurrencyStringified));
    } else {
      // If saved currency does not exist, select usd as default currency
      const usd = this.currencies.find(currency => currency.code === 'USD');
      this.setCurrency(usd);
    }
  }

  getCurrency() {
    // Returns the currently selected currency
    return JSON.parse(localStorage.getItem('currency')) as Currency;
  }

  setCurrency(currency: Currency) {
    // Updates state for currently selected currency
    localStorage.setItem('currency', JSON.stringify(currency));
    this.currencyState.selectedCurrency$.next(currency);
  }
}
