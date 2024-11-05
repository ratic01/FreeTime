// view-offers.component.ts
import { Component, OnInit } from '@angular/core';

import { TourPackageServiceService } from 'src/app/services/tour-package-service.service';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { PackageQuantityDialogComponent } from '../package-quantity-dialog/package-quantity-dialog.component';
import { MatDialog } from '@angular/material/dialog';
import { ReservationService } from 'src/app/services/reservation.service';


@Component({
  selector: 'app-view-offers',
  templateUrl: './view-offers.component.html',
  styleUrls: ['./view-offers.component.css']
})
export class ViewOffersComponent implements OnInit {
  offers: any[] = [];
  packageId:number|any;
  userRole: string | null = null;
  displayedColumns: string[] = ['packageName', 'description', 'startDate', 'endDate', 'pricePerPerson', 'totalPackage', 'numberOfPeople', 'hotelName', 'countryName', 'actions'];

  constructor(private tourpackageservice: TourPackageServiceService,private reservationservice: ReservationService, private router: Router,private authService: AuthService, public dialog: MatDialog) { }

  ngOnInit(): void {
    this.loadOffers();
    this.userRole = this.authService.getUserRole();
  }

  loadOffers() {
    this.tourpackageservice.getAllOffers()
      .then(response => {
        console.log('Response from API:', response);
        this.offers = response;
      })
      .catch(error => {
        console.error('Error loading offers:', error);
      });
  }

  openDialog(packageId: number): void {
    const dialogRef = this.dialog.open(PackageQuantityDialogComponent);

    dialogRef.afterClosed().subscribe(result => {
        console.log('Dialog result:', result); // Dodaj log
        if (result && result > 0) {
            // Ako je rezultat dobar i veći od nule
            this.reserveOffer(packageId, result);
        } else {
            // Ako rezultat nije dobar
            alert('Please enter a valid number of packages to reserve (must be greater than 0).');
            console.log('Invalid input detected');
        }
    });
}


  reserveOffer(packageId: number,numberOfPackages: number) {
    const userId = this.authService.getUserId(); // Dobij ID korisnika iz tokena
    const email = this.authService.getUserEmail(); // Dobij ID korisnika iz tokena
    const reservationData = {
        tourPackageId: packageId,
        userId: userId, // Koristi ID korisnika dobijen iz tokena
        numberOfPackages: numberOfPackages,
        email:email
        
    };    

    this.reservationservice.createReservation(reservationData)
      .then((response: any) => {
          alert('Reservation successful!');
          this.loadOffers();
      })
      .catch((error: any) => {
          console.error('Error creating reservation:', error);
      });
}


  goToUpdateOffer(packageId: number) {
    this.router.navigate(['/update-offers', packageId]); // Navigacija na stranicu za ažuriranje
  }

  goToReserveOffer(packageId: number) {
    this.openDialog(packageId);;  // Navigacija na stranicu za rezervaciju
  }


  
}
