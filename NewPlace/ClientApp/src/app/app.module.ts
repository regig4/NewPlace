/// <reference path="shared/components/search/search.component.ts" />
/// <reference path="shared/components/search/search.component.ts" />
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { ApiAuthorizationModule } from 'src/api-authorization/api-authorization.module';
import { AuthorizeGuard } from 'src/api-authorization/authorize.guard';
import { AuthorizeInterceptor } from 'src/api-authorization/authorize.interceptor';

import { CatalogComponent } from './catalog/catalog.component';
import { DetailsComponent } from './details/details.component';
import { SearchComponent } from './shared/components/search/search.component';
import { RecomendationService } from './shared/services/recomendations.service';
import { AdvertisementsService } from './shared/services/advertisements.service';

import { HttpModule } from '@angular/http'; // TODO: delete this
import { AdvertisementFormComponent } from './shared/components/advertisement-form/advertisement-form.component';

import { HttpLogInterceptor } from './http.interceptor';
import { SafeHtmlPipe } from './shared/pipes/safe-html.pipe';

import { MatSelectModule } from '@angular/material/select';
import { MatStepperModule } from '@angular/material/stepper';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatRadioModule } from '@angular/material/radio';
import { MatSliderModule } from '@angular/material/slider';

import { AgmCoreModule } from '@agm/core';
import { RecommendationsComponent } from './recommendations/recommendations.component';


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    CatalogComponent,
    DetailsComponent,
    RecommendationsComponent,
    SearchComponent,
    AdvertisementFormComponent,
    SafeHtmlPipe
  ],
  imports: [
    MatSelectModule,
    MatStepperModule,
    MatRadioModule,
    MatSliderModule,
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    HttpModule,
    FormsModule,
    ApiAuthorizationModule,
    RouterModule.forRoot([
      { path: 'home', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: '', component: CatalogComponent },
      { path: 'new', component: AdvertisementFormComponent },
      { path: 'recommend', component: RecommendationsComponent },
      { path: 'details/:id', component: DetailsComponent },
      { path: 'fetch-data', component: FetchDataComponent, canActivate: [AuthorizeGuard] },
    ]),
    AgmCoreModule.forRoot({
      apiKey: 'AIzaSyDUT4MrztopHqglMDoCx0qi8MbWapM0u6k'
      /* apiKey is required, unless you are a
      premium customer, in which case you can
      use clientId
      */
    }),
    BrowserAnimationsModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: HttpLogInterceptor, multi: true },
    AdvertisementsService,
    RecomendationService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
