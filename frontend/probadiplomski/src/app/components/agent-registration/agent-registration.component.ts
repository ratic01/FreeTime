import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-agent-registration',
  templateUrl: './agent-registration.component.html',
  styleUrls: ['./agent-registration.component.css']
})
export class AgentRegistrationComponent  {
  agentData = {
    firstName: '',
    lastName: '',
    email: '',
    password: ''
  };


  constructor(private authService: AuthService, private router:Router) { }

  

  registerAgent(){
    this.authService.registerAgent(this.agentData)
    .then(response=>{
      console.log('Agent registered successfully:', response);
      alert('Agent registered successfully!');
      this.router.navigate(['/']); // Redirect after success
    })
    .catch(error => {
      console.error('Error registering agent:', error);
      alert('Error registering agent.');
    });
  }

}
