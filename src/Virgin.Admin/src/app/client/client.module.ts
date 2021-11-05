import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { ClientComponent } from './client.component';
import { Constants } from 'src/app/constants';
import { CoreModule } from '../core/core.module';
import { HomeComponent } from './components/home/home.component';
import { NewQrRedirectComponent } from './components/new-qr-redirect/new-qr-redirect.component';
import { EditQrRedirectComponent } from './components/edit-qr-redirect/edit-qr-redirect.component';
import { QrRedirectListItemComponent } from './components/home/components/qr-redirect-list-item/qr-redirect-list-item.component';
import { ReactiveFormsModule } from '@angular/forms';
import { CustomerRedirectFormComponent } from './components/forms/customer-redirect-form/customer-redirect-form.component';
import { ShortenStringPipe } from './pipes/shorten-string.pipe';

const route: Routes = [
  {
    path: '', component: ClientComponent, children: [
      { path: Constants.Routes.Customer.Children.Home.Path, component: HomeComponent },
      { path: Constants.Routes.Customer.Children.NewQrRedirect.Path, component: NewQrRedirectComponent },
      { path: Constants.Routes.Customer.Children.EditQrRedirect.Path, component: EditQrRedirectComponent },
      { path: '**', redirectTo: Constants.Routes.Customer.Children.Home.Path }
    ]
  },
];

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(route),
    CoreModule,
    ReactiveFormsModule
  ],
  declarations: [
    ClientComponent,
    HomeComponent,
    NewQrRedirectComponent,
    EditQrRedirectComponent,
    QrRedirectListItemComponent,
    CustomerRedirectFormComponent,
    ShortenStringPipe
  ],
  exports: [ClientComponent]
})
export class ClientModule { }
