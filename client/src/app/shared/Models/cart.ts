import {nanoid} from 'nanoid'

export type CartType = {
    id: string
    items: CartItems[]
}

export type CartItems = {
    id: number;
    name: string;
    price: number;
    pictureUrl: string;
    quantity: number;
    brand: string;
    type: string;
}

export class Cart implements CartType {
    id = nanoid();
    items: CartItems[] = [];
}