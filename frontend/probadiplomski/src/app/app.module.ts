import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { RegistercomponentComponent } from './components/registercomponent/registercomponent.component';
import { FormsModule } from '@angular/forms';
import { LoginuserComponent } from './components/loginuser/loginuser.component';
import { AgentRegistrationComponent } from './components/agent-registration/agent-registration.component';

import { CreateOfferComponent } from './components/create-offer/create-offer.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { ViewOffersComponent } from './components/view-offers/view-offers.component';
import { UpdateOffersComponent } from './components/update-offers/update-offers.component';
import { ReserveOfferComponent } from './components/reserve-offer/reserve-offer.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { PackageQuantityDialogComponent } from './components/package-quantity-dialog/package-quantity-dialog.component';
import { MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field'; 
import { MatInputModule } from '@angular/material/input'; 
import { MatButtonModule } from '@angular/material/button';
import { ViewReservationsComponent } from './components/view-reservations/view-reservations.component';
import { ViewAllreservationsComponent } from './components/view-allreservations/view-allreservations.component';
import { MatTableModule } from '@angular/material/table';
import { ReservationDetailsDialogComponent } from './components/reservation-details-dialog/reservation-details-dialog.component';
import { ReservationHistoryDialogComponent } from './components/reservation-history-dialog/reservation-history-dialog.component';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';



@NgModule({
  declarations: [
    AppComponent,
    RegistercomponentComponent,
    LoginuserComponent,
    AgentRegistrationComponent,    
    CreateOfferComponent,
    DashboardComponent,
    ViewOffersComponent,
    UpdateOffersComponent,
    ReserveOfferComponent,
    PackageQuantityDialogComponent,
    ViewReservationsComponent,
    ViewAllreservationsComponent,
    ReservationDetailsDialogComponent,
    ReservationHistoryDialogComponent,
    
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    BrowserAnimationsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatTableModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatToolbarModule,
    MatIconModule,
    MatCardModule
    
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
