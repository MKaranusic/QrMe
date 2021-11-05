import { Customer } from './customer';

export interface QRDetails {
  password: string;
  userName: string;
  qrAddress: string;
  customer: Customer;
}
