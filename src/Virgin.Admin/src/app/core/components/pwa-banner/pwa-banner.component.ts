import { Component, OnInit } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Constants } from 'src/app/constants';
import { PwaService } from '../../services/pwa.service';

@Component({
  selector: 'app-pwa-banner',
  templateUrl: './pwa-banner.component.html',
  styleUrls: ['./pwa-banner.component.scss']
})
export class PwaBannerComponent implements OnInit {

  showBanner$!: BehaviorSubject<boolean>;

  constructor(private pwaService: PwaService) { }

  ngOnInit(): void {
    this.showBanner$ = this.pwaService.displayDownloadPrompt$;
  }

  installPwa() {
    this.showBanner$.next(false);
    this.pwaService.installPwa();
  }

  hideDownloadBanner() {
    this.showBanner$.next(false);
    sessionStorage.setItem(Constants.SessionKey.HideDownloadBanner, 'true');
  }
}
