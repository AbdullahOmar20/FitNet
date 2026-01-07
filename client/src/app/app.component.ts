import { Component, inject, OnInit, Type } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HeaderComponent } from "./layout/header/header.component";
import { Products } from './shared/Models/Products';
import { ShopService } from './core/services/shop.service';
import { ShopComponent } from "./features/shop/shop.component";

@Component({
    selector: 'app-root',
    imports: [RouterOutlet, HeaderComponent, ShopComponent],
    templateUrl: './app.component.html',
    styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'FitNet';
}
