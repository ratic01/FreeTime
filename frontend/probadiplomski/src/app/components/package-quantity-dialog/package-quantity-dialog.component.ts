import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-package-quantity-dialog',
  templateUrl: './package-quantity-dialog.component.html',
  styleUrls: ['./package-quantity-dialog.component.css']
})
export class PackageQuantityDialogComponent {

  numberOfPackages: number = 1; // Default value

  constructor(public dialogRef: MatDialogRef<PackageQuantityDialogComponent>) {}

  onNoClick(): void {
    this.dialogRef.close(); // Close the dialog without returning a value
  }

  

}
