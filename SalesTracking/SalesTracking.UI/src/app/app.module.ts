import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SharedModule } from 'src/shared/shared.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { DataService } from 'src/shared/services/data.service';
import { Configuration } from 'src/shared/utilities/configuration';
import { RequestInterceptor } from 'src/shared/services/interceptors/RequestInterceptor';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { StorageHandlerService } from 'src/shared/services/global/StorageHandlerService';
import { BlockUIModule } from 'ng-block-ui';
import { BlockUITemplateComponent } from 'src/shared/component/block-ui-template.component';
import { NgxPermissionsModule } from 'ngx-permissions';
import { AuthTokenHandlerService } from 'src/shared/services/global/authenciation-token-handler';
import { PaginationConfigService } from 'src/shared/services/global/pagination-config.service';
import { LogoutHandlerService } from 'src/shared/services/global/logout-handler.service';
import { HashLocationStrategy, LocationStrategy } from '@angular/common';
import { ResponseInterceptor } from 'src/shared/services/interceptors/response-interceptor';

@NgModule({
  declarations: [
    AppComponent,
    BlockUITemplateComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    BlockUIModule.forRoot({ template: BlockUITemplateComponent }),
    NgxPermissionsModule.forRoot(),
    SharedModule,
    AppRoutingModule
  ],
  providers: [Configuration, DataService,
    StorageHandlerService, AuthTokenHandlerService,
    LogoutHandlerService,
    PaginationConfigService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: RequestInterceptor,
      multi: true
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: ResponseInterceptor,
      multi: true
    },
    {provide: LocationStrategy, useClass: HashLocationStrategy}],
  bootstrap: [AppComponent]
})
export class AppModule { }
