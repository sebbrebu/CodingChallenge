import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { CountryListComponent } from './country-list/country-list.component';
import { CountryDetailsComponent } from './country-details/country-details.component';
import { BrowserModule } from '@angular/platform-browser';
import {NgxPaginationModule} from 'ngx-pagination';

const routes: Routes = [{ path: '', component: CountryListComponent },
                        { path: 'countries/:name', component: CountryDetailsComponent }];

@NgModule({
  imports: [
    BrowserModule, 
    NgxPaginationModule,
    RouterModule.forRoot(routes)],
  declarations: [
    CountryListComponent,
    CountryDetailsComponent
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }