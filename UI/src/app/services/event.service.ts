import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { EventDetails } from '../models/EventDetails';

@Injectable({
  providedIn: 'root'
})
export class EventService {

  private apiUrl = 'https://localhost:7270/api/Event';

  constructor(private http: HttpClient) {}

  getEvents(): Observable<EventDetails[]> {
    return this.http.get<EventDetails[]>(this.apiUrl);
  }

  addEvent(event: EventDetails): Observable<any> {
    return this.http.post(this.apiUrl, event);
  }
  // Fetch event details by ID
  getEventById(eventId: string): Observable<EventDetails> {
    return this.http.get<EventDetails>(`${this.apiUrl}/${eventId}`);
  }

  updateEvent(eventId: string, event: EventDetails): Observable<any> {
    return this.http.put(`${this.apiUrl}/${eventId}`, event);
  }

 // Delete event by ID
 deleteEvent(eventId: string): Observable<any> {
  return this.http.delete(`${this.apiUrl}/${eventId}`);  // Ensure this correctly points to the API
}
  
}
