import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { AuthService } from 'src/app/services/auth.service';
import { ReservationService } from 'src/app/services/reservation.service';
import { ReservationHistoryDialogComponent } from '../reservation-history-dialog/reservation-history-dialog.component';

@Component({
  selector: 'app-view-allreservations',
  templateUrl: './view-allreservations.component.html',
  styleUrls: ['./view-allreservations.component.css']
})
export class ViewAllreservationsComponent implements OnInit {

  displayedColumns: string[] = ['reservationId','userId', 'packageName', 'status', 'actions'];
  reservations: any[] = [];
  userRole: string | null = null;

  statusMap: { [key: number]: string } = {
    0: 'Processing',
    1: 'Accepting',
    2: 'Rejected',
  };
  constructor(private authService: AuthService,private reservationService: ReservationService,private dialog: MatDialog) { }

  ngOnInit(): void {
    this.userRole = this.authService.getUserRole();
    if (this.userRole === 'Admin' || this.userRole === 'Agent') {
      this.loadAllReservations();
    }
  }

  loadAllReservations() {
    this.reservationService.getAllReservations()
      .then(response => {
        this.reservations = response;
      })
      .catch(error => {
        console.error('Error loading reservations:', error);
      });
  }

  acceptReservation(reservationId: number) {
    this.reservationService.acceptReservation(reservationId)
      .then(() => {
        alert('Reservation accepted successfully!');
        this.loadAllReservations();
      })
      .catch(error => {
        console.error('Error accepting reservation:', error);
      });
  }

  rejectReservation(reservationId: number) {
    this.reservationService.rejectReservation(reservationId)
      .then(() => {
        alert('Reservation rejected successfully!');
        this.loadAllReservations();
      })
      .catch(error => {
        console.error('Error rejecting reservation:', error);
      });
  }

  openHistoryDialog(reservationId: number) {
    const dialogRef = this.dialog.open(ReservationHistoryDialogComponent, {
      data: { reservationId: reservationId }
    });

    dialogRef.afterClosed().subscribe(result => {
      // Logika nakon zatvaranja dijaloga ako je potrebna
    });
  }

}
