import { Component, OnInit } from '@angular/core';
import { Output, EventEmitter } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Customer } from 'src/app/admin/models/customer';

@Component({
  selector: 'app-create-customer-form',
  templateUrl: './create-customer-form.component.html',
  styleUrls: ['./create-customer-form.component.scss'],
})
export class CreateCustomerFormComponent implements OnInit {
  @Output() postForm = new EventEmitter<Customer>();

  customerFormGroup = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    name: new FormControl('', [Validators.required]),
    surname: new FormControl('', [Validators.required]),
    city: new FormControl('', [Validators.required]),
    address: new FormControl('', [Validators.required]),
    postal: new FormControl('', [Validators.required, Validators.pattern('^[0-9]*$')]),
  });

  constructor() { }

  ngOnInit() { }

  submitForm() {
    if (this.customerFormGroup.valid) {
      const customer: Customer = {
        email: this.customerFormGroup.controls.email.value,
        givenName: this.customerFormGroup.controls.name.value,
        surname: this.customerFormGroup.controls.surname.value,
        city: this.customerFormGroup.controls.city.value,
        streetAddress: this.customerFormGroup.controls.address.value,
        postalCode: this.customerFormGroup.controls.postal.value,
      };

      this.postForm.emit(customer);
    }
  }
}
