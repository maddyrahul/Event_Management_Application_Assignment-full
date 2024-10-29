import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BudgetService } from '../../services/budget.service';
import { Budget } from '../../models/budget.model';

@Component({
  selector: 'app-budget',
  templateUrl: './budget.component.html',
  styleUrl: './budget.component.css'
})
export class BudgetComponent {
  budget: Budget = { eventId: '', expenses: 0, revenue: 0 };
  eventId: string;

  constructor(
    private route: ActivatedRoute,
    private budgetService: BudgetService
  ) {
    this.eventId = this.route.snapshot.paramMap.get('eventId')!;
    console.log("coming from budget",this.eventId)
  }

  ngOnInit(): void {
    this.loadBudget();
  }

  loadBudget() {
    this.budgetService.getBudget(this.eventId).subscribe(
      data => {
        if (data) {
          this.budget = data;
          if(this.budget.expenses===0 && this.budget.revenue===0){
            this.budget.eventId='';
            this.budget.budgetId='';
          }
          console.log("data form budget", this.budget)
        } else {
          this.budget.eventId = this.eventId;
        }
      },
      error => console.log(error)
    );
  }

  saveBudget() {
   
    if (this.budget.budgetId) {
      this.budgetService.updateBudget(this.eventId, this.budget).subscribe(
        () => alert('Budget updated successfully!'),
        error => console.log(error)
      );
    } else {
    
      this.budget.eventId = this.eventId;
      this.budgetService.addBudget(this.budget).subscribe(
        () => {
          alert('Budget added successfully!');
          console.log(this.budget); // Log the budget object after successful addition
        },
        error => console.log(error)
      );
      
    }
  }
}
