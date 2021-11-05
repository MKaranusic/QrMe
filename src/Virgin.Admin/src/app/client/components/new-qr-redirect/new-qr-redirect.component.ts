import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Constants } from 'src/app/constants';
import { CustomerRedirect } from '../../models/customer-redirect';
import { QrRedirectService } from '../../services/qr-redirect.service';

@Component({
  selector: 'app-new-qr-redirect',
  templateUrl: './new-qr-redirect.component.html',
  styleUrls: ['./new-qr-redirect.component.scss']
})
export class NewQrRedirectComponent implements OnInit {

  constructor(private qrRedirectService: QrRedirectService, private router: Router) { }

  ngOnInit() {
  }

  postForm(customerRedirect: CustomerRedirect) {
    //TODO: dodat nekakav loader dok se ceka response od servera
    this.qrRedirectService.createQrRedirect(customerRedirect).subscribe(x => this.router.navigate([Constants.Routes.Customer.Path]));
  }

  onBack(): void {
    this.router.navigate([Constants.Routes.Customer.Path]);
  }
}
