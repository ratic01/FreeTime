import { Injectable } from '@angular/core';
import axios from 'axios';
import { jwtDecode } from 'jwt-decode';







@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private apiUrl: string='https://localhost:44336/api/users';

  constructor(){}


  isLoggedIn(): boolean {
    const token = localStorage.getItem('authToken');
    return !!token; // Vraća true ako je korisnik prijavljen
  }

  getUserRole(): string | null {
    const token = localStorage.getItem('authToken');
    console.log('Token:', token); // Prikazuje token u konzoli
    if (token) {
      try {
        const decoded: any = jwtDecode(token);
        console.log('Decoded JWT:', decoded); // Prikazuje dekodirane vrednosti
        return decoded['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] || null; // Pretpostavljamo da JWT sadrži polje 'role'
      } catch (error) {
        console.error('Error decoding token:', error); // Prikazuje greške prilikom dekodiranja
      }
    }
    return null;
  }

  registerUser(userData:{firstName:string; lastName:string; email:string; password:string}) : Promise<any>{

    return axios.post(`${this.apiUrl}/register`, userData)
    .then(response=>response.data)
    .catch(error=>{
      console.error('Error creating user: ', error); // Prikaz greške
        throw error; // Prosledi grešku kako bi se uhvatila u komponenti
      });
  }

  loginUser(loginData: any): Promise<any> {
    return axios.post(`${this.apiUrl}/login`, loginData)
      .then(response => {
        console.log('Login response:', response.data); // Prikazuje ceo odgovor radi provere
  
        // Proveri da li je response.data string ili objekat
        if (typeof response.data === 'string') {
          // Ako je response.data string, to je verovatno token
          console.log('Token found:', response.data);
          return { token: response.data };  // Vraća objekat sa tokenom
        } else if (response.data && response.data.token) {
          // Ako je response.data objekat i ima token
          console.log('Token found:', response.data.token);
          return response.data;  // Vraća ceo objekat sa tokenom
        }
  
        // Ako token ne postoji u odgovoru
        console.error('Token not found in response');
        throw new Error('Token not found in response');
      })
      .catch(error => {
        console.error('Error during login:', error.response?.data?.message || error.message);
        throw new Error(error.response?.data?.message || 'Došlo je do greške. Pokušajte ponovo kasnije.');
      });
  }

  getUserId(): string | null {
    const token = localStorage.getItem('authToken');
    if (token) {
        try {
            const decoded: any = jwtDecode(token);
            return decoded.sub; // Vraća ID korisnika koji se čuva kao 'sub' claim
        } catch (error) {
            console.error('Error decoding token:', error);
        }
    }
    return null;
}

getUserEmail(): string | null {
  const token = localStorage.getItem('authToken');
  if (token) {
    try {
      const decoded: any = jwtDecode(token);
      return decoded.email; // Vraća email iz dekodiranog tokena
    } catch (error) {
      console.error('Error decoding token:', error);
    }
  }
  return null; // Ako token nije pronađen ili ne može da se dekodira, vraća null
}

  
  
  

logout() {
  localStorage.removeItem('authToken'); // Ukloni token iz lokalne memorije
  // Ovde možete dodati dodatne akcije ako su potrebne
}



  // Primer korišćenja tokena u registraciji agenta
  registerAgent(agentData: { firstName: string; lastName: string; email: string; password: string }): Promise<any> {
    const token = localStorage.getItem('authToken');  // Proveri da li je token sačuvan
    return axios.post(`${this.apiUrl}/register-agent`, agentData, {
      headers: { Authorization: `Bearer ${token}` }
    })
      .then(response => response.data)
      .catch(error => {
        console.error('Error registering agent: ', error);
        throw error;
      });
  }


 
}
