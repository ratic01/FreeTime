import { Component,Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { ReservationService } from 'src/app/services/reservation.service';


@Component({
  selector: 'app-reservation-history-dialog',
  templateUrl: './reservation-history-dialog.component.html',
  styleUrls: ['./reservation-history-dialog.component.css']
})
export class ReservationHistoryDialogComponent implements OnInit {

  history: any[] = [];

  constructor(
    private dialogRef: MatDialogRef<ReservationHistoryDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { reservationId: number },
    private reservationService: ReservationService
  ) {}

  ngOnInit() {
    this.loadHistory();
  }

  async loadHistory() {
    try {
      this.history = await this.reservationService.getReservationEvents(this.data.reservationId);
    } catch (error) {
      console.error('Error loading reservation events:', error);
    }
  }

  closeDialog() {
    this.dialogRef.close();
  }

}
