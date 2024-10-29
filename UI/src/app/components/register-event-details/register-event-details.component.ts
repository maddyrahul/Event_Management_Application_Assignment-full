// import { Component, OnInit } from '@angular/core';
// import { Guest } from '../../models/guest.model';
// import { GuestService } from '../../services/guest.service';
// import { ActivatedRoute } from '@angular/router';
// import { EventDetails } from '../../models/EventDetails';
// import { EventService } from '../../services/event.service';

// @Component({
//   selector: 'app-register-event-details',
//   templateUrl: './register-event-details.component.html',
//   styleUrl: './register-event-details.component.css'
// })
// export class RegisterEventDetailsComponent implements OnInit {
//   registeredEvents: Guest[] = [];
//   attendeeId: string = '';
//   events: EventDetails[] = []; // List to hold all event details

//   constructor(
//     private guestService: GuestService,
//     private route: ActivatedRoute,
//     private eventService: EventService
//   ) {}

//   ngOnInit(): void {
//     // Get attendeeId from localStorage (assuming user is logged in and attendeeId is stored)
//     this.attendeeId = localStorage.getItem('userId') || '';
//     this.loadRegisteredEvents();
//   }

//   // Fetch the events registered by the attendee
//   loadRegisteredEvents(): void {
//     this.guestService.getGuestsByAttendeeId(this.attendeeId).subscribe(
//       (data: Guest[]) => {
//         this.registeredEvents = data;

//         // Loop through each registered event and fetch its details
//         this.registeredEvents.forEach(guest => {
//           this.eventService.getEventById(guest.eventId).subscribe(
//             (eventData: EventDetails) => {
//               this.events.push(eventData); // Add each event's details to the events array
//             },
//             (error) => {
//               console.error('Error fetching event details:', error);
//             }
//           );
//         });

//         console.log('Registered events:', this.registeredEvents);
//       },
//       (error) => {
//         console.error('Error fetching registered events:', error);
//       }
//     );
//   }

  
// }



import { Component, OnInit } from '@angular/core';
import { Guest } from '../../models/guest.model';
import { GuestService } from '../../services/guest.service';
import { ActivatedRoute } from '@angular/router';
import { EventDetails } from '../../models/EventDetails';
import { EventService } from '../../services/event.service';

@Component({
  selector: 'app-register-event-details',
  templateUrl: './register-event-details.component.html',
  styleUrls: ['./register-event-details.component.css']
})
export class RegisterEventDetailsComponent implements OnInit {
  registeredEvents: Guest[] = [];
  attendeeId: string = '';
  events: EventDetails[] = []; // List to hold all event details

  constructor(
    private guestService: GuestService,
    private route: ActivatedRoute,
    private eventService: EventService
  ) {}

  ngOnInit(): void {
    // Get attendeeId from localStorage (assuming user is logged in and attendeeId is stored)
    this.attendeeId = localStorage.getItem('userId') || '';
    this.loadRegisteredEvents();
  }

  // Fetch the events registered by the attendee
  loadRegisteredEvents(): void {
    
    this.guestService.getGuestsByAttendeeId(this.attendeeId).subscribe(
      (data: Guest[]) => {
        this.registeredEvents = data;

        // Loop through each registered event and fetch its details
        this.registeredEvents.forEach(guest => {
          this.eventService.getEventById(guest.eventId).subscribe(
            (eventData: EventDetails) => {
              this.events.push(eventData); // Add each event's details to the events array
            },
            (error) => {
              console.error('Error fetching event details:', error);
            }
          );
          console.log("reg details",this.events)
        });

        console.log('Registered events:', this.registeredEvents);
      },
      (error) => {
        console.error('Error fetching registered events:', error);
      }
    );
  }

  // Method to remove a registered event by guestListId
  removeEvent(guestListId: string, index: number): void {
    console.log("guestid",guestListId)
    this.guestService.removeGuest(guestListId).subscribe(
      () => {
        alert('Event registration removed successfully!');

        // Remove the event from the UI
        this.registeredEvents.splice(index, 1); // Remove from the registeredEvents list
        this.events.splice(index, 1); // Also remove from the events list
      },
      (error) => {
        console.error('Error removing event registration:', error);
      }
    );
  }
}
