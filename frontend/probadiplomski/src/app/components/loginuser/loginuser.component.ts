// loginuser.component.ts
import { Component } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-loginuser',
  templateUrl: './loginuser.component.html',
  styleUrls: ['./loginuser.component.css']
})
export class LoginuserComponent {
  loginData = {
    email: '',
    password: ''
  };

  errorMessage: string = '';

  constructor(private authService: AuthService, private router: Router) { }

  loginUser() {
    this.authService.loginUser(this.loginData)
      .then(response => {
        console.log('User logged in successfully:', response);
        
        const token = response.token; // Pronađi token
        if (token) {
          localStorage.setItem('authToken', token);  // Sačuvaj token u localStorage
          console.log('Token saved:', token);
          
          // Navigacija na dashboard ako je prijava uspešna
          this.router.navigate(['/dashboard']);
        } else {
          // Ako token nije prisutan, postaviti grešku
          this.errorMessage = 'Greška prilikom prijave. Pokušajte ponovo.'; 
        }
      })
      .catch(error => {
        console.error('Error logging in:', error.message);
        
        // Postavi poruku o grešci i ostani na istoj stranici
        this.errorMessage = 'Neispravni podaci za prijavu. Pokušajte ponovo.'; 
        // Ne navigiraj se na dashboard
      });
}

  



  navigateToRegistration() {
    this.router.navigate(['/register']);
  }
  
  
}
