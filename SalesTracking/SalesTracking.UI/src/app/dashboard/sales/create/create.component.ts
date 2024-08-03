import { Component } from '@angular/core';
import { FormArray, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import Validation from 'src/shared/common/valdators/confirmed.validator';
import { RegX } from 'src/shared/utilities/regex.common';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { DataService } from 'src/shared/services/data.service';
import { NotificationService } from 'src/shared/services/global/notification-service';
import { BaseComponent } from 'src/shared/common/base.component';
import { finalize } from 'rxjs';
import { PaginationConfigService } from 'src/shared/services/global/pagination-config.service';
import { DataStateChangeEvent, GridDataResult, PagerSettings } from '@progress/kendo-angular-grid';
import { State, process } from '@progress/kendo-data-query';

@Component({
  selector: 'app-dashboard-sales-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.scss']
})
export class SalesCreateComponent extends BaseComponent {
  @BlockUI() blockUI: NgBlockUI;
  public currentDate: Date = new Date();
  public state: State;
  public statePayments: State;
  public paginationConfig: PagerSettings;
  public gridData: GridDataResult;
  public gridDataPayments: GridDataResult;

  loadedCustomer: any[] = [];
  loadedProduct: any[] = [];
  loadedsellprice: any = [];
  loadedQOH: any = [];
  public sales: [];
  products: any = [];
  id = 0;
  isView = false;
  isLinear = false;
  isValidatorRemoved = false;
  sale: any = {};
  customer: any[];

  public form = new FormGroup({
    id: new FormControl(0),
    transactionDate: new FormControl('', [Validators.required]),
    invoiceNo: new FormControl('', [Validators.required]),
    isActive: new FormControl(true),
    customerId: new FormControl(0, [Validators.required]),
    totalAmout: new FormControl(0),
    totalPayment: new FormControl(0),
    balancePayment: new FormControl(0),
    salesDetails: new FormArray<FormGroup>([]),
    payments: new FormArray<FormGroup>([]),
  });

  public formProduct = new FormGroup({
    product: new FormControl(null, [Validators.required]),
    productId: new FormControl(0),
    productName: new FormControl(''),
    // price: new FormControl(null, [Validators.required]),
    sellPrice: new FormControl(0),
    qoh: new FormControl(0),
    priceId: new FormControl(0),
    unitPrice: new FormControl(0),
    qty: new FormControl('', [Validators.required]),
    salesId: new FormControl(0),
    stockBalanceId: new FormControl(0),

  }
  );

