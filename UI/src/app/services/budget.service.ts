import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { Budget } from '../models/budget.model';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class BudgetService {

  private apiUrl = 'https://localhost:7270/api/Budget';

  constructor(private http: HttpClient) {}

  getBudget(eventId: string): Observable<Budget> {
    return this.http.get<Budget>(`${this.apiUrl}/${eventId}`);
  }

  addBudget(budget: Budget): Observable<any> {
    return this.http.post(this.apiUrl, budget);
  }

  updateBudget(eventId: string, budget: Budget): Observable<any> {
    return this.http.put(`${this.apiUrl}/${eventId}`, budget);
  }
}
