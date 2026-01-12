import { computed, inject, Injectable, signal } from '@angular/core';
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

  cartItemsCount = computed(() => {
    return this.cart()?.items.reduce((sum, item) => sum + item.quantity, 0)
  })

  cartItemsTotal = computed(() => {
    const cart = this.cart();
    if(!cart) return null;
    const subtotal = cart.items.reduce((total, item) => total + item.quantity * item.price, 0)
    const discount = 0;
    const deliveryFees = 0;
    return {
      subtotal, 
      discount, 
      deliveryFees,
      total: subtotal + deliveryFees - discount 
    }
  })

  getCart(id: string){
    return this.http.get<Cart>(this.baseUrl + 'Basket?id=' + id).pipe(
      map((cart) => {
        this.cart.set(cart)
        return cart
      })
    )
  }

  setCart(cart: Cart){
    return this.http.post<Cart>(this.baseUrl + 'Basket', cart).subscribe({
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

  removeItemFromCart(productId: number, quantity = 1){
    const cart = this.cart();
    if(!cart) return;

    const index = cart.items.findIndex(x => x.id === productId);
    if(index === -1) return;

    cart.items[index].quantity -= quantity;

    if(cart.items[index].quantity <= 0){
      cart.items.splice(index, 1);
    }

    if(cart.items.length === 0)
    {
      this.deleteCart()
    }
    this.setCart(cart);
  }

  deleteCart(){
    return this.http.delete<Cart>(this.baseUrl + 'Basket?id=' + this.cart()?.id).subscribe({
      next: () => {
        localStorage.removeItem('cart_id');
        this.cart.set(null);
      }
    })
  }

  getCoumputedCartCount(){
    return this.cartItemsCount
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
  private removeItem(items: CartItems[], productId: number, quantity: number): CartItems[]{
    const index = items.findIndex(x => x.id === productId);
    if(index === -1) return items;

    items[index].quantity -= quantity;

    if(items[index].quantity <= 0){
      items.splice(productId, 1);
      return items;
    }

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
