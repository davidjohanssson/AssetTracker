import { NgModule } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';

@NgModule({
    imports: [
        MatButtonModule,
        MatInputModule,
        MatSelectModule,
        MatIconModule,
        MatMenuModule,
    ],
    exports: [
        MatButtonModule,
        MatInputModule,
        MatSelectModule,
        MatIconModule,
        MatMenuModule,
    ]
})
export class MaterialModule { }