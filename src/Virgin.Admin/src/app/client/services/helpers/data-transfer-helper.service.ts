import { Injectable } from '@angular/core';
import { CustomerRedirect } from '../../models/customer-redirect';

@Injectable({
    providedIn: 'root',
})
export class DataTransferHelperService {
    private editCustomerRedirect!: CustomerRedirect;

    setEditCustomerRedirect = (model: CustomerRedirect) => this.editCustomerRedirect = model;
    getEditCustomerRedirect = () => this.editCustomerRedirect;
}
