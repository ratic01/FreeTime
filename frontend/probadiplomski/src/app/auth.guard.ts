import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from './services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private authService: AuthService, private router: Router) { }
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): boolean {
    const token = localStorage.getItem('authToken');
    const userRole = this.authService.getUserRole();

    // Proveravamo da li je korisnik admin ili agent
    if (token) {
      // Then check the user role
      if (userRole === 'Admin' || userRole === 'Agent') {
        return true;  // Allow access if role is valid
      } else {
        // Redirect to dashboard if user is not authorized
        this.router.navigate(['/dashboard']);
        return false;
      }
    } else {
      // Redirect to login page if not authenticated
      this.router.navigate(['/login']);
      return false;
    }
  }
  
}
