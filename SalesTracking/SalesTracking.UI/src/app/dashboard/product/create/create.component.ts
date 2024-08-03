import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators, FormArray } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import Validation from 'src/shared/common/valdators/confirmed.validator';
import { RegX } from 'src/shared/utilities/regex.common';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { DataService } from 'src/shared/services/data.service';
import { NotificationService } from 'src/shared/services/global/notification-service';
import { BaseComponent } from 'src/shared/common/base.component';
import { finalize } from 'rxjs';

@Component({
  selector: 'app-dashboard-product-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.scss']
})
export class ProductCreateComponent extends BaseComponent {
  @BlockUI() blockUI: NgBlockUI;

  id = 0;
  isView = false;
  product: any = {};
  productPrices: any = {};
  isValidatorRemoved = false;

  public form = new FormGroup({
    id: new FormControl(0),
    name: new FormControl('', [Validators.required]),
    isActive: new FormControl(true),
    productPrice: new FormArray<FormGroup>([])
  });

  public formPrice = new FormGroup({
    unitPrice: new FormControl(0, [Validators.required]),
    sellPrice: new FormControl(0, [Validators.required]),
    productId: new FormControl(this.id),
  });


  constructor(private router: Router,
    private dataService: DataService,
    public override notificationService: NotificationService,
    private route: ActivatedRoute) {
    super(notificationService);

  }

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.id = params['id'] ? parseInt(params['id']) : 0;
      if (params['id']) this.isView = params['isView'] ? params['isView'] === 'false' ? false : true : true;
    });
    this.retrivedata();
  }

  retrivedata() {
    if (this.id > 0) {
      this.product = {};
      this.blockUI.start('Please Wait...');
      const url = 'productData/getProductById';
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

      //if (this.id > 0) { this.form.controls.email.disable(); }
    }
  }

  mapRetriveData(data: any) {
    this.product = { ...data };
    this.form.patchValue(this.product);
    if (data.productPrice && data.productPrice.length > 0) {
      this.formPrice.patchValue(data.productPrice[0]);
    }
    this.form.updateValueAndValidity();
    this.formPrice.updateValueAndValidity();
  }

  navigateToRoute(isReroute: boolean) {
    if (isReroute) this.router.navigate(['./dashboard/product/create']);
    else this.router.navigate(['./dashboard/product']);
  }

  save() {
    debugger
    const url = 'productdata/' + ((this.id === 0) ? 'addproduct' : 'updateproduct');
    if (this.form.valid) {
      this.blockUI.start('Please Wait...');
      this.dataService
        .getAllByPost(url, this.mapSaveData())
        .pipe(finalize(() => this.blockUI.stop()))
        .subscribe((data: any) => { this.handleResponse(data) });
    }
    else {
      this.notificationService.showWarningMsg('Please fill required fields.');
      this.formValidation(this.form);
    }
  }

  addPrice() {
    debugger
    const price = this.formPrice.getRawValue();
    this.productPrices.push(this.formPrice.getRawValue());

  }

  mapSaveData() {
    const data = this.form.getRawValue();
    data.productPrice.push(this.formPrice.getRawValue());
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
    if (this.id > 0) this.mapRetriveData(this.product);
    else this.form.get('isActive')?.setValue(true);
  }

  getStatusString(): string { return (this.form.get('isActive')?.value === true) ? 'Active' : 'In-Active'; }




}

