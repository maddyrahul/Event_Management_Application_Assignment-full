import { Component } from '@angular/core';
import { GuestService } from '../../services/guest.service';
import { Guest } from '../../models/guest.model';
import { ActivatedRoute } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-add-guest',
  templateUrl: './add-guest.component.html',
  styleUrl: './add-guest.component.css'
})
export class AddGuestComponent {
  guest: Guest = { guestListId: '', eventId: '', attendeeId: '', userName: '', email: '' };
  userId: string;
  eventId: string;
  username: string | null = null;
  role: string | null = null;
  errorMessage: string | null = null; // Property to store error message

  constructor(private route: ActivatedRoute, private guestService: GuestService, private authService: AuthService) {
    this.userId = this.route.snapshot.paramMap.get('userId')!;
    this.eventId = this.route.snapshot.paramMap.get('eventId')!;
    this.username = localStorage.getItem('username');
    if (this.isLoggedIn()) {
      this.role = this.authService.getRole();
    }
  }

  addGuest() {
    this.guest.eventId = this.eventId;
    this.guest.attendeeId = this.userId;

    // Reset error message
    this.errorMessage = null;

    this.guestService.addGuest(this.guest).subscribe(
      () => {
        if (this.role == 'Organizer') {
          alert('Guest added successfully!');
        }
        if (this.role == 'Attendee') {
          alert('Event registered successfully!');
        }
      },
      error => {
        // Check if the error is a BadRequest and display the error message
        if (error.status === 400 && error.error?.Message) {
          this.errorMessage = error.error.Message; // Set error message
        } else {
          console.log(error);
        }
      }
    );
  }

  isLoggedIn(): boolean {
    return this.authService.isAuthenticated();
  }
}
