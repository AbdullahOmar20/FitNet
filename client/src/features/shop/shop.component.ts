import { Component, inject, OnInit } from '@angular/core';
import { ShopService } from '../../core/services/shop.service';
import { Products } from '../../shared/Models/Products';
import { ProductItemComponent } from "../product-item/product-item.component";
import { MatDialog } from "@angular/material/dialog"
import { FilterDialogComponent } from '../filter-dialog/filter-dialog.component';
import { MatButton } from '@angular/material/button';
import { MatIcon } from '@angular/material/icon';
import { MatMenu, MatMenuTrigger } from '@angular/material/menu';
import { MatListOption, MatSelectionList, MatSelectionListChange } from '@angular/material/list';

@Component({
  selector: 'app-shop',
  standalone: true,
  imports: [
    ProductItemComponent,
    MatButton,
    MatIcon,
    MatMenu,
    MatSelectionList,
    MatListOption,
    MatMenuTrigger
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
  selectedSort: string = 'Name'

  sortOptions = [
    {name: 'Alphabetical', value: 'Name'},
    {name: 'Price: Low-High', value: 'priceAsc'},
    {name: 'Price: High-Low', value: 'priceDesc'}
  ]
  
  ngOnInit(): void {
    this.initializeShop();
  }

  initializeShop(){
    this.shopService.getBrands()
    this.shopService.getTypes()
    this.getProducts()
  }

  getProducts(){
    this.shopService.getProducts(this.selectedBrands, this.selectedTypes, this.selectedSort).subscribe({
      next: response => this.products = response.data,
      error: err => console.log("error" + err)
    })
  }

  onSortChange(event: MatSelectionListChange){
    const selectOption = event.options[0]
    if(selectOption){
      this.selectedSort = selectOption.value
      this.getProducts()
    }
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
          this.selectedBrands = result.selectedBrands;
          this.selectedTypes = result.selectedTypes;
          this.getProducts()
        }
      }
    })
  }
}
