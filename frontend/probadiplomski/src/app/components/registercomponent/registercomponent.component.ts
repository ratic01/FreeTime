import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-registercomponent',
  templateUrl: './registercomponent.component.html',
  styleUrls: ['./registercomponent.component.css']
})
export class RegistercomponentComponent  {

  newUser={
    firstName:'',
    lastName:'',
    email:'',
    password:''
  };


  constructor(private authService:AuthService, private router:Router){}


  registerUser(){
    this.authService.registerUser(this.newUser)
    .then(response=>{
      console.log('User created successfully:', response); // Ako je uspešno, ispiši odgovor
      alert('User created successfully!'); // Obavesti korisnika da je registracija uspešna
      this.router.navigate(['/login']);
    })
    .catch(error => {
      console.error('Error creating user:', error); // Ako je greška, ispiši grešku
      alert('Error creating user.'); // Obavesti korisnika da je došlo do greške
    });
  }
 

}
