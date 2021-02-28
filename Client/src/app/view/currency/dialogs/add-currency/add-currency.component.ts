import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-add-currency',
  templateUrl: './add-currency.component.html',
  styleUrls: ['./add-currency.component.scss']
})
export class AddCurrencyComponent implements OnInit {

  constructor(
    private dialogRef: MatDialogRef<AddCurrencyComponent>,
  ) { }

  ngOnInit(): void {
  }

}
