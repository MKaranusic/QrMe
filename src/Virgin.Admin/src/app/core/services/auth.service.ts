import { Inject, Injectable, OnDestroy, OnInit } from '@angular/core';
import { MsalBroadcastService, MsalGuardConfiguration, MsalService, MSAL_GUARD_CONFIG } from '@azure/msal-angular';
import { AccountInfo, AuthenticationResult, EventMessage, EventType, InteractionType, PopupRequest, RedirectRequest } from '@azure/msal-browser';
import { BehaviorSubject, Subject } from 'rxjs';
import { filter, takeUntil } from 'rxjs/operators';
import { User } from '../models/user';
import { Constants } from '../../constants';
import { Router } from '@angular/router';

@Injectable({
    providedIn: 'root',
})
export class AuthService implements OnDestroy {
    readonly destroying$ = new Subject<void>();
    readonly loggedIn$ = new BehaviorSubject<boolean>(this.hasAccount);

    get hasAccount(): boolean {
        return this.msalAuthService.instance.getAllAccounts().length > 0;
    }

    constructor(
        @Inject(MSAL_GUARD_CONFIG) private msalGuardConfig: MsalGuardConfiguration,
        private msalAuthService: MsalService,
        private msalBroadcastService: MsalBroadcastService,
        private router: Router
    ) {

        this.checkAccount();
        this.msalBroadcastService.msalSubject$
            .pipe(
                filter(
                    (msg: EventMessage) =>
                        msg.eventType === EventType.LOGIN_SUCCESS ||
                        msg.eventType === EventType.ACQUIRE_TOKEN_SUCCESS
                ),
                takeUntil(this.destroying$)
            )
            .subscribe(() => {
                this.checkAccount();
            });
    }

    login() {
        if (this.msalGuardConfig.interactionType === InteractionType.Popup) {
            if (this.msalGuardConfig.authRequest) {
                this.msalAuthService
                    .loginPopup({ ...this.msalGuardConfig.authRequest } as PopupRequest)
                    .subscribe((response: AuthenticationResult) => {
                        this.activateAccount(response.account);
                        this.redirectByRole();
                    });
            } else {
                this.msalAuthService.loginPopup().subscribe((response: AuthenticationResult) => {
                    this.activateAccount(response.account);
                    this.redirectByRole();
                });
            }
        }
        else {
            if (this.msalGuardConfig.authRequest) {
                this.msalAuthService.loginRedirect({
                    ...this.msalGuardConfig.authRequest,
                } as RedirectRequest);
            } else {
                this.msalAuthService.loginRedirect();
            }
        }
    }

    logout() {
        this.msalAuthService.logout();
        this.router.navigate([Constants.Routes.Login.Path]);
    }

    getCurrentUser(): User {
        if (this.hasAccount) {
            const tokenModel = this.msalAuthService.instance.getActiveAccount()?.idTokenClaims as any;
            return {
                oid: tokenModel?.oid,
                name: tokenModel?.name,
                givenName: tokenModel?.given_name,
                familyName: tokenModel?.family_name,
                role: tokenModel?.extension_Role
            } as User;
        }

        return {} as User;
    }

    redirectByRole() {
        if (this.getCurrentUser().role === Constants.Role.Admin) {
            this.router.navigate([Constants.Routes.Admin.Path]);
        }
        else {
            this.router.navigate([Constants.Routes.Customer.Path]);
        }
    }

    ngOnDestroy(): void {
        this.destroying$.next();
        this.destroying$.complete();
    }

    private checkAccount(): boolean {
        this.loggedIn$.next(this.hasAccount);
        return this.hasAccount;
    }

    private activateAccount(account: AccountInfo | null) {
        this.msalAuthService.instance.setActiveAccount(account);
        this.checkAccount();
    }
}
