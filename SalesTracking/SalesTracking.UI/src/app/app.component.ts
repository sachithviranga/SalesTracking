import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { NavigationCancel, NavigationEnd, NavigationError, NavigationStart, Router } from '@angular/router';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { BlockUITemplateComponent } from 'src/shared/component/block-ui-template.component';
import { Configuration } from 'src/shared/utilities/configuration';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  blockTemplate: BlockUITemplateComponent;
  title = 'salestracking';
  public configFileObject: any;
  public onlineOffline: boolean = navigator.onLine;
  @BlockUI() blockUI: NgBlockUI;

  constructor(public configuration: Configuration, private http: HttpClient, private router: Router) {

    this.configFileObject = this.configuration.configFileObject;

    router.events.subscribe((val) => {
      if (val instanceof NavigationStart) {
        this.blockUI.start('Please Wait...');
      }

      if (val instanceof NavigationError) {
        this.blockUI.stop();
      }

      if (val instanceof NavigationCancel) {
        this.blockUI.stop();
      }

      if (val instanceof NavigationEnd) {
        this.blockUI.stop();
      }
    });

    window.addEventListener('online', () => {
      this.onlineOffline = true;
      this.blockUI.stop();
    });

    window.addEventListener('offline', () => {
      this.onlineOffline = false;
      this.blockUI.start('No Connection...');
    });

  }
}
