import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import Validation from 'src/shared/common/valdators/confirmed.validator';
import { RegX } from 'src/shared/utilities/regex.common';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { DataService } from 'src/shared/services/data.service';
import { NotificationService } from 'src/shared/services/global/notification-service';
import { BaseComponent } from 'src/shared/common/base.component';
import { finalize } from 'rxjs';

@Component({
  selector: 'app-dashboard-customer-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.scss']
})
export class CustomerCreateComponent extends BaseComponent {
  @BlockUI() blockUI: NgBlockUI;
  public form = new FormGroup({
    id : new FormControl(0),
    name: new FormControl('', [Validators.required]),
    address: new FormControl(''),
    phoneNo: new FormControl('', [Validators.required]),
    isActive: new FormControl(true),
    customerTypeId: new FormControl([], [Validators.required]),
    customerTypeName:new FormControl()
  });
  id = 0;
  isView = false;
  customerTypes: any = [];
  customer: any = {};

  constructor(private router: Router,
    private dataService: DataService,
    public override notificationService: NotificationService,
    private route: ActivatedRoute) {
    super(notificationService);
    this.retriveCustomerTypes();
  }

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.id = params['id'] ? params['id'] : 0;
      if (params['id'])
        this.isView = params['isView'] ? params['isView'] === 'false' ? false : true : true;
    });
    this.retriveCustomer();
  }


  retriveCustomer() {
    if (this.id > 0) {
      this.customer = {};
      this.blockUI.start('Please Wait...');
      const url = 'customerData/getCustomerById';
      const params = { 'id': this.id };
      this.dataService
        .getAllByGet(url, params)
        .pipe(finalize(() => this.blockUI.stop()))
        .subscribe((data: any) => {
          if (data != null) {
            if (!data.isError) { this.mapRetriveData(data.returnObject); }
            else { this.showErrorMessages(data.messages); }
          }
        });
      if (this.isView) this.form.disable();
      else this.form.enable();
    }

  }

  mapRetriveData(data: any) { debugger
    this.customer = { ...data };
    this.form.patchValue(this.customer);
    this.form.updateValueAndValidity();
  }

  retriveCustomerTypes() {
    this.customerTypes = [];
    this.blockUI.start('Please Wait...');
    const url = 'masterdata/getcustomertypes';
    this.dataService
      .getAllByGet(url)
      .pipe(finalize(() => this.blockUI.stop()))
      .subscribe((data: any) => {
        if (data != null) {
          if (!data.isError) { this.customerTypes = data.returnObject; }
          else { this.showErrorMessages(data.messages); }
        }
      });
  }


  navigateToRoute(isReroute: boolean) {
    if (isReroute)
      this.router.navigate(['./dashboard/customer/create']);
    else
      this.router.navigate(['./dashboard/customer']);
  }

  save() {
    const url = 'customerdata/' + ((this.id === 0) ? 'addcustomer' : 'updatecustomer');
    if (this.form.valid) {
      this.blockUI.start('Please Wait...');
      this.dataService
        .getAllByPost(url,this.mapSaveData())
        .pipe(finalize(() => this.blockUI.stop()))
        .subscribe((data: any) => { this.handleResponse(data) });
    }
    else {
      this.notificationService.showWarningMsg('Please fill required fields.');
      this.formValidation(this.form);
    }
  }

  mapSaveData() {
    const data = this.form.getRawValue();
    //const userRole = data.userRole?.map(a => { return { roleId: a, userId: this.id }; });
    return { ...data };
  }

  handleResponse(data: any) {
    if (data != null) {
      if (!data.isError) {
        const message = (this.id === 0) ? 'Saved Succussfully.' : 'Sucessfully updated.';
        this.notificationService.showSuccessMsg(message);
        this.navigateToRoute(false);
      }
      else { this.showErrorMessages(data.messages); }
    }
  }

  cancel() {
    this.formReset(this.form);
    if (this.id > 0) this.mapRetriveData(this.customer);
    else this.form.get('isActive')?.setValue(true);
  }
 
  // cancel() {
  //   this.formReset(this.form);
  //   this.form.get('isActive')?.setValue(true);
  // }
  getStatusString(): string { return (this.form.get('isActive')?.value === true) ? 'Active' : 'In-Active'; }

  
 

}

