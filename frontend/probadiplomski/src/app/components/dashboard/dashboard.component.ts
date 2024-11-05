import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { Router } from '@angular/router';


@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit  {

  userRole: string | null = null;

  constructor(public authService: AuthService, private router:Router ) { }


  ngOnInit(): void {   
    this.userRole = this.authService.getUserRole();
  }

  logout() {
    this.authService.logout(); // Pozivamo metodu za izlogovanje
    this.router.navigate(['/login']); // Preusmeravamo korisnika na stranicu za prijavu
  }

  


  

}
