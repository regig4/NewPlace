import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { AdvertisementsComponent } from './components/catalog/catalog.component';
import { DetailsComponent } from "./components/details/details.component";
import { AdvertisementsService } from "./shared/services/advertisements.service";

import { SearchComponent } from "./shared/components/search/search.component";

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        HomeComponent,
        AdvertisementsComponent,
        DetailsComponent,
        SearchComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'advertisements', component: AdvertisementsComponent },
            { path: 'details/:id', component: DetailsComponent },
            { path: '**', redirectTo: 'home' }
        ])
    ],
    providers: [AdvertisementsService]
})
export class AppModuleShared {
}
