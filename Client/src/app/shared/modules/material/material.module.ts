import { NgModule } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';
import { MatDividerModule } from '@angular/material/divider';
import { MatTableModule } from '@angular/material/table';
import { MatRippleModule } from '@angular/material/core';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatDialogModule } from '@angular/material/dialog';

@NgModule({
    imports: [
        MatButtonModule,
        MatInputModule,
        MatSelectModule,
        MatIconModule,
        MatMenuModule,
        MatDividerModule,
        MatTableModule,
        MatRippleModule,
        MatPaginatorModule,
        MatSortModule,
        MatDialogModule,
    ],
    exports: [
        MatButtonModule,
        MatInputModule,
        MatSelectModule,
        MatIconModule,
        MatMenuModule,
        MatDividerModule,
        MatTableModule,
        MatRippleModule,
        MatPaginatorModule,
        MatSortModule,
        MatDialogModule,
    ]
})
export class MaterialModule { }