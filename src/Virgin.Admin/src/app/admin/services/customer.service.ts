import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Customer } from '../models/customer';
import { QRDetails } from '../models/qr-details';
import { Observable } from 'rxjs';
import { appConfig } from 'src/assets/configuration/config';

@Injectable({
  providedIn: 'root',
})
export class CustomerService {
  private _endpoint: string;

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    }),
  };

  constructor(
    private _http: HttpClient,
  ) {
    this._endpoint = `${appConfig.apiUrl}/api/CustomerQR`;
  }

  createCustomer(customer: Customer): Observable<QRDetails> {
    return this._http.post<QRDetails>(
      `${this._endpoint}`,
      customer,
      this.httpOptions
    );
  }
}
