import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { Currency } from 'src/app/shared/resources/currency/currency';
import { CurrencyHttp } from 'src/app/shared/resources/currency/currency.http';
import { debounceTime } from 'rxjs/operators';
import { CurrencyFilter } from 'src/app/shared/resources/currency/currency.filter';
import { CurrencyStore } from 'src/app/shared/resources/currency/currency.store';

@UntilDestroy()
@Component({
  selector: 'app-currency-search',
  templateUrl: './currency-search.component.html',
  styleUrls: ['./currency-search.component.scss']
})
export class CurrencySearchComponent implements OnInit {
  formGroup: FormGroup;
  currencies: Currency[];

  constructor(
    private formBuilder: FormBuilder,
    private currencyHttp: CurrencyHttp,
    private currencyStore: CurrencyStore,
  ) { }

  ngOnInit() {
    this.formGroup = this.formBuilder.group({
      currencies: new FormControl(),
    });

    this.onFormGroupChange();
    this.getCurrencies();
  }

  async onFormGroupChange() {
    this.formGroup.valueChanges
      .pipe(untilDestroyed(this), debounceTime(125))
      .subscribe(() => this.search());
  }

  async getCurrencies() {
    const filtered = await this.currencyHttp.filter();
    this.currencies = filtered[0];
  }

  async search() {
    const currencyNames = this.formGroup.get('currencies').value as string[];
    const currencyFilter = new CurrencyFilter();

    if (currencyNames && currencyNames.length) {
      currencyFilter.names = currencyNames;
    }
    
    this.currencyStore.latestSearch$.next(null);
    const filtered = await this.currencyHttp.filter(currencyFilter);
    this.currencyStore.latestSearch$.next(filtered);
  }
}
