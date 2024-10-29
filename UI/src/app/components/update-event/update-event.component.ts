import { Component, OnInit } from '@angular/core';
import { EventDetails } from '../../models/EventDetails';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { EventService } from '../../services/event.service';

@Component({
  selector: 'app-update-event',
  templateUrl: './update-event.component.html',
  styleUrl: './update-event.component.css'
})
export class UpdateEventComponent implements OnInit {
  eventForm!: FormGroup;
  eventId!: string;
  organizerId: string = '';

  constructor(
    private formBuilder: FormBuilder,
    private eventService: EventService,
    private route: ActivatedRoute,
    private router: Router // Router service for navigation
  ) {
    // Get the organizerId from localStorage or another source
    this.organizerId = localStorage.getItem('userId') || '';
  }

  ngOnInit(): void {
    // Retrieve the event ID from the route parameters
    this.eventId = this.route.snapshot.paramMap.get('eventId') || '';

    // Initialize the reactive form with validation
    this.eventForm = this.formBuilder.group({
      eventName: ['', [Validators.required, Validators.minLength(3)]],
      date: ['', [Validators.required]],
      location: ['', [Validators.required]],
      description: ['']
    });

    // Load the existing event details if eventId is available
    if (this.eventId) {
      this.loadEventDetails(this.eventId);
    }
  }

  // Load event details for editing
  loadEventDetails(eventId: string) {
    this.eventService.getEventById(eventId).subscribe(
      (event: EventDetails) => {
        this.eventForm.patchValue({
          eventName: event.eventName,
          date: new Date(event.date).toISOString().split('T')[0], // Format the date
          location: event.location,
          description: event.description
        });
      },
      (error) => {
        console.error('Error loading event details:', error);
        alert('Failed to load event details.');
      }
    );
  }

  // Getter methods for accessing form controls in the template
  get eventName() { return this.eventForm.get('eventName'); }
  get date() { return this.eventForm.get('date'); }
  get location() { return this.eventForm.get('location'); }

  // Submit method for updating the event
  updateEvent() {
    if (this.eventForm.invalid) {
      // If the form is invalid, show validation errors
      this.eventForm.markAllAsTouched();
      return;
    }

    // Prepare the updated event object based on form values
    const updatedEvent: EventDetails = {
      eventId: this.eventId, // Ensure the eventId is set for the update
      eventName: this.eventForm.value.eventName,
      date: new Date(this.eventForm.value.date).toISOString(),
      location: this.eventForm.value.location,
      description: this.eventForm.value.description || '',
      organizerId: this.organizerId
    };

    // Send the updated event data to the backend
    this.eventService.updateEvent(this.eventId, updatedEvent).subscribe(
      (updatedEventDetails: EventDetails) => {
        alert('Event updated successfully!');
        console.log('Updated event:', updatedEventDetails);

        // Navigate to the event list page after successful update
        this.router.navigate(['/event-list']);
      },
      (error) => {
        console.error('Error updating event:', error);
        alert('Failed to update event. Please try again.');
      }
    );
  }
}