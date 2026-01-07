import { Component, inject, OnInit } from '@angular/core';
import { ShopService } from '../../core/services/shop.service';
import { Products } from '../../shared/Models/Products';
import { Pagination } from '../../shared/Models/Pagination';
import { ProductItemComponent } from "../product-item/product-item.component";
import { MatDialog } from "@angular/material/dialog"
import { FilterDialogComponent } from '../filter-dialog/filter-dialog.component';
import { MatButton, MatIconButton } from '@angular/material/button';
import { MatIcon } from '@angular/material/icon';
import { MatMenu, MatMenuTrigger } from '@angular/material/menu';
import { MatListOption, MatSelectionList, MatSelectionListChange } from '@angular/material/list';
import { ShopParams } from '../../shared/Models/ShopParams';
import { MatPaginatorModule, PageEvent } from '@angular/material/paginator';
import { FormsModule } from '@angular/forms';
import { AppSearchInputComponent } from '../app-search-input/app-search-input.component';
import { Subscription } from 'rxjs';

@Component({
    selector: 'app-shop',
    imports: [
        ProductItemComponent,
        MatButton,
        MatIcon,
        MatMenu,
        MatSelectionList,
        MatListOption,
        MatMenuTrigger,
        MatPaginatorModule,
        FormsModule,
        MatIconButton,
        AppSearchInputComponent
    ],
    templateUrl: './shop.component.html',
    styleUrl: './shop.component.scss'
})
export class ShopComponent implements OnInit{
  shopService = inject(ShopService)
  private dialogeService = inject(MatDialog)
  products?: Pagination<Products>;

  subs: Subscription[] = []

  shopParams = new ShopParams()

  pageSizeOptions = [5,10,15,20]

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
    this.shopService.getProducts(this.shopParams).subscribe({
      next: response => this.products = response,
      error: err => console.log("error" + err)
    })
  }
  getProductssubs(): Subscription{
    return this.shopService.getProducts(this.shopParams).subscribe({
      next: response => this.products = response,
      error: err => console.log("error" + err)
    })
  }

  onTextChange(changedText: string){
    this.cancelSubs()
    this.shopParams.pageNumber = 1
    this.shopParams.search = changedText
    const sub = this.getProductssubs()
    this.subs.push(sub)
  }

  cancelSubs(){
    this.subs.forEach(s => s.unsubscribe())
  }

  onResetSubmit(){
    if(this.shopParams.search != '')
    {
      this.shopParams.pageNumber = 1
      this.shopParams.search = ''
      this.cancelSubs()
      this.getProducts()
    }
  }
  
  handlePageEvent(event: PageEvent){
    this.shopParams.pageNumber = event.pageIndex + 1
    this.shopParams.pageSize = event.pageSize
    this.getProducts()
  }

  onSortChange(event: MatSelectionListChange){
    const selectOption = event.options[0]
    if(selectOption){
      this.shopParams.sort = selectOption.value
      this.shopParams.pageNumber = 1
      this.getProducts()
    }
  }

  openFiltersDialog(){
    const dialogRef = this.dialogeService.open(FilterDialogComponent, {
      minWidth: '500px',
      data: {
        selectedBrands: this.shopParams.brands,
        selectedTypes: this.shopParams.types,
      }
    })
    dialogRef.afterClosed().subscribe({
      next: result => {
        if(result){
          this.shopParams.brands = result.selectedBrands;
          this.shopParams.types = result.selectedTypes;
          this.shopParams.pageNumber = 1
          this.getProducts()
        }
      }
    })
  }
}
