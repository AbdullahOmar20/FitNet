import { Component, inject, OnInit } from '@angular/core';
import { ShopService } from '../../core/services/shop.service';
import { Products } from '../../shared/Models/Products';
import { ProductItemComponent } from "../product-item/product-item.component";
import { MatDialog } from "@angular/material/dialog"
import { FilterDialogComponent } from '../filter-dialog/filter-dialog.component';
import { MatButton } from '@angular/material/button';
import { MatIcon } from '@angular/material/icon';

@Component({
  selector: 'app-shop',
  standalone: true,
  imports: [
    ProductItemComponent,
    MatButton,
    MatIcon
],
  templateUrl: './shop.component.html',
  styleUrl: './shop.component.scss'
})
export class ShopComponent implements OnInit{
  shopService = inject(ShopService)
  private dialogeService = inject(MatDialog)
  products: Products[] = [];
  selectedBrands: number[] = [];
  selectedTypes: number[] = [];
  
  ngOnInit(): void {
    this.initializeShop();
  }

  initializeShop(){
    this.shopService.getBrands()
    this.shopService.getTypes()
    this.shopService.getProducts().subscribe({
      next: response => this.products = response.data,
      error: err => console.log("error" + err)
    })
  }

  openFiltersDialog(){
    const dialogRef = this.dialogeService.open(FilterDialogComponent, {
      minWidth: '500px',
      data: {
        selectedBrands: this.selectedBrands,
        selectedTypes: this.selectedTypes,
      }
    })
    dialogRef.afterClosed().subscribe({
      next: result => {
        if(result){
          console.log(result);
          this.selectedBrands = result.selectedBrands;
          this.selectedTypes = result.selectedTypes;
        }
      }
    })
  }
}
