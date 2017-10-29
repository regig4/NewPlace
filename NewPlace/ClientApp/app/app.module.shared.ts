import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { FetchDataComponent } from './components/fetchdata/fetchdata.component';
import { CounterComponent } from './components/counter/counter.component';
import { AdvertisementsComponent } from './components/advertisements/advertisements.component';
import { DetailsComponent } from "./components/details/details.component";
import { AdvertisementsService } from "./components/advertisements/advertisements.service";
@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        CounterComponent,
        FetchDataComponent,
        HomeComponent,
        AdvertisementsComponent,
        DetailsComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'counter', component: CounterComponent },
            { path: 'fetch-data', component: FetchDataComponent },
            { path: 'advertisements', component: AdvertisementsComponent },
            { path: 'details/:id', component: DetailsComponent },
            { path: '**', redirectTo: 'home' }
        ])
    ],
    providers: [AdvertisementsService]
})
export class AppModuleShared {
}
