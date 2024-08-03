import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { GridDataResult, PagerSettings ,DataStateChangeEvent} from '@progress/kendo-angular-grid';
import { State , process } from '@progress/kendo-data-query';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { BaseComponent } from 'src/shared/common/base.component';
import { DataService } from 'src/shared/services/data.service';
import { NotificationService } from 'src/shared/services/global/notification-service';
import { PaginationConfigService } from 'src/shared/services/global/pagination-config.service';
import { finalize } from 'rxjs';

@Component({
    selector: 'app-dashboard-sales-landing',
    templateUrl: './landing.component.html',
    styleUrls: ['./landing.component.scss']
  })

  export class SalesLandingComponent extends BaseComponent{
    @BlockUI() blockUI: NgBlockUI;
    public state: State;
    public paginationConfig: PagerSettings;
    public gridData: GridDataResult;
    public sales :[];

    constructor(private router: Router, private dataService: DataService
      , public override  notificationService: NotificationService
      , public paginationService: PaginationConfigService) {
      super(notificationService);
    }

    ngOnInit(){
      this.intializeComponents();
      this.retrieveSales();
    }

    
  navigateToRoute(isReroute: boolean) {
    if (isReroute) this.navigateToPage();
    else this.router.navigate(['./dashboard']);
  }

  navigateToPage(id = 0, isView = true) {
    if (id === 0) this.router.navigate(['./dashboard/sales/create']);
    else this.router.navigate(['./dashboard/sales/create', { id: id, isView: isView }]);
  }

  intializeComponents(){
    this.paginationConfig = this.paginationService.updatePagination();
    this.state = this.paginationService.state;

  }

  retrieveSales(){
    this.sales = [];
    this.blockUI.start('Please Wait...');
    const url = 'sales/getsales';
    this.dataService
      .getAllByGet(url)
      .pipe(finalize(() => this.blockUI.stop()))
      .subscribe((data: any) => {
        if (data != null) {
          if (!data.isError) {
            this.sales = data.returnObject;
            this.gridData = process(this.sales, this.state);
          }
          else { this.showErrorMessages(data.messages); }
        }
      });

  }

  public dataStateChange(state: DataStateChangeEvent): void {
    this.state = state;
    this.gridData = process(this.sales, this.state);
  }

  

  }
  