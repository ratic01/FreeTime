// create-offer.component.ts
import { Component, OnInit } from '@angular/core';
import { OfferService } from 'src/app/services/offer.service';
import { Router } from '@angular/router';
import { TourPackageServiceService } from 'src/app/services/tour-package-service.service';

@Component({
  selector: 'app-create-offer',
  templateUrl: './create-offer.component.html',
  styleUrls: ['./create-offer.component.css']
})
export class CreateOfferComponent {
  package = {
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

  constructor(private tourPackageService: TourPackageServiceService,private router:Router) {}

  async createOffer() {
    try {
      const response = await this.tourPackageService.createTourPackage(this.package);
      alert('Ponuda je uspešno kreirana!');
      console.log(response);
      this.router.navigate(['view-offers'])
    } catch (error) {
      console.error('Greška prilikom kreiranja ponude:', error);
      alert('Došlo je do greške prilikom kreiranja ponude.');
    }
  }
}
