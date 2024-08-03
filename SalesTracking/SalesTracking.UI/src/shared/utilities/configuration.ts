import { Injectable } from '@angular/core';
import * as config from '../../assets/configurations/config.json';
import * as  AppSettingsDev from '../../settings/appsettings.Development.json';
import * as  AppSettingProduction from '../../settings/appsettings.Prod.json';
import { environment } from '../../environments/environment';

export function configuration(env: any) {
    let configObject;
    if (env === 'production') { configObject = AppSettingProduction; }
    else { configObject = AppSettingsDev; }
    return configObject;
}

@Injectable()
export class Configuration {

    private appSettings: any;
    public configFileObject: any;
    public languageFileObject: any;

    public baseServerURL: any;
    public restServerURL: any;
    public authServerURL: any;
    public authServerAPIURL: any;

    public loginMethod: any;



    public idleTimeInSeconds: any;
    public idleTimeOutInSeconds: any;
    public idlePingTimeInSecons: any;

    constructor() {
        this.configFileObject = config;
        //this.languageFileObject = en;
        this.appSettings = configuration(environment.mode);

        this.baseServerURL = this.appSettings.default.Configuration.Server;

        this.idleTimeInSeconds = this.configFileObject.default.IdleHandling.IdleTimeInSeconds;
        this.idleTimeOutInSeconds = this.configFileObject.default.IdleHandling.TimeOutTimeInSeconds;
        this.idlePingTimeInSecons = this.configFileObject.default.IdleHandling.PingTimeInSeconds;
    }

}
