import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { GuestService } from '../../services/guest.service';
import { Guest } from '../../models/guest.model';

@Component({
  selector: 'app-edit-guest',
  templateUrl: './edit-guest.component.html',
  styleUrls: ['./edit-guest.component.css']
})
export class EditGuestComponent implements OnInit {
  guestForm!: FormGroup;
  guestListId!: string;
  eventId!: string;
  attendeeId: string = '';

  constructor(
    private formBuilder: FormBuilder,
    private guestService: GuestService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    this.attendeeId = localStorage.getItem('userId') || ''; // Get attendeeId from localStorage
  }

  ngOnInit(): void {
    // Retrieve guestListId and eventId from route parameters
    this.guestListId = this.route.snapshot.paramMap.get('guestListId') || '';
    this.eventId = this.route.snapshot.paramMap.get('eventId') || '';

    // Initialize reactive form with validation
    this.guestForm = this.formBuilder.group({
      userName: ['', [Validators.required, Validators.minLength(3)]],
      email: ['', [Validators.required, Validators.email]]
    });

    // Load guest details if attendeeId is available
    if (this.guestListId) {
      this.loadGuestDetails(this.guestListId);
    }
  }

  // Load guest details for editing and prefill the form using patchValue
  loadGuestDetails(guestListId: string) {
    this.guestService.getGuestById(guestListId).subscribe(
      (guests: Guest) => {
        console.log("guests",guests)
       
         
        
            this.guestForm.patchValue({
              userName: guests.userName,
              email: guests.email
            });
      
      },
      (error) => {
        console.error('Error loading guest details:', error);
        alert('Failed to load guest details.');
      }
    );
  }

  // Getter methods for accessing form controls in the template
  get userName() { return this.guestForm.get('userName'); }
  get email() { return this.guestForm.get('email'); }

  // Submit method for updating the guest
  updateGuest() {
    if (this.guestForm.invalid) {
      // If the form is invalid, show validation errors
      this.guestForm.markAllAsTouched();
      return;
    }

    // Prepare the updated guest object based on form values
    const updatedGuest: Guest = {
      guestListId: this.guestListId, // Ensure guestListId is set for the update
      eventId: this.eventId, // Ensure eventId is set
      attendeeId: this.attendeeId, // Ensure attendeeId is set
      userName: this.guestForm.value.userName,
      email: this.guestForm.value.email
    };

    // Send the updated guest data to the backend
    this.guestService.editGuest(this.guestListId, updatedGuest).subscribe(
      (updatedGuestDetails: Guest) => {
        alert('Guest updated successfully!');
        console.log('Updated guest:', updatedGuestDetails);

        // Navigate to the guest list page after successful update
        this.router.navigate(['/guest-list']);
      },
      (error) => {
        console.error('Error updating guest:', error);
        alert('Failed to update guest. Please try again.');
      }
    );
  }
}
