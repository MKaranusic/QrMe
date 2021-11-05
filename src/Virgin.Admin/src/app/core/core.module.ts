import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ToastrModule } from 'ngx-toastr';
import { PwaBannerComponent } from './components/pwa-banner/pwa-banner.component';

@NgModule({
  declarations: [
    PwaBannerComponent
  ],
  imports: [
    CommonModule,
    ToastrModule.forRoot(),
  ],
  exports: [
    PwaBannerComponent
  ]
})
export class CoreModule { }
