import { Routes, RouterModule } from '@angular/router';

import { HomeComponent } from './home';
import { LoginComponent } from './login';
import { AuthGuard } from './_helpers';

import { AddClaimComponent } from './components/add-claim/add-claim.component';
import { ClaimDetailsComponent } from './components/claim-details/claim-details.component';
import { ClaimsListComponent } from './components/claims-list/claims-list.component';


const routes: Routes = [
  { path: '', component: ClaimsListComponent, canActivate: [AuthGuard] },
  { path: 'login', component: LoginComponent },
  { path: 'claims', component: ClaimsListComponent },
  { path: 'claims/:id', component: ClaimDetailsComponent },
  { path: 'add', component: AddClaimComponent },

    // otherwise redirect to home
    { path: '**', redirectTo: '' }
];

export const appRoutingModule = RouterModule.forRoot(routes);
