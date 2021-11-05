import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { appConfig } from 'src/assets/configuration/config';
import { CustomerRedirect } from '../models/customer-redirect';

@Injectable({
  providedIn: 'root',
})
export class QrRedirectService {
  private _endpoint: string;

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    }),
  };

  constructor(
    private _http: HttpClient,
  ) {
    this._endpoint = `${appConfig.apiUrl}/api/CustomerRedirect`;
  }

  getQrRedirects(): Observable<CustomerRedirect[]> {
    return this._http.get<CustomerRedirect[]>(`${this._endpoint}`);
  }

  getQrRedirectById(id: number): Observable<CustomerRedirect> {
    return this._http.get<CustomerRedirect>(`${this._endpoint}/${id}`);
  }

  createQrRedirect(customerRedirect: CustomerRedirect): Observable<number> {
    return this._http.post<number>(this._endpoint, customerRedirect, this.httpOptions);
  }

  updateQrRedirect(customerRedirect: CustomerRedirect): Observable<number> {
    return this._http.put<number>(this._endpoint, customerRedirect, this.httpOptions);
  }

  getCustomerTimesViewedSum(): Observable<number> {
    return this._http.get<number>(`${this._endpoint}/times-viewed`);
  }
}
