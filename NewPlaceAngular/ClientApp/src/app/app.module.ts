import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';

import { CatalogComponent } from './catalog/catalog.component';
import { SearchComponent } from './shared/components/search/search.component';
import { RecommendationsComponent } from './shared/components/recommendations/recommendations.component';
import { DetailsComponent } from './details/details.component';
import { AdvertisementFormComponent } from './shared/components/advertisement-form/advertisement-form.component';

import { AdvertisementsService } from './shared/services/advertisements.service';
import { PaymentService } from './shared/services/payment.service';

import { MatSelectModule } from '@angular/material/select';
import { MatStepperModule } from '@angular/material/stepper';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatRadioModule } from '@angular/material/radio';
import { MatSliderModule } from '@angular/material/slider';


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    CatalogComponent,
    DetailsComponent,
    AdvertisementFormComponent,
    SearchComponent
  ],
  imports: [
    MatSelectModule,
    MatStepperModule,
    MatRadioModule,
    MatSliderModule,
    BrowserAnimationsModule,
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: 'home', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'recommend', component: RecommendationsComponent },
      { path: 'new', component: AdvertisementFormComponent },
      { path: '', component: CatalogComponent },
    ])
  ],
  providers: [
    AdvertisementsService,
    PaymentService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
