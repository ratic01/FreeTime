import { Injectable } from '@angular/core';
import axios from 'axios';

@Injectable({
  providedIn: 'root'
})
export class ReservationService {


  private apiUrl = 'https://localhost:44336/api/Users';
  constructor() { }

  async createReservation(reservationData: any): Promise<any> {
    const token = localStorage.getItem('authToken'); // Preuzimanje tokena iz localStorage-a

    if (!token) {
      throw new Error('No auth token found');
    }

    try {
      const response = await axios.post(`${this.apiUrl}/create-reservation`, reservationData, {
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

  async getReservationsByUserId(userId: number): Promise<any[]> {
    const token = localStorage.getItem('authToken'); // Uzimanje tokena iz localStorage

    if (!token) {
      throw new Error('No auth token found');
    }

    try {
      const response = await axios.get(`${this.apiUrl}/getreservationsbyuserid/${userId}`, {
        headers: {
          Authorization: `Bearer ${token}`
        }
      });
      
      console.log('API response:', response.data); // Dodaj logovanje odgovora
      return response.data; // Proveri da li je ovo niz ili jedan objekat
    } catch (error) {
      console.error('Error fetching reservations:', error);
      throw error;
    }
    
  }

  async cancelReservationbyUser(reservationId: number): Promise<void> {
    const token = localStorage.getItem('authToken'); // Uzimanje tokena iz localStorage

    if (!token) {
      throw new Error('No auth token found');
    }

    try {
      await axios.delete(`${this.apiUrl}/User-cancel-reservation/${reservationId}`, {
        headers: {
          Authorization: `Bearer ${token}`
        }
      });
    } catch (error) {
      console.error('Error canceling reservation:', error);
      throw error;
    }
  }

  getAllReservations(): Promise<any> {
    const token = localStorage.getItem('authToken'); // Uzimanje tokena iz localStorage
    if (!token) {
      throw new Error('No auth token found');
    }
  
    return axios.get(`${this.apiUrl}/allreservations`, {
      headers: {
        Authorization: `Bearer ${token}`
      }
    })
    .then(response => response.data)
    .catch(error => {
      console.error('Error fetching all reservations:', error);
      throw error;
    });
  }
  
  
  acceptReservation(reservationId: number): Promise<any> {
    const token = localStorage.getItem('authToken'); // Preuzimanje tokena iz localStorage

    if (!token) {
        throw new Error('No auth token found'); // Greška ako nema tokena
    }

    return axios.post(`${this.apiUrl}/accept-reservation`, { reservationId }, {
        headers: {
            Authorization: `Bearer ${token}`, // Dodavanje tokena u zaglavlje
            'Content-Type': 'application/json' // Specifikacija tipa sadržaja
        }
    })
    .then(response => response.data)
    .catch(error => {
        console.error('Error accepting reservation:', error);
        throw error;
    });
}


  
rejectReservation(reservationId: number): Promise<any> {
  const token = localStorage.getItem('authToken'); // Preuzimanje tokena iz localStorage

  if (!token) {
      throw new Error('No auth token found'); // Greška ako nema tokena
  }

  return axios.delete(`${this.apiUrl}/deny-reservation`, {
      headers: {
          Authorization: `Bearer ${token}`, // Dodavanje tokena u zaglavlje
          'Content-Type': 'application/json' // Specifikacija tipa sadržaja
      },
      data: { reservationId } // Prosledi rezervaciju u telu zahteva
  })
  .then(response => response.data)
  .catch(error => {
      console.error('Error rejecting reservation:', error);
      throw error;
  });
}



// Metoda za dobijanje paketa na osnovu ID-a rezervacije
async getPackageFromReservation(reservationId: number): Promise<any> {
  const token = localStorage.getItem('authToken'); // Preuzimanje tokena iz localStorage

  if (!token) {
    throw new Error('No auth token found');
  }

  try {
    const response = await axios.get(`${this.apiUrl}/getpackagefromreservation/${reservationId}`, {
      headers: {
        Authorization: `Bearer ${token}`, // Dodavanje tokena u zaglavlje
        'Content-Type': 'application/json'
      }
    });
    return response.data;
  } catch (error) {
    console.error('Error fetching package from reservation:', error);
    throw error;
  }
}

async getReservationEvents(reservationId: number): Promise<any> {
  const token = localStorage.getItem('authToken');

  if (!token) {
    throw new Error('No auth token found');
  }

  const response = await axios.get(`${this.apiUrl}/events/${reservationId}`, {
    headers: {
      Authorization: `Bearer ${token}`,
      'Content-Type': 'application/json'
    }
  });
  return response.data;
}




  
}
