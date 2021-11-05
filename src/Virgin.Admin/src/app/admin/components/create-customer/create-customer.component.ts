import { Component } from '@angular/core';
import { Customer } from '../../models/customer';
import { CustomerService } from '../../services/customer.service';
import { QRDetails } from '../../models/qr-details';

@Component({
  selector: 'app-create-customer',
  templateUrl: './create-customer.component.html',
  styleUrls: ['./create-customer.component.scss'],
})
export class CreateCustomerComponent {

  resultView = false;
  data!: QRDetails;

  constructor(private customerService: CustomerService) {
  }

  postForm(customer: Customer) {
    this.customerService.createCustomer(customer).subscribe(x => {
      this.data = x;
      this.resultView = true;
    });
  }
}
