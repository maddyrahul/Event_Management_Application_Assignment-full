import { Component, OnInit } from '@angular/core';
import { EventDetails } from '../../models/EventDetails';
import { EventService } from '../../services/event.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Budget } from '../../models/budget.model';
import { BudgetService } from '../../services/budget.service';

@Component({
  selector: 'app-event-details',
  templateUrl: './event-details.component.html',
  styleUrl: './event-details.component.css'
})
export class EventDetailsComponent implements OnInit {
  event: EventDetails | null = null;
  eventId: string = '';
  userId: string = '';
  budget: Budget | null = null;  // Store the budget details

  constructor(private eventService: EventService, private route: ActivatedRoute,private router: Router, private budgetService:BudgetService) {}

  ngOnInit(): void {
    // Get eventId from the route
    this.eventId = this.route.snapshot.paramMap.get('eventId')!;
    this.userId = localStorage.getItem('userId') || '';
    this.loadEventDetails();
    this.loadBudget(); 
  }

  // Load the event details
  loadEventDetails(): void {
    this.eventService.getEventById(this.eventId).subscribe(
      (data: EventDetails) => {
        this.event = data;
      },
      (error) => {
        console.error('Error fetching event details:', error);
      }
    );
  }

   // Load the budget details
   loadBudget(): void {
    this.budgetService.getBudget(this.eventId).subscribe(
      (data: Budget) => {
        this.budget = data;
        console.log("budget",this.budget)
      },
      (error) => {
        console.error('Error fetching budget details:', error);
      }
    );
  }

   // Method to navigate back to the dashboard
   goBack(): void {
    this.router.navigate(['/dashboard']);
  }

  
}
