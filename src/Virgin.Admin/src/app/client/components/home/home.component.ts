import { Component, OnDestroy, OnInit } from '@angular/core';
import { QrRedirectService } from '../../services/qr-redirect.service';
import { Constants } from 'src/app/constants';
import { CustomerRedirect } from '../../models/customer-redirect';
import { BehaviorSubject } from 'rxjs';
import { AuthService } from 'src/app/core/services/auth.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit, OnDestroy {
  routes = Constants.Routes;
  customerQrViewedSum!: number;
  noRedirectLinks = false;

  customerRedirects$ = new BehaviorSubject<CustomerRedirect[]>([]);
  activeCustomerRedirect$ = new BehaviorSubject<any>(null);

  constructor(private qrRedirectService: QrRedirectService, private authService: AuthService) { }

  ngOnDestroy(): void {
    this.customerRedirects$.complete();
    this.activeCustomerRedirect$.complete();
  }

  ngOnInit() {
    this.initPageData();
  }

  onLogout() {
    this.authService.logout();
  }

  updateUI(redirectId: number) {
    this.noRedirectLinks = false;

    let newActive = this.customerRedirects$.value.find(x => x.id == redirectId) as CustomerRedirect;
    let uncativatedList = this.customerRedirects$.value.filter(x => x.id != redirectId);

    const oldActive = this.activeCustomerRedirect$.value
    if (oldActive)
      uncativatedList.unshift(oldActive);

    newActive.isActive = true;
    this.activeCustomerRedirect$.next(newActive);
    this.customerRedirects$.next(uncativatedList);
  }

  private initPageData() {
    this.qrRedirectService.getCustomerTimesViewedSum().subscribe(x => this.customerQrViewedSum = x);

    this.qrRedirectService.getQrRedirects().subscribe((data: CustomerRedirect[]) => {
      this.noRedirectLinks = data.length == 0;

      const notActiveRedirects = data.filter(x => x.isActive == false);
      this.customerRedirects$.next(notActiveRedirects);

      let activeRedirect = data.find(x => x.isActive == true) as CustomerRedirect;
      this.activeCustomerRedirect$.next(activeRedirect);
    });
  }
}
