import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminGuard } from './core/guards/admin-guard';
import { CustomerGuard } from './core/guards/customer-guard';
import { LoginComponent } from './core/components/login/login.component';
import { Constants } from 'src/app/constants';
import { NotSetLinkComponent } from './core/components/not-set-link/not-set-link.component';

const routes: Routes = [
  {
    path: Constants.Routes.Login.Path,
    component: LoginComponent
  },
  {
    path: Constants.Routes.NotSetLinkComponent.Path,
    component: NotSetLinkComponent
  },
  {
    path: Constants.Routes.Admin.Path,
    loadChildren: async () =>
      (await import('./admin/admin.module')).AdminModule,
    canActivate: [AdminGuard]
  },
  {
    path: Constants.Routes.Customer.Path,
    loadChildren: async () =>
      (await import('./client/client.module')).ClientModule,
    canActivate: [CustomerGuard]
  },
  { path: '**', redirectTo: Constants.Routes.Login.Path, pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule { }
