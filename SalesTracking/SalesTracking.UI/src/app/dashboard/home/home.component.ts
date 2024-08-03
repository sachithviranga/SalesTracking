import { Component } from '@angular/core';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { finalize } from 'rxjs';
import { BaseComponent } from 'src/shared/common/base.component';
import { DataService } from 'src/shared/services/data.service';
import { NotificationService } from 'src/shared/services/global/notification-service';

@Component({
  selector: 'app-dashboard-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent extends BaseComponent {

  cols: number;
  chartCols: number;

  gridByBreakpoint = {
    xl: 4,
    lg: 4,
    md: 2,
    sm: 2,
    xs: 1
  }
  chartGridByBreakpoint = {
    xl: 5,
    lg: 5,
    md: 3,
    sm: 3,
    xs: 3
  }

  public cards = [];
  public KPICards = [
    {
      title: 'ORDERS',
      query: { measures: ['Orders.count'] },
      difference: 'Orders',
      progress: false,
      duration: 2.25,
    },
    {
      title: 'TOTAL USERS',
      query: { measures: ['Users.count'] },
      difference: 'Users',
      progress: false,
      duration: 2.5,
    },
    {
      title: 'COMPLETED ORDERS',
      query: { measures: ['Orders.percentOfCompletedOrders'] },
      difference: 'false',
      progress: true,
      duration: 2.75,
    },
    {
      title: 'TOTAL PROFIT',
      query: { measures: ['LineItems.price'] },
      difference: 'false',
      progress: false,
      duration: 3.25,
    },
  ];


  @BlockUI() blockUI: NgBlockUI;
  product = { title: 'Current Stock', data: [] };

  productChartLabels: any[] = [];
  productChartData: any[] = [];

  constructor(private dataService: DataService
    , public override  notificationService: NotificationService) {
    super(notificationService);
    this.cols = this.KPICards.length;
    this.retrieveAvalableProdcuts();
  }

  retrieveAvalableProdcuts() {
    this.product.data = [];
    this.blockUI.start('Please Wait...');
    const url = 'dashboard/GetAvailableProdcuts';
    this.dataService
      .getAllByGet(url)
      .pipe(finalize(() => this.blockUI.stop()))
      .subscribe((data: any) => {
        if (data != null) {
          if (!data.isError) {
            const returnData = [...data.returnObject];
            this.productChartLabels = returnData.map((a: any) =>  a.productName );
            this.productChartData = [{
              label: 'Prodcuts',
              data: returnData.map((r: any) => r.qty),
              backgroundColor: '#6200EE',
              fill: false,
            }];
          }
          else { this.showErrorMessages(data.messages); }
        }
      });
  }

}