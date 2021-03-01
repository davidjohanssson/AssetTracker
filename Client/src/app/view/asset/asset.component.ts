import { Component, OnInit } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { AddAssetComponent } from './dialogs/add-asset/add-asset.component';

@Component({
  selector: 'app-asset',
  templateUrl: './asset.component.html',
  styleUrls: ['./asset.component.scss']
})
export class AssetComponent implements OnInit {

  constructor(
    private addAssetDialog: MatDialog,
  ) { }

  ngOnInit(): void {
  }

  openAddAssetDialog() {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.autoFocus = true;
    dialogConfig.disableClose = true;
    dialogConfig.width = '48rem';
    dialogConfig.maxWidth = '90vw';
    dialogConfig.maxHeight = '90vh';

    this.addAssetDialog.open(AddAssetComponent, dialogConfig);
  }
}
