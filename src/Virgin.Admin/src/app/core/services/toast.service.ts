import { Injectable } from '@angular/core';
import { IndividualConfig, ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { take } from 'rxjs/operators';
import { Messages } from 'src/app/messages';

@Injectable({
  providedIn: 'root',
})
export class ToastService {
  updateOptions: Partial<IndividualConfig> = {
    disableTimeOut: true,
    positionClass: 'toast-bottom-center',
  };

  constructor(private toasterService: ToastrService) { }

  updateApp(): Observable<void> {
    return this.toasterService.info(Messages.Toast.Update.Message, Messages.Toast.Update.Title, this.updateOptions).onTap.pipe(take(1));
  }

  unauthorizedAccess(): Observable<void> {
    return this.toasterService.error(Messages.Toast.UnauthorizedAccess.Message, Messages.Toast.UnauthorizedAccess.Title, { positionClass: 'toast-bottom-center' }).onTap.pipe(take(1));
  }

  loginToAccessThisArea(): Observable<void> {
    return this.toasterService.warning(Messages.Toast.LoginToAccessThisArea.Message, Messages.Toast.LoginToAccessThisArea.Title, { positionClass: 'toast-bottom-center' }).onTap.pipe(take(1));
  }
}
