import { Injectable } from '@angular/core';
import { SwUpdate } from '@angular/service-worker';
import { DeviceDetectorService } from 'ngx-device-detector';
import { BehaviorSubject } from 'rxjs';
import { Constants } from 'src/app/constants';
import { ToastService } from './toast.service';

@Injectable({
  providedIn: 'root',
})
export class PwaService {
  displayDownloadPrompt$ = new BehaviorSubject(false);
  deferredPrompt: any;

  constructor(private swUpdateService: SwUpdate, private deviceService: DeviceDetectorService, private toastService: ToastService) {
    this.displayDownloadPrompt().then(x => this.displayDownloadPrompt$.next(x));
  }

  savePwaInstallPromptEvent() {
    window.addEventListener('beforeinstallprompt', (e) => {
      e.preventDefault();
      this.deferredPrompt = e;
    });
  }

  listenForUpdates() {
    this.swUpdateService.available.subscribe(() => {
      this.toastService.updateApp().subscribe(() => {
        this.swUpdateService.activateUpdate().then(() => document.location.reload());
      });
    });
  }

  installPwa() {
    this.deferredPrompt?.prompt();
    this.deferredPrompt = null;
  }

  private async displayDownloadPrompt(): Promise<boolean> {
    const isDisabledByUser: boolean = !!sessionStorage.getItem(Constants.SessionKey.HideDownloadBanner);
    if (isDisabledByUser)
      return false;

    const isDesktopOrTablet: boolean = !this.deviceService.isMobile();
    if (isDesktopOrTablet)
      return false;

    const installedApps: [] = await (navigator as any).getInstalledRelatedApps();
    const isInstalled: boolean = installedApps.length > 0;
    if (isInstalled)
      return false;

    return true;
  }
}
