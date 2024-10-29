import { Component, OnInit } from '@angular/core';
import { Guest } from '../../models/guest.model';
import { GuestService } from '../../services/guest.service';
import { Router } from '@angular/router';
import { EventService } from '../../services/event.service';
import { EventDetails } from '../../models/EventDetails';

@Component({
  selector: 'app-guest-list',
  templateUrl: './guest-list.component.html',
  styleUrl: './guest-list.component.css'
})
export class GuestListComponent implements OnInit {

  guests: Guest[] = [];
  events: { [key: string]: string } = {}; // Mapping of eventId to eventName

  constructor(private guestService: GuestService, private router: Router,private eventService: EventService) {}
  ngOnInit(): void {
    this.loadAllGuests();
  }

  // Method to load all guest lists and fetch corresponding event names
  loadAllGuests(): void {
    this.guestService.getAllGuests().subscribe({
      next: (data: Guest[]) => {
        this.guests = data;

        // Fetch event details for each guest and map eventId to eventName
        this.guests.forEach(guest => {
          if (guest.eventId) {
            this.eventService.getEventById(guest.eventId).subscribe({
              next: (event: EventDetails) => {
                this.events[guest.eventId] = event.eventName;
              },
              error: (error) => {
                console.log('Error fetching event details', error);
              }
            });
          }
        });
      },
      error: (error) => {
        console.log('Error fetching all guest lists', error);
      }
    });
  }

  editGuest(guestListId: string, eventId: string): void {
    this.router.navigate(['/edit-guest', guestListId,eventId]); 
  }

  deleteGuest(guestId: string): void {
    if (confirm('Are you sure you want to delete this guest?')) {
      this.guestService.removeGuest(guestId).subscribe({
        next: () => {
          alert('Guest deleted successfully!');
          this.loadAllGuests(); // Reload guest list after deletion
        },
        error: (error) => {
          console.log('Error deleting guest', error);
        }
      });
    }
  }
}
