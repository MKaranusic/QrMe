import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Constants } from 'src/app/constants';
import { CustomerRedirect } from '../../models/customer-redirect';
import { DataTransferHelperService } from '../../services/helpers/data-transfer-helper.service';
import { QrRedirectService } from '../../services/qr-redirect.service';

@Component({
  selector: 'app-edit-qr-redirect',
  templateUrl: './edit-qr-redirect.component.html',
  styleUrls: ['./edit-qr-redirect.component.scss']
})
export class EditQrRedirectComponent implements OnInit {

  customerRedirectToEdit: CustomerRedirect;

  constructor(dataTransferService: DataTransferHelperService, private qrRedirectService: QrRedirectService, private router: Router) {
    this.customerRedirectToEdit = dataTransferService.getEditCustomerRedirect();
  }

  ngOnInit() {
  }

  postForm(customerRedirect: CustomerRedirect) {
    //TODO: dodat nekakav loader dok se ceka response od servera
    this.qrRedirectService.updateQrRedirect(customerRedirect).subscribe(x => this.router.navigate([Constants.Routes.Customer.Path]));
  }

  onBack(): void {
    this.router.navigate([Constants.Routes.Customer.Path]);
  }
}