  constructor(private router: Router,
    private dataService: DataService,
    public override notificationService: NotificationService,
    public paginationService: PaginationConfigService,
    private route: ActivatedRoute) {
    super(notificationService);
    this.intializeComponents();
    this.retrivecustomer();
    this.retriveproducts();
    this.retrivePaymentTypes();

  }

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.id = params['id'] ? parseInt(params['id']) : 0;
      if (params['id']) this.isView = params['isView'] ? params['isView'] === 'false' ? false : true : true;
    });
    this.retrieveSales();
  }



  intializeComponents() {
    this.paginationConfig = this.paginationService.updatePagination();
    this.state = this.paginationService.state;
    this.statePayments = this.paginationService.state;
  }

  navigateToRoute(isReroute: boolean) {
    if (isReroute) this.router.navigate(['./dashboard/sales/create']);
    else this.router.navigate(['./dashboard/sales']);
  }
  getStatusString(): string { return (this.form.get('isActive')?.value === true) ? 'Active' : 'In-Active'; }

  retrivecustomer() {
    this.loadedCustomer = [];
    this.blockUI.start('Please Wait...');
    const url = 'customerData/getCustomers';
    this.dataService
      .getAllByGet(url)
      .pipe(finalize(() => this.blockUI.stop()))
      .subscribe((data: any) => {
        if (data != null) {
          if (!data.isError) { this.loadedCustomer = data.returnObject; }
          else { this.showErrorMessages(data.messages); }
        }
      });
  }

  retriveproducts() {
    this.loadedProduct = [];
    this.blockUI.start('Please Wait...');
    const url = 'productData/getProducts';
    this.dataService
      .getAllByGet(url)
      .pipe(finalize(() => this.blockUI.stop()))
      .subscribe((data: any) => {
        if (data != null) {
          if (!data.isError) { this.loadedProduct = data.returnObject; }
          else { this.showErrorMessages(data.messages); }
        }
      });
  }

  retriveSellprice() {
    this.formProduct.controls.sellPrice.reset();
    const productdata = this.formProduct.getRawValue();
    const productId = productdata.product != null ? parseInt(productdata.product['id']) : 0;
    this.loadedsellprice = [];
    this.blockUI.start('Please Wait...');
    const url = 'productData/getSellingPriceByItem';
    const params = { 'id': productId };
    this.dataService
      .getAllByGet(url, params)
      .pipe(finalize(() => this.blockUI.stop()))
      .subscribe((data: any) => {
        if (data != null) {
          if (!data.isError) {
            this.loadedsellprice = data.returnObject;
            this.formProduct.controls.sellPrice.patchValue(this.loadedsellprice.sellPrice);
            this.formProduct.controls.priceId.patchValue(this.loadedsellprice.id)
            this.formProduct.controls.unitPrice.patchValue(this.loadedsellprice.unitPrice)
          }
          else { this.showErrorMessages(data.messages); }
        }
      });
  }

  getQOH(){
    const productdata = this.formProduct.getRawValue();
    const productId = productdata.product != null ? parseInt(productdata.product['id']) : 0;
    this.loadedQOH=[];
    this.blockUI.start('Please Wait...');
    const url = 'productData/GetAvaibleProductQty';
    const params = { 'id': productId };
    this.dataService
      .getAllByGet(url, params)
      .pipe(finalize(() => this.blockUI.stop()))
      .subscribe((data: any) => {
        if (data != null) {
          if (!data.isError) {
            this.loadedsellprice = data.returnObject;
            this.formProduct.controls.sellPrice.patchValue(this.loadedsellprice.sellPrice);
          }
          else { this.showErrorMessages(data.messages); }
        }
      });


  }



  public dataStateChange(state: DataStateChangeEvent): void {
    this.state = state;
    this.gridData = process(this.sales, this.state);

  }

  calculateTotalAmount() {
    debugger
    let sum: number = 0;
    const sellPrice: any = this.formProduct.controls.sellPrice.value
    const totPayAmy : any =this.form.controls.totalPayment.value; 
    const qty: any = this.formProduct.controls.qty.value

    this.products.forEach((amount: any) => {
      sum += amount.sellPrice * amount.qty
     
    }

    )
    this.form.controls.totalAmout.patchValue(sum);
    this.form.controls.balancePayment.patchValue(sum-totPayAmy);
      
  }


  addProduct() {

    if (this.formProduct.valid && !this.checkProductExists()) {
      const productdata = this.formProduct.getRawValue();
      this.formProduct.controls.productId.patchValue(productdata.product != null ? parseInt(productdata.product['id']) : 0);
      //this.formProduct.controls.sellPrice.patchValue(productdata.price != null ? parseInt(productdata.price['sellPrice']) : 0);
      this.formProduct.controls.productName.patchValue(productdata.product != null ? productdata.product['name'] : '');
      this.formProduct.controls.salesId.patchValue(0);
      this.products.push(this.formProduct.getRawValue());
      this.gridData = process(this.products, this.state);
      this.calculateTotalAmount();

      // const sellPrice: any = this.formProduct.controls.sellPrice.value;
      // const qty: any = this.formProduct.controls.qty.value;
      // let sum: number = 0;
      // this.products.forEach((amount: any) => { sum += amount.sellPrice * amount.qty }
      // )
      // this.form.controls.totalAmount.patchValue(sum);

      this.formReset(this.formProduct);
      
    }
    else { this.formValidation(this.formProduct); }

  }
  checkProductExists(): boolean {
    let value = false;
    const productdata = this.formProduct.getRawValue();
    const productId = productdata.product != null ? parseInt(productdata.product['id']) : 0;
    //const sellPrice = productdata.price != null ? parseInt(productdata.price['sellPrice']) : 0;
    const exitingProdcuts = this.products.filter((a: any) => { return a.productId === productId });
    value = (exitingProdcuts && exitingProdcuts.length > 0);
    if (value) {
      this.notificationService.showErrorMsg("Product already added.");
    }
    return value;
  }

  deleteProduct(item: any) {

    const index = this.products.indexOf(item);
    this.products.splice(index, 1);
    this.gridData = process(this.products, this.state);
    this.calculateTotalAmount();

    // const sellPrice: any = this.formProduct.controls.sellPrice.value;
    // const qty: any = this.formProduct.controls.qty.value;
    // const totPayAmy : any =this.form.controls.totalPayment.value; 
    // let sum: number = 0;
    // this.products.forEach((amount: any) => { sum += amount.sellPrice * amount.qty }
    // )
    // this.form.controls.totalAmount.patchValue(sum);
    // this.form.controls.balancePayment.patchValue(sum-totPayAmy);
  }


  //------------Note:Payment-----------------

  loadpaymentTypes: any = [];
  chequeNos: any = [];
  public payments: [];
  show = false;

  public formPayment = new FormGroup({
    paymentType: new FormControl(null, [Validators.required]),
    paymentTypeId: new FormControl(0),
    paymentTypeName: new FormControl(''),
    chequeNo: new FormControl('', [Validators.required]),
    chequeDate: new FormControl('', [Validators.required]),
    amount: new FormControl('', [Validators.required]),
    note: new FormControl('', [Validators.required]),
    salesId: new FormControl(0),
    totalAmount: new FormControl(0),

  });

  retrivePaymentTypes() {
    this.loadpaymentTypes = [];
    this.blockUI.start('Please Wait...');
    const url = 'masterdata/getPaymentTypes';
    this.dataService
      .getAllByGet(url)
      .pipe(finalize(() => this.blockUI.stop()))
      .subscribe((data: any) => {
        if (data != null && !data.isError) {
          this.loadpaymentTypes = data.returnObject;
          
        }
        else { this.showErrorMessages(data.messages); }

      });

  }

  public dataStateChangePayment(statePayments: DataStateChangeEvent): void {
    this.statePayments = statePayments;
    this.gridDataPayments = process(this.payments, this.statePayments);

  }

  deletePayment(cheqno: any) {
    const index = this.chequeNos.indexOf(cheqno);
    this.chequeNos.splice(index, 1);
    this.gridDataPayments = process(this.chequeNos, this.statePayments);
    this.calculatePayment();
  }

  addPayments() {
    if (this.formPayment.valid && !this.checkChequeNoExists() && this.show == true) {
      const paymentData = this.formPayment.getRawValue();
      this.formPayment.controls.paymentTypeId.patchValue(paymentData.paymentType != null ? parseInt(paymentData.paymentType['id']) : 0);
      this.formPayment.controls.paymentTypeName.patchValue(paymentData.paymentType != null ? (paymentData.paymentType['name']) : '');
      this.formPayment.controls.salesId.patchValue(0);

      this.chequeNos.push(this.formPayment.getRawValue());
      this.gridDataPayments = process(this.chequeNos, this.statePayments);

      //calculate total payments
      this.calculatePayment();    
      const paymentDatat = this.formPayment.getRawValue();

      this.formReset(this.formPayment);
    }
    else { this.formValidation(this.formPayment); }

    const payamount: any = this.formPayment.controls.amount.value;

    if (this.show == false && payamount > 0.00) {
      const paymentData = this.formPayment.getRawValue();
      this.formPayment.controls.paymentTypeId.patchValue(paymentData.paymentType != null ? parseInt(paymentData.paymentType['id']) : 0);
      this.formPayment.controls.paymentTypeName.patchValue(paymentData.paymentType != null ? (paymentData.paymentType['name']) : '');
      this.formPayment.controls.salesId.patchValue(0);
      this.formPayment.controls.chequeNo.patchValue(null);
      this.formPayment.controls.chequeDate.patchValue(null);
      this.chequeNos.push(this.formPayment.getRawValue());
      this.gridDataPayments = process(this.chequeNos, this.statePayments);

      debugger
      this.calculatePayment();      
      this.formReset(this.formPayment);
    }
  }

  calculatePayment(){ debugger
    let sum: number = 0;
      const amount: any = this.formPayment.controls.amount.value;
      const totAmt:any=this.form.controls.totalAmout.value; 
      this.chequeNos.forEach((paymentAmt: any) => { sum += paymentAmt.amount }

      )
      this.form.controls.totalPayment.patchValue(sum);
      this.form.controls.balancePayment.patchValue(totAmt-sum);
  }


  checkpaymenttype() {
    const selectedPaymentType: any = this.formPayment.controls.paymentType.value;
    this.show = (selectedPaymentType.id === 2);
    this.formPayment.controls.amount.reset();
  }

  checkChequeNoExists(): boolean {
    let value = true;
    const cheque = this.formPayment.controls.chequeNo.value;
    const exitingChequeno = this.chequeNos.filter((a: any) => { return a.chequeNo === cheque });
    value = (exitingChequeno && exitingChequeno.length > 0);
    if (value) {
      this.notificationService.showErrorMsg("Cheque No already added.");
    }
    return value;
  }

  //------------Common------------------------

  retrieveSales() {

    if (this.id > 0) {
      this.sale = {};
      this.blockUI.start('Please Wait...');
      const url = 'sales/getSalesById';
      const params = { 'id': this.id };
      this.dataService
        .getAllByGet(url, params)
        .pipe(finalize(() => this.blockUI.stop()))
        .subscribe((data: any) => {
          if (data != null) {
            if (!data.isError) {
              if (data.returnObject.isApproved) { this.isView = true; this.form.disable(); }
              this.sale = data.returnObject;
              this.mapData(data.returnObject);
            }
            else { this.showErrorMessages(data.messages); }
          }
        });
      if (this.isView) this.form.disable();
      else this.form.enable();
    }
  }

  mapData(data: any) {
    
    this.form.patchValue({ ...data });
    this.products = [...data.salesDetails];
    this.gridData = process(this.products, this.state);
    this.chequeNos = [...data.payments];
    this.gridDataPayments = process(this.chequeNos, this.statePayments);
    this.form.updateValueAndValidity();
    this.calculateTotalAmount();
    this.calculatePayment();
  }

  cancel() {
    this.formReset(this.form);
    if (this.id > 0) this.mapData(this.sale);
    else this.form.get('isActive')?.setValue(true);
  }
  save() {
    debugger
    const url = 'sales/' + ((this.id === 0) ? 'addSales' : 'updateSales');
    if (this.form.valid && this.checkProuctsAddValidation()) {
      this.blockUI.start('Please Wait...');
      this.dataService
        .getAllByPost(url, this.mapSaveData())
        .pipe(finalize(() => this.blockUI.stop()))
        .subscribe((data: any) => { this.handleResponse(data) });
    }
    else {
      if (this.form.valid) {
        this.notificationService.showErrorMsg("Please add at least one product.");
      }
      else {
        this.notificationService.showWarningMsg('Please fill required fields.');
      }
      this.formValidation(this.form);
    }
  }

  checkProuctsAddValidation(): boolean {
    return this.products && this.products.length > 0;
  }

  chequeNosvalidate(): boolean {
    return this.chequeNos && this.chequeNos.length > 0;

  }

  mapSaveData() {
    debugger
    const data = this.form.getRawValue();
    return { ...data, salesDetails: this.products, payments: this.chequeNos };
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

  approve() {
    debugger
    const url = 'sales/approveSales'
    if (this.form.valid && this.checkProuctsAddValidation() && this.chequeNosvalidate()) {
      this.blockUI.start('Please Wait...');
      this.dataService
        .getAllByPost(url, this.mapSaveData())
        .pipe(finalize(() => this.blockUI.stop()))
        .subscribe((data: any) => { this.handleResponse(data) });
    }
    else {
      if (this.form.valid) {
        this.notificationService.showErrorMsg("Please add at least one product and Payment Details to Proceed.");
      }
      else {
        this.notificationService.showWarningMsg('Please fill required fields.');
      }
      this.formValidation(this.form);
    }
  }




}