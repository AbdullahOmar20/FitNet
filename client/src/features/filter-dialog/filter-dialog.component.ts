import { Component, inject } from '@angular/core';
import { ShopService } from '../../core/services/shop.service';
import { MatDivider } from "@angular/material/divider"
import { MatSelectionList, MatListOption } from "@angular/material/list"
import { MatButton } from '@angular/material/button';
import { MAT_DIALOG_DATA, MatDialog, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-filter-dialog',
  standalone: true,
  imports: [
    MatDivider,
    MatSelectionList,
    MatListOption,
    MatButton
  ],
  templateUrl: './filter-dialog.component.html',
  styleUrl: './filter-dialog.component.scss'
})
export class FilterDialogComponent {
  shopService = inject(ShopService);
  private dialogRef = inject(MatDialogRef<FilterDialogComponent>)
  data = inject(MAT_DIALOG_DATA);

  selectedBrands: string[] = this.data.selectedBrands
  selectedTypes: string[] = this.data.selectedTypes

  applyFilter(){
    this.dialogRef.close({
      selectedBrands: this.selectedBrands,
      selectedTypes: this.selectedBrands
    })
  }
}
