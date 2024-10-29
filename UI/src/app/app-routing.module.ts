import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { AddEventComponent } from './components/add-event/add-event.component';
import { AddGuestComponent } from './components/add-guest/add-guest.component';
import { BudgetComponent } from './components/budget/budget.component';
import { EventListComponent } from './components/event-list/event-list.component';
import { UpdateEventComponent } from './components/update-event/update-event.component';
import { GuestListComponent } from './components/guest-list/guest-list.component';
import { EditGuestComponent } from './components/edit-guest/edit-guest.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { AuthGuard } from './guards/auth.guard';
import { AdminGuard } from './guards/admin.guard';
import { EventDetailsComponent } from './components/event-details/event-details.component';
import { RegisterEventDetailsComponent } from './components/register-event-details/register-event-details.component';

const routes: Routes = [

  {path:'login',component:LoginComponent},
  {path:'add-event',component:AddEventComponent,canActivate: [AdminGuard]},
  {path:'add-guest/:eventId/:userId',component:AddGuestComponent,canActivate: [AuthGuard]},
  {path:'event-list',component:EventListComponent,canActivate: [AdminGuard]},
  {path: 'budget/:eventId', component: BudgetComponent ,canActivate: [AuthGuard]},
  {path: 'update-event/:eventId',component:UpdateEventComponent,canActivate: [AdminGuard]},
  {path: 'guest-list', component: GuestListComponent,canActivate: [AuthGuard] },
  {path: 'edit-guest/:guestListId/:eventId', component: EditGuestComponent ,canActivate: [AuthGuard]},
  {path:'dashboard',component:DashboardComponent,canActivate: [AuthGuard]},
  { path: 'event-details/:eventId', component: EventDetailsComponent ,canActivate: [AuthGuard]},
  { path: 'register-event-details', component: RegisterEventDetailsComponent ,canActivate: [AuthGuard]},
  { path: '**', redirectTo: '/login', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
