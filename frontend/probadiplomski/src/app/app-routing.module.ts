import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RegistercomponentComponent } from './components/registercomponent/registercomponent.component';
import { LoginuserComponent } from './components/loginuser/loginuser.component';
import { AuthGuard } from './auth.guard';
import { AgentRegistrationComponent } from './components/agent-registration/agent-registration.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { CreateOfferComponent } from './components/create-offer/create-offer.component';
import { ViewOffersComponent } from './components/view-offers/view-offers.component';
import { UpdateOffersComponent } from './components/update-offers/update-offers.component';
import { ViewReservationsComponent } from './components/view-reservations/view-reservations.component';
import { ViewAllreservationsComponent } from './components/view-allreservations/view-allreservations.component';

const routes: Routes = [
  {path:'register', component:RegistercomponentComponent},  
  { path: 'dashboard', component: DashboardComponent },
  { path: 'create-offer', component: CreateOfferComponent,canActivate: [AuthGuard]}, 
  { path: 'view-offers', component: ViewOffersComponent }, 
  { path: 'update-offers/:id', component: UpdateOffersComponent },
  { path: 'view-reservations', component: ViewReservationsComponent },
  { path: 'view-allreservations', component: ViewAllreservationsComponent },
  {path:'login', component:LoginuserComponent},
  { path: 'register-agent', component: AgentRegistrationComponent },
  { path: '', redirectTo: '/login', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
