import { Component, inject } from '@angular/core';
import { CartService } from '../../core/services/cart.service';
import { CartItemsComponent } from "../cart-items/cart-items.component";

@Component({
    selector: 'app-cart',
    imports: [CartItemsComponent],
    templateUrl: './cart.component.html',
    styleUrl: './cart.component.scss'
})
export class CartComponent {
    cartService = inject(CartService);
}
