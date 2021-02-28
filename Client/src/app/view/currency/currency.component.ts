import { Component, OnInit } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { AddCurrencyComponent } from './dialogs/add-currency/add-currency.component';

@Component({
  selector: 'app-currency',
  templateUrl: './currency.component.html',
  styleUrls: ['./currency.component.scss']
})
export class CurrencyComponent implements OnInit {

  constructor(
    private addCurrencyDialog: MatDialog,
  ) { }

  ngOnInit(): void {
  }

  openAddCurrencyDialog() {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.autoFocus = true;
    dialogConfig.disableClose = true;
    dialogConfig.width = '24rem';
    dialogConfig.maxWidth = '90vw';
    dialogConfig.maxHeight = '90vh';

    this.addCurrencyDialog.open(AddCurrencyComponent, dialogConfig);
  }
}
