import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { ReservationService } from 'src/app/services/reservation.service';
import { MatDialog } from '@angular/material/dialog';
import { ReservationDetailsDialogComponent } from '../reservation-details-dialog/reservation-details-dialog.component';


@Component({
  selector: 'app-view-reservations',
  templateUrl: './view-reservations.component.html',
  styleUrls: ['./view-reservations.component.css']
})
export class ViewReservationsComponent implements OnInit {

  reservations: any[] = [];
  userRole: string | null = null;
  displayedColumns: string[] = ['reservationId', 'packageName', 'numberOfPackages', 'status', 'actions'];

  statusMap: { [key: number]: string } = {
    0: 'Processing',
    1: 'Accepting',
    2: 'Rejected',
    // Dodaj ostale statuse po potrebi
  };

  constructor(private authService: AuthService, private reservationService: ReservationService, public dialog: MatDialog) {}

  ngOnInit(): void {
    this.userRole = this.authService.getUserRole();
    this.loadUserReservations(); // Učitaj rezervacije kada se komponenta inicijalizuje
  }

  loadUserReservations() {
    if (this.userRole !== 'Customer') {
      alert('Nemate pristup ovom delu. Samo korisnici mogu videti svoje rezervacije.');
      return;
    }
    
    const userId = this.authService.getUserId();
    const userIdNumber = Number(userId);
    
    this.reservationService.getReservationsByUserId(userIdNumber)
      .then(response => {
        // Ako API vraća niz rezervacija
        if (Array.isArray(response)) {
          this.reservations = response; // Dodeli niz rezervacija
        } else {
          // Ako API vraća jedan objekat ili objekat sa rezervacijama
          this.reservations = [response]; // Stavi objekat u niz
        }
      })
      .catch(error => {
        console.error('Error loading reservations:', error);
      });
  }

  openReservationDetails(reservation: any): void {
    // Pozovi backend da dobiješ informacije o paketu na osnovu ID-a rezervacije
    this.reservationService.getPackageFromReservation(reservation.reservationId)
      .then(packageData => {
        // Otvori dijalog sa detaljima o paketu
        this.dialog.open(ReservationDetailsDialogComponent, {
          width: '600px',
          data: { ...reservation, packageDetails: packageData } // Spoji podatke o rezervaciji i paketu
        });
      })
      .catch(error => {
        console.error('Error fetching reservation details:', error);
      });
  }
  
  
  cancelReservation(reservationId: number) {
    this.reservationService.cancelReservationbyUser(reservationId) // Otkazi rezervaciju
      .then(() => {
        alert('Reservation canceled successfully!');
        this.loadUserReservations(); // Ponovno učitaj rezervacije
      })
      .catch(error => {
        console.error('Error canceling reservation:', error);
      });
  }
}
