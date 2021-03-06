import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { finalize } from 'rxjs/operators';
import { TokenService, LogService, UtilsService } from 'abp-ng2-module';
import { AppConsts } from '@shared/AppConsts';
import { UrlHelper } from '@shared/helpers/UrlHelper';
import { HumanApiSessionResultModel } from '../service-proxies/service-proxies';
import {
    AuthenticateModel,
    AuthenticateResultModel,
    TokenAuthServiceProxy,
} from '@shared/service-proxies/service-proxies';

@Injectable()
export class AppAuthService {
    authenticateModel: AuthenticateModel;
    authenticateResult: AuthenticateResultModel;
    rememberMe: boolean;
    humanApiSessionResult: HumanApiSessionResultModel;

    constructor(
        private _tokenAuthService: TokenAuthServiceProxy,
        private _router: Router,
        private _utilsService: UtilsService,
        private _tokenService: TokenService,
        private _logService: LogService
    ) {
        this.clear();
    }

    getHumanApiSession(): void {
        this._tokenAuthService
            .getHumanApiSessionToken()
            .subscribe((result: HumanApiSessionResultModel) => {
                this.processHumanApiSessionResult(result);
            });
    }

    logout(reload?: boolean): void {
        abp.auth.clearToken();
        abp.utils.setCookieValue(
            AppConsts.authorization.encryptedAuthTokenName,
            undefined,
            undefined,
            abp.appPath
        );
        if (reload !== false) {
            location.href = AppConsts.appBaseUrl;
        }
    }

    authenticate(finallyCallback?: () => void): void {
        finallyCallback = finallyCallback || (() => { });

        this._tokenAuthService
            .authenticate(this.authenticateModel)
            .pipe(
                finalize(() => {
                    finallyCallback();
                })
            )
            .subscribe((result: AuthenticateResultModel) => {
                this.processAuthenticateResult(result);
            });
    }

    private processAuthenticateResult(
        authenticateResult: AuthenticateResultModel
    ) {
        this.authenticateResult = authenticateResult;

        if (authenticateResult.accessToken) {
            // Successfully logged in
            this.login(
                authenticateResult.accessToken,
                authenticateResult.encryptedAccessToken,
                authenticateResult.expireInSeconds,
                this.rememberMe
            );
        } else {
            // Unexpected result!

            this._logService.warn('Unexpected authenticateResult!');
            this._router.navigate(['account/login']);
        }
    }

    private login(
        accessToken: string,
        encryptedAccessToken: string,
        expireInSeconds: number,
        rememberMe?: boolean
    ): void {
        const tokenExpireDate = rememberMe
            ? new Date(new Date().getTime() + 1000 * expireInSeconds)
            : undefined;

        this._tokenService.setToken(accessToken, tokenExpireDate);

        this._utilsService.setCookieValue(
            AppConsts.authorization.encryptedAuthTokenName,
            encryptedAccessToken,
            tokenExpireDate,
            abp.appPath
        );

        let initialUrl = UrlHelper.initialUrl;
        if (initialUrl.indexOf('/login') > 0) {
            initialUrl = AppConsts.appBaseUrl;
        }

        location.href = initialUrl;
    }

    private clear(): void {
        this.authenticateModel = new AuthenticateModel();
        this.authenticateModel.rememberClient = false;
        this.authenticateResult = null;
        this.rememberMe = false;
        this.humanApiSessionResult = null;
    }

    private processHumanApiSessionResult(
        humanApiSessionResult: HumanApiSessionResultModel
    ) {
        this.humanApiSessionResult = humanApiSessionResult;

        if (humanApiSessionResult.sessionToken) {
            // Successfully retrieved session
            const tokenExpireDate = new Date(new Date().getTime() + 1000 * humanApiSessionResult.expireInSeconds);

        this._utilsService.setCookieValue(
            AppConsts.authorization.humanApiSessionTokenName,
            humanApiSessionResult.sessionToken,
            tokenExpireDate,
            abp.appPath
        );
        } else {
            // Unexpected result!

            this._logService.warn('Unexpected HumanApiSessionResultModel!');
        }
    }
}
