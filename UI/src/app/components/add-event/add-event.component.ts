import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { EventService } from '../../services/event.service';
import { EventDetails } from '../../models/EventDetails';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-event',
  templateUrl: './add-event.component.html',
  styleUrls: ['./add-event.component.css']
})
export class AddEventComponent implements OnInit {
  eventForm!: FormGroup;
  organizerId: string = '';

  constructor(
    private formBuilder: FormBuilder,
    private eventService: EventService,private router: Router
  ) {
    // Get the organizerId from localStorage or another source
    this.organizerId = localStorage.getItem('userId') || '';
  }

  ngOnInit(): void {
    // Initialize the reactive form with validation
    this.eventForm = this.formBuilder.group({
      eventName: ['', [Validators.required, Validators.minLength(3)]],
      date: ['', [Validators.required]],
      location: ['', [Validators.required]],
      description: ['']
    });
  }

  // Getter methods for accessing form controls in the template
  get eventName() { return this.eventForm.get('eventName'); }
  get date() { return this.eventForm.get('date'); }
  get location() { return this.eventForm.get('location'); }

  // Submit method
  addEvent() {
    if (this.eventForm.invalid) {
      // If the form is invalid, show validation errors
      this.eventForm.markAllAsTouched();
      return;
    }

    // Prepare the event object based on form values
    const event: EventDetails = {
      eventId: '', 
      eventName: this.eventForm.value.eventName,
      date: new Date(this.eventForm.value.date).toISOString(),
      location: this.eventForm.value.location,
      description: this.eventForm.value.description || '',
      organizerId: this.organizerId
    };

    // Send the event data to the backend
    this.eventService.addEvent(event).subscribe(
      (createdEvent: EventDetails) => {
        alert('Event added successfully!');
        console.log('Created event:', createdEvent);
        this.router.navigate(['/event-list']);
        // Reset the form after submission
        this.eventForm.reset();
      },
      (error) => {
        console.error('Error adding event:', error);
        alert('Failed to add event. Please try again.');
      }
    );
  }
}
