import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AssetComponent } from './view/asset/asset.component';
import { CurrencyComponent } from './view/currency/currency.component';
import { ProductComponent } from './view/product/product.component';
import { ViewComponent } from './view/view.component';

const routes: Routes = [
  {
    path: '',
    component: ViewComponent,
    children: [
      {
        path: '',
        component: AssetComponent,
      },
      {
        path: 'products',
        component: ProductComponent,
      },
      {
        path: 'currencies',
        component: CurrencyComponent,
      }
    ]
  },
  {
    path: '**',
    redirectTo: '',
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
