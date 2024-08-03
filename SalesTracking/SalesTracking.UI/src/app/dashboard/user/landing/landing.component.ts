import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { DataStateChangeEvent, GridDataResult, PagerSettings } from '@progress/kendo-angular-grid';
import { State, process } from '@progress/kendo-data-query';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { finalize } from 'rxjs';
import { BaseComponent } from 'src/shared/common/base.component';
import { DataService } from 'src/shared/services/data.service';
import { NotificationService } from 'src/shared/services/global/notification-service';
import { PaginationConfigService } from 'src/shared/services/global/pagination-config.service';

@Component({
  selector: 'app-dashboard-user-landing',
  templateUrl: './landing.component.html',
  styleUrls: ['./landing.component.scss']

})

export class UserLandingComponent extends BaseComponent {

  @BlockUI() blockUI: NgBlockUI;

  public state: State;
  public paginationConfig: PagerSettings;
  public gridData: GridDataResult;

  public users: [];

  constructor(private router: Router, private dataService: DataService
    , public override  notificationService: NotificationService
    , public paginationService: PaginationConfigService) {
    super(notificationService);
  }

  ngOnInit() { debugger
    this.intializeComponents();
    this.retrieveUsers();
  }

  intializeComponents() {
    this.paginationConfig = this.paginationService.updatePagination();
    this.state = this.paginationService.state;
  }

  public dataStateChange(state: DataStateChangeEvent): void {
    this.state = state;
    this.gridData = process(this.users, this.state);
  }

  retrieveUsers() {
    this.users = [];
    this.blockUI.start('Please Wait...');
    const url = 'userdata/GetUsers';
    this.dataService
      .getAllByGet(url)
      .pipe(finalize(() => this.blockUI.stop()))
      .subscribe((data: any) => {
        if (data != null) {
          if (!data.isError) {
            this.users = data.returnObject;
            this.gridData = process(this.users, this.state);
          }
          else { this.showErrorMessages(data.messages); }
        }
      });
  }

  navigateToRoute(isReroute: boolean) {
    if (isReroute) this.navigateToPage();
    else this.router.navigate(['./dashboard']);
  }

  navigateToPage(id = 0, isView = true) {
    if (id === 0) this.router.navigate(['./dashboard/user/create']);
    else this.router.navigate(['./dashboard/user/create', { id: id, isView: isView }]);
  }

}

