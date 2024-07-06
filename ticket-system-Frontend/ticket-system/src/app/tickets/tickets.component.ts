import { Component, OnInit } from '@angular/core';
import { TicketsService, PaginatedList } from '../tickets.service';
import { Ticket } from '../ticket.model';

@Component({
  selector: 'app-tickets',
  templateUrl: './tickets.component.html',
  styleUrls: ['./tickets.component.css']
})
export class TicketsComponent implements OnInit {
  tickets: Ticket[] = [];
  newTicket: Ticket = { phoneNumber: '', governorate: '', city: '', district: '', creationDate: '' };
  page = 1;
  pageSize = 5;
  totalPages = 1;
  totalItems = 0;
  displayedColumns: string[] = ['id', 'creationDate', 'phoneNumber', 'governorate', 'city', 'district', 'isHandled', 'actions'];

  governorates = ['Governorate1', 'Governorate2', 'Governorate3'];
  cities = ['City1', 'City2', 'City3'];
  districts = ['District1', 'District2', 'District3'];

  constructor(private ticketsService: TicketsService) { }

  ngOnInit(): void {
    this.loadTickets();
    setInterval(() => {
      this.updateTicketStatuses();
    }, 60000); // Update ticket statuses every minute
  }

  loadTickets(): void {
    this.ticketsService.getTickets(this.page, this.pageSize).subscribe(response => {
      this.tickets = response.items;
      this.totalPages = response.totalPages;
      this.totalItems = response.totalItems;
    });
  }

  createTicket(): void {
    this.ticketsService.createTicket(this.newTicket).subscribe(() => {
      this.newTicket = { phoneNumber: '', governorate: '', city: '', district: '', creationDate: '' };
      this.loadTickets();
    });
  }

  handleTicket(id: number): void {
    if (id !== undefined) {
      this.ticketsService.handleTicket(id).subscribe(() => {
        this.loadTickets();
      });
    }
  }

  getTicketColor(ticket: Ticket): string {
    const creationDate = new Date(ticket.creationDate + 'Z'); // Parse as UTC
    const now = new Date();
    const minutes = (now.getTime() - creationDate.getTime()) / (1000 * 60);

    console.log(`Ticket ID: ${ticket.id}, Time now is: ${now.toLocaleString()}, Creation Date: ${creationDate.toLocaleString()}, Minutes since creation: ${minutes}`); // Debugging line

    if (minutes >= 60) return 'table-danger';
    if (minutes >= 45) return 'table-primary';
    if (minutes >= 30) return 'table-success';
    if (minutes >= 15) return 'table-warning';
    return '';
  }

  updateTicketStatuses(): void {
    const now = new Date();
    this.tickets.forEach(ticket => {
      const creationDate = new Date(ticket.creationDate + 'Z');
      const minutes = (now.getTime() - creationDate.getTime()) / (1000 * 60);
      if (minutes >= 60 && !ticket.isHandled) {
        this.ticketsService.handleTicket(ticket.id!).subscribe(() => {
          ticket.isHandled = true;
          this.loadTickets(); 
        });
      }
    });
  }

  previousPage(): void {
    if (this.page > 1) {
      this.page--;
      this.loadTickets();
    }
  }

  nextPage(): void {
    if (this.page < this.totalPages) {
      this.page++;
      this.loadTickets();
    }
  }
}
