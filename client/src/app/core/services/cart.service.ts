import { inject, Injectable, signal } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Cart, CartItems } from '../../shared/Models/cart';
import { Products } from '../../shared/Models/Products';
import { map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  baseUrl = environment.apiUrl

  private http = inject(HttpClient)
  cart = signal<Cart | null>(null)

  getCart(id: string){
    return this.http.get<Cart>(this.baseUrl + 'cart?id=' + id).pipe(
      map((cart) => {
        this.cart.set(cart)
        return cart
      })
    )
  }

  setCart(cart: Cart){
    return this.http.post<Cart>(this.baseUrl + 'cart', cart).subscribe({
      next: cart => this.cart.set(cart)
    })
  }
  addItemToCart(item: CartItems | Products, quantity = 1){
    const cart = this.cart() ?? this.createCart();
    if(this.isProduct(item)){
      item = this.mapProductToCartItem(item);
    }
    cart.items = this.addOrUpdaeItem(cart.items, item, quantity)
    this.setCart(cart)

  }
  private addOrUpdaeItem(items: CartItems[], item: CartItems, quantity: number): CartItems[]{
    const index = items.findIndex(x => x.id === item.id);
    if(index === -1){
      item.quantity = quantity
      items.push(item);
      return items;
    }
    items[index].quantity += quantity;

    return items;
  }
  private mapProductToCartItem(item : Products): CartItems{
    return {
      id: item.id,
      name: item.name,
      price: item.price,
      pictureUrl: item.pictureUrl,
      quantity: 0,
      type: item.productType,
      brand: item.productBrand
    }
  }
  isProduct(item: CartItems | Products): item is Products{
    return (item as Products).description !== undefined
  }
  createCart() : Cart{
    const cart = new Cart();
    localStorage.setItem('cart_id', cart.id)
    return cart
  }
  constructor() { }
}
