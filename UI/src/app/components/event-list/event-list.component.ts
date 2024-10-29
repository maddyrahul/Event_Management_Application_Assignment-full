import { Component, OnInit } from '@angular/core';
import { EventService } from '../../services/event.service';
import { EventDetails } from '../../models/EventDetails';


@Component({
  selector: 'app-event-list',
  templateUrl: './event-list.component.html',
  styleUrls: ['./event-list.component.css']  // Corrected styleUrls typo
})
export class EventListComponent implements OnInit {
  events: EventDetails[] = [];
  userId: string='';
  constructor(private eventService: EventService) {}

  ngOnInit(): void {
    this.loadEvents();
    this.userId = localStorage.getItem('userId') ?? '';
    console.log("userid in events",this.userId)
  }

  // Load events from the backend
  loadEvents() {
    this.eventService.getEvents().subscribe(
      data => {
        console.log('Fetched events:', data);
        this.events = data;
      },
      error => {
        console.error('Error fetching events:', error);  // Better error handling
      }
    );
  }

  // Delete event and refresh the list
  deleteEvent(eventId: string) {
    if (confirm('Are you sure you want to delete this event?')) {
      this.eventService.deleteEvent(eventId).subscribe(
        () => {
          console.log('Event deleted successfully');
          this.loadEvents();  // Reload the event list after successful deletion
        },
        error => {
          console.error('Error deleting event:', error);  // Log any errors
        }
      );
    }
  }
}
