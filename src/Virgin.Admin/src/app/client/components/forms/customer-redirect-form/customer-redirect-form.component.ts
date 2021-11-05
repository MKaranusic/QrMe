import { Component, EventEmitter, OnInit, Output, Input } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { CustomerRedirect } from 'src/app/client/models/customer-redirect';

@Component({
  selector: 'app-customer-redirect-form',
  templateUrl: './customer-redirect-form.component.html',
  styleUrls: ['./customer-redirect-form.component.scss']
})
export class CustomerRedirectFormComponent implements OnInit {
  @Input() initialCustomerRedirect: CustomerRedirect = {
    name: '',
    targetUrl: '',
    isActive: true,
  }
  @Output() postForm = new EventEmitter<CustomerRedirect>();

  customerRedirectFormGroup!: FormGroup;

  constructor() { }

  ngOnInit() {
    this.customerRedirectFormGroup = new FormGroup({
      name: new FormControl(this.initialCustomerRedirect.name, [Validators.required, Validators.maxLength(75)]),
      targetUrl: new FormControl(this.initialCustomerRedirect.targetUrl, [Validators.required]),
      isActive: new FormControl(this.initialCustomerRedirect.isActive, [Validators.required]),
    });
  }

  submitForm() {
    if (this.customerRedirectFormGroup.valid) {
      const customerRedirect: CustomerRedirect = {
        id: this.initialCustomerRedirect.id,
        name: this.customerRedirectFormGroup.controls.name.value,
        targetUrl: this.customerRedirectFormGroup.controls.targetUrl.value,
        isActive: this.customerRedirectFormGroup.controls.isActive.value,
      };

      this.postForm.emit(customerRedirect);
    }
  }
}
