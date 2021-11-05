import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { Constants } from 'src/app/constants';
import { AuthService } from '../services/auth.service';
import { ToastService } from '../services/toast.service';

@Injectable({
    providedIn: 'root',
})
export class AdminGuard implements CanActivate {

    constructor(private router: Router, private authService: AuthService, private toastService: ToastService) { }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        if (this.authService.getCurrentUser().role === Constants.Role.Admin)
            return true;
        else if (this.authService.getCurrentUser().role === Constants.Role.Customer) {
            this.toastService.unauthorizedAccess();
            this.router.navigate([Constants.Routes.Customer.Path]);
            return false;
        }

        this.toastService.loginToAccessThisArea();
        this.router.navigate([Constants.Routes.Login.Path]);
        return false;
    }
}
