import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AboutUsComponent } from './about-us/about-us.component';
import { ContactComponent } from './contact/contact.component';
import { HelpRoutingModule } from './help-routing.module';
import { SharedModule } from '../shared/shared.module';



@NgModule({
  declarations: [
    AboutUsComponent,
    ContactComponent
  ],
  imports: [
    CommonModule,
    HelpRoutingModule,
    SharedModule
  ]
})
export class HelpModule { }
