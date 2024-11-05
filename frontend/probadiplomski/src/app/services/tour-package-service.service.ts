import { Injectable } from '@angular/core';
import axios from 'axios';

@Injectable({
  providedIn: 'root'
})
export class TourPackageServiceService {

  private apiUrl = 'https://localhost:44336/api/Users';  // Zameni sa pravim URL-om API-ja

  constructor() {}

  async createTourPackage(packageData: any): Promise<any> {
    const token = localStorage.getItem('authToken'); // Preuzimanje tokena iz localStorage-a

    if (!token) {
      throw new Error('No auth token found');
    }
    try {
      const response = await axios.post(`${this.apiUrl}/create-package`, packageData, {
        headers: {
          Authorization: `Bearer ${token}`,
          'Content-Type': 'application/json'
        }
      });
      return response.data;
    } catch (error) {
      throw error;
    }
  }

  getAllOffers(): Promise<any> {
    const token = localStorage.getItem('authToken'); // Uzimanje tokena iz localStorage

    return axios.get(`${this.apiUrl}/getallpackages`, {
      headers: { Authorization: `Bearer ${token}` }
    })
    .then(response => response.data)
    .catch(error => {
      console.error('Error fetching offers:', error);
      throw error;
    });
  }

    // Method to get a package by ID
    getPackageById(packageId: number): Promise<any> {
      const token = localStorage.getItem('authToken');
      
      return axios.get(`${this.apiUrl}/getpackagebyid/${packageId}`, {
        headers: { Authorization: `Bearer ${token}` }
      })
      .then(response => response.data)
      .catch(error => {
        console.error('Error fetching package:', error);
        throw error;
      });
    }
  
    // Method to update a tour package
    updateTourPackage(packageId: number, packageData: any): Promise<any> {
      const token = localStorage.getItem('authToken');
  
      return axios.put(`${this.apiUrl}/update-package/${packageId}`, packageData, {
        headers: {
          Authorization: `Bearer ${token}`,
          'Content-Type': 'application/json'
        }
      })
      .then(response => response.data)
      .catch(error => {
        console.error('Error updating package:', error);
        throw error;
      });
    }

    
}
