// offer.service.ts
import { Injectable } from '@angular/core';
import axios from 'axios';

@Injectable({
  providedIn: 'root'
})
export class OfferService {

  private apiUrl: string = 'https://localhost:44336/api/users';  // Backend URL

  constructor() { }

  // Metod za kreiranje ponude
  /* createOffer(offerData: any): Promise<any> {
    const token = localStorage.getItem('authToken'); // Dohvati token iz LocalStorage-a
    return axios.post(`${this.apiUrl}/create-package`, offerData, {
      headers: { Authorization: `Bearer ${token}` }  // Prosledi token u zaglavlju
    })
    .then(response => response.data)
    .catch(error => {
      console.error('Error creating offer:', error);
      throw error;
    });
  }
 */
  // Metod za dobijanje lista država
  getCountries(): Promise<any> {
    return axios.get(`${this.apiUrl}/countries`)
    .then(response => response.data)
    .catch(error => {
      console.error('Error fetching countries:', error);
      throw error;
    });
  }

  /* getOffers(): Promise<any> {
    const token = localStorage.getItem('authToken');
    return axios.get(`${this.apiUrl}/getallpackages`, {
      headers: { Authorization: `Bearer ${token}` }
    })
    .then(response => response.data)
    .catch(error => {
      console.error('Error fetching offers:', error);
      throw error;
    });
  } */
}
