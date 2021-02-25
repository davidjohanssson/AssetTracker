import { NgModule } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';
import { MatDividerModule } from '@angular/material/divider';

@NgModule({
    imports: [
        MatButtonModule,
        MatInputModule,
        MatSelectModule,
        MatIconModule,
        MatMenuModule,
        MatDividerModule,
    ],
    exports: [
        MatButtonModule,
        MatInputModule,
        MatSelectModule,
        MatIconModule,
        MatMenuModule,
        MatDividerModule,
    ]
})
export class MaterialModule { }