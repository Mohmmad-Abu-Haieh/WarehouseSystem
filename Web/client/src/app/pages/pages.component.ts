import { Component, OnDestroy, OnInit, ViewChild, AfterViewInit } from '@angular/core';
import { ActivatedRoute, NavigationEnd, Router } from '@angular/router';
import { AuthService } from '../_guards/auth.service';
import { PagesService } from './pages.service';
import { Subscription } from 'rxjs';
import { filter, map } from 'rxjs/operators'

export let browserRefresh = false;
declare var $: any;

var thisComponent;
@Component({
    selector: 'app-pages',
    templateUrl: './pages.component.html',
    styleUrls: ['./pages.component.scss'],

})

export class PagesComponent implements OnInit {
    sidebarExpanded = false;
    routeLogin: any;
    isWebPlatForm: boolean = false;
    mobileView: boolean = false;
    routed: any;
    subscription: Subscription | undefined;
    constructor(public pagesService: PagesService
        , public authService: AuthService,
        private router: Router,
        public route: ActivatedRoute,
        
    ) {
        // this.router.events
        //     .pipe(filter((rs): rs is NavigationEnd => rs instanceof NavigationEnd))
        //     .subscribe(event => {
        //         if (
        //             event.id === 1 &&
        //             event.url === event.urlAfterRedirects
        //         ) {
        //             browserRefresh = true;
        //         }
        //     });
        // thisComponent = this;
    }

    ngOnInit() {
 



}



}