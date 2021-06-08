import { Component, OnInit } from '@angular/core';
import { HumanConnect } from 'humanapi-connect-client';
import { TokenService, LogService, UtilsService } from 'abp-ng2-module';
import { AppAuthService } from '@shared/auth/app-auth.service';
import { AppConsts } from '@shared/AppConsts';
import { HumanApiDataServiceProxy } from '@shared/service-proxies/service-proxies';

@Component({
  templateUrl: './humanapi.component.html'
})

export class ConnectComponent implements OnInit {
  token = '';
  connectClosed = false;
  constructor(
    public authService: AppAuthService,
    private _utilsService: UtilsService,
    private _humanApiService: HumanApiDataServiceProxy
  ) {
  }

  syncData() {
    this._humanApiService.syncData(undefined);
  }


  fetchToken() {
      if (this._utilsService.getCookieValue(AppConsts.authorization.humanApiSessionTokenName)) {
          this.token = this._utilsService.getCookieValue(AppConsts.authorization.humanApiSessionTokenName);
      } else {
          this.authService.getHumanApiSession();
          this.token = this._utilsService.getCookieValue(AppConsts.authorization.humanApiSessionTokenName);
      }
    }

  ngOnInit() {
    //  const { HumanConnect } = window;
    if ( ! this.token ) {
        this.fetchToken();
    }
    const event = document.createEvent('Event');
    event.initEvent('load', true, true);
    window.dispatchEvent(event);
    if (HumanConnect) {
      HumanConnect.on('connect', response => {
        console.log('connect', response);
      });
      HumanConnect.on('disconnect', response => {
        console.log('disconnect', response);

      });
      HumanConnect.on('close', response => {
        console.log('connect', response);
      });
    }
  }
}
