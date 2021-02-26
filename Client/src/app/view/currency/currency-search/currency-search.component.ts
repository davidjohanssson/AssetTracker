import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { Currency } from 'src/app/shared/resources/currency/currency';
import { CurrencyHttp } from 'src/app/shared/resources/currency/currency.http';
import { debounceTime } from 'rxjs/operators';
import { CurrencyFilter } from 'src/app/shared/resources/currency/currency.filter';
import { CurrencyState } from 'src/app/shared/resources/currency/currency.state';

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
    private currencyState: CurrencyState,
  ) { }

  ngOnInit() {
    this.formGroup = this.formBuilder.group({
      name: new FormControl(),
      codes: new FormControl(),
    });

    this.onFormGroupChange();
    this.getCurrencies();
    this.search();
  }

  async onFormGroupChange() {
    this.formGroup.valueChanges
      .pipe(untilDestroyed(this), debounceTime(125))
      .subscribe(() => this.search());
  }

  async getCurrencies() {
    const filtered = await this.currencyHttp.search();
    this.currencies = filtered[0];
  }

  async search() {
    const name = this.formGroup.get('name').value as string;
    const codes = this.formGroup.get('codes').value as string[];
    const currencyFilter = new CurrencyFilter();

    if (name) {
      currencyFilter.name = name;
    }

    if (codes && codes.length) {
      currencyFilter.codes = codes;
    }
    
    await this.currencyHttp.search(currencyFilter);
  }
}
