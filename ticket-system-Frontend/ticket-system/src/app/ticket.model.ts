export interface Ticket {
    id?: number;
    creationDate?: string;
    phoneNumber: string;
    governorate: string;
    city: string;
    district: string;
    isHandled?: boolean;
  }
  