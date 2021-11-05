import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { CustomerRedirect } from 'src/app/client/models/customer-redirect';
import { Router } from '@angular/router';
import { DataTransferHelperService } from 'src/app/client/services/helpers/data-transfer-helper.service';
import { Constants } from 'src/app/constants';
import { QrRedirectService } from 'src/app/client/services/qr-redirect.service';

export const DIRECTIONS = {
  LEFT: "L",
  RIGHT: "R"
}

@Component({
  selector: 'app-qr-redirect-list-item',
  templateUrl: './qr-redirect-list-item.component.html',
  styleUrls: ['./qr-redirect-list-item.component.scss']
})
export class QrRedirectListItemComponent implements OnInit {
  @Input() customerRedirect!: CustomerRedirect | null;
  @Input() isActive: boolean = false;
  @Output() activeChange = new EventEmitter<number>();

  eventActivationTresholdInPx: number = 200;
  animationActivationTresholdInPx: number = 50;

  animation = {
    direction: '',
    inProgress: false,
    animationClass: ''
  }

  constructor(private router: Router, private dataTransferService: DataTransferHelperService, private qrRedirectService: QrRedirectService) { }

  ngOnInit() {
  }

  callEdit() {
    this.dataTransferService.setEditCustomerRedirect(this.customerRedirect as CustomerRedirect);
    this.router.navigate([Constants.Routes.Customer.Children.EditQrRedirect.RouterNavigateLink]);
  }

  setActive() {
    this.qrRedirectService.updateQrRedirect({ ...this.customerRedirect, isActive: true } as CustomerRedirect).subscribe();
    this.activeChange.emit(this.customerRedirect?.id);
  }

  numberOfDigits(): string {
    switch (this.customerRedirect?.timesViewed?.toString().length) {
      case 1:
        return 'oneDigit'
      case 2:
        return 'twoDigits'
      case 3:
        return 'threeDigits'
      default:
        return 'fourDigits'
    }
  }

  panLeft(e: any) {
    if (e.distance >= this.animationActivationTresholdInPx && !this.animation.inProgress) {
      this.animation = {
        inProgress: true,
        direction: DIRECTIONS.LEFT,
        animationClass: "animation-left"
      }
    }
  }

  panRight(e: any) {
    if (e.distance >= this.animationActivationTresholdInPx && !this.animation.inProgress) {
      this.animation = {
        inProgress: true,
        direction: DIRECTIONS.RIGHT,
        animationClass: "animation-right"
      }
    }
  }

  panEnd(e: any) {

    if (e.distance < this.eventActivationTresholdInPx) {
      this.animation = {
        inProgress: false,
        direction: '',
        animationClass: ''
      }
    }
    else {
      if (this.animation.direction == DIRECTIONS.RIGHT) {
        this.callEdit();
      }
      else if (this.animation.direction == DIRECTIONS.LEFT && !this.isActive) {
        this.setActive();
      }
    }
  }
}
