import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-reservation-details-dialog',
  templateUrl: './reservation-details-dialog.component.html',
  styleUrls: ['./reservation-details-dialog.component.css']
})
export class ReservationDetailsDialogComponent  {


  statusMap: { [key: number]: string } = {
    0: 'Processing',
    1: 'Accepting',
    2: 'Rejected'
  };

  constructor(
    public dialogRef: MatDialogRef<ReservationDetailsDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {}

  close(): void {
    this.dialogRef.close();
  }

  
}
