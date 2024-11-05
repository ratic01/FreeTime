import { Component, OnInit } from '@angular/core';
import { TourPackageServiceService } from 'src/app/services/tour-package-service.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-update-offers',
  templateUrl: './update-offers.component.html',
  styleUrls: ['./update-offers.component.css']
})
export class UpdateOffersComponent implements OnInit {

  package: any = {
    packageName: '',
    description: '',
    startDate: '',
    endDate: '',
    pricePerPerson: 0,
    countryName: '',
    numberOfPeople: 0,
    hotelName: '',
    totalPackage: 0
  };
  packageId: number | any; // ID ponude koju ažuriramo

  constructor(
    private tourPackageService: TourPackageServiceService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.packageId = this.route.snapshot.params['id']; // Preuzimanje ID-a iz URL-a
 // Preuzimanje ID-a iz URL-a
    this.loadPackage(); // Učitavanje postojećih podataka ponude
  }

  loadPackage() {
    this.tourPackageService.getPackageById(this.packageId)
      .then(response => {
        this.package = response; // Pretpostavka da je `response` objekat ponude
      })
      .catch(error => {
        console.error('Error loading package:', error);
      });
  }

  updateOffer() {
    this.tourPackageService.updateTourPackage(this.packageId, this.package)
      .then(() => {
        alert('Ponuda je uspešno ažurirana!');
        this.router.navigate(['view-offers']); // Preusmeravanje na stranicu sa svim ponudama
      })
      .catch(error => {
        console.error('Error updating offer:', error);
      });
  }

}
