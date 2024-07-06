import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Ticket } from './ticket.model';

export interface PaginatedList<T> {
  items: T[];
  pageIndex: number;
  totalPages: number;
  totalItems: number;
}

@Injectable({
  providedIn: 'root'
})
export class TicketsService {
  private apiUrl = 'https://localhost:7117/api/tickets'; // Update this URL if necessary

  constructor(private http: HttpClient) { }

  getTickets(page: number, pageSize: number): Observable<PaginatedList<Ticket>> {
    return this.http.get<PaginatedList<Ticket>>(`${this.apiUrl}?page=${page}&pageSize=${pageSize}`);
  }

  createTicket(ticket: Ticket): Observable<number> {
    return this.http.post<number>(this.apiUrl, ticket);
  }

  handleTicket(id: number): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}/handle`, {});
  }

  markTicketHandled(id: number): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}/mark-handled`, {});
  }
}
