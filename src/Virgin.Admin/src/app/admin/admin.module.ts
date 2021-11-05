import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CreateCustomerComponent } from './components/create-customer/create-customer.component';
import { CreateCustomerFormComponent } from './components/create-customer/create-customer-form/create-customer-form.component';
import { NavigationComponent } from './components/navigation/navigation.component';
import { AdminComponent } from './admin.component';
import { RouterModule, Routes } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';
import { CoreModule } from '../core/core.module';
import { Constants } from 'src/app/constants';

const route: Routes = [
  {
    path: '', component: AdminComponent, children: [
      { path: Constants.Routes.Admin.Children.CreateCustomer.Path, component: CreateCustomerComponent },
      { path: '**', redirectTo: Constants.Routes.Admin.Children.CreateCustomer.Path }
    ]
  }
];

@NgModule({
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RouterModule.forChild(route),
    CoreModule
  ],
  declarations: [
    AdminComponent,
    CreateCustomerComponent,
    CreateCustomerFormComponent,
    NavigationComponent
  ],
  exports: [],
})
export class AdminModule { }
