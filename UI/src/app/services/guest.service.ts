import { Injectable } from '@angular/core';
import { Guest } from '../models/guest.model';
import { Observable } from 'rxjs/internal/Observable';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class GuestService {
  private apiUrl = 'https://localhost:7270/api/GuestList';
  constructor(private http: HttpClient) {}

  getGuestsByEvent(eventId: string): Observable<Guest[]> {
    return this.http.get<Guest[]>(`${this.apiUrl}/event/${eventId}`);
  }

  addGuest(guest: Guest): Observable<any> {
    return this.http.post(this.apiUrl, guest);
  }

  removeGuest(guestListId: string): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${guestListId}`);
  }

  // Get All Guest Lists
  getAllGuests(): Observable<Guest[]> {
    return this.http.get<Guest[]>(`${this.apiUrl}/all`);
  }

  // Get Guests by AttendeeId
  getGuestsByAttendeeId(attendeeId: string): Observable<Guest[]> {
    return this.http.get<Guest[]>(`${this.apiUrl}/attendee/${attendeeId}`);
  }

// Fetch a guest by their GuestListId
getGuestById(guestListId: string): Observable<Guest> {
  return this.http.get<Guest>(`${this.apiUrl}/${guestListId}`);
}

  // Edit a guest by guestListId
  editGuest(guestId: string, guest: Guest): Observable<any> {
    return this.http.put(`${this.apiUrl}/${guestId}`, guest);
  }
}
