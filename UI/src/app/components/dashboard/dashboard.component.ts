import { Component, OnInit } from '@angular/core';
import { EventDetails } from '../../models/EventDetails';
import { EventService } from '../../services/event.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']  // Corrected 'styleUrl' to 'styleUrls'
})
export class DashboardComponent implements OnInit {
  events: EventDetails[] = [];
  userId: string = '';

  constructor(private eventService: EventService) {}

  ngOnInit(): void {
    this.loadEvents();
    this.userId = localStorage.getItem('userId') ?? '';
    console.log('userid in events', this.userId);
  }

  // Load events from the backend and filter upcoming events
  loadEvents() {
    this.eventService.getEvents().subscribe(
      data => {
        const currentDate = new Date();
        console.log('Fetched events:', data);

        // Filter upcoming events (event date should be in the future)
        this.events = data.filter(event => new Date(event.date) > currentDate);

        console.log('Upcoming events:', this.events);
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
