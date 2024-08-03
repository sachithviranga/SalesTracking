import { Component } from '@angular/core';
import { AbstractControl, FormArray, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { DataService } from 'src/shared/services/data.service';
import { NotificationService } from 'src/shared/services/global/notification-service';
import { BaseComponent } from 'src/shared/common/base.component';
import { finalize } from 'rxjs';
import { PaginationConfigService } from 'src/shared/services/global/pagination-config.service';
import { DataStateChangeEvent, GridDataResult, PagerSettings } from '@progress/kendo-angular-grid';
import { State, process } from '@progress/kendo-data-query';

@Component({
  selector: 'app-dashboard-stock-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.scss']
})
export class StockCreateComponent extends BaseComponent { 
  @BlockUI() blockUI: NgBlockUI;
  
  public currentDate: Date = new Date();
  public state: State;
  public statePayments: State;
  public paginationConfig: PagerSettings;
  public gridData: GridDataResult;
  public gridDataPayments: GridDataResult;
  products: any = [];

  isLinear = false;
  loadedProduct: any[] = [];
  id = 0;
  isView = false;
  user: any = {};
  isValidatorRemoved = false;
  pricevalidate = true;
  loadedunitprice: any = [];

  loadpaymentTypes: any = [];
  displaypaymenttype: any;
  chequeNos: any = [];
  payValue: number = 0.00;

  show = false;

  public form = new FormGroup({
    id: new FormControl(0),
    transactionDate: new FormControl('', [Validators.required]),
    purchaseNo: new FormControl('', [Validators.required]),
    isActive: new FormControl(true),
    stockPurchaseDetails: new FormArray<FormGroup>([]),
    totalAmount: new FormControl(0),
    totalPayment: new FormControl(0),
    balancePayment: new FormControl(0),
    stockPurchasePayment: new FormArray<FormGroup>([])
  });



  public formProduct = new FormGroup({
    product: new FormControl(null, [Validators.required]),
    productId: new FormControl(0),
    productName: new FormControl(''),
    qty: new FormControl('', [Validators.required]),   
    unitPrice: new FormControl('', [Validators.required]),
    stockPurchaseId: new FormControl(0),
    priceId: new FormControl(0),
    amount: new FormControl(0),
  }//, { validators: this.priceComparisonValidator }
  );

  constructor(private router: Router,
    private dataService: DataService,
    public override notificationService: NotificationService,
    public paginationService: PaginationConfigService,
    private route: ActivatedRoute) {
    super(notificationService);
    this.intializeComponents();
    this.retriveproducts();
    this.retrivePaymentTypes();
  }


  ngOnInit() {
    this.route.params.subscribe(params => {
      this.id = params['id'] ? parseInt(params['id']) : 0;
      if (params['id']) this.isView = params['isView'] ? params['isView'] === 'false' ? false : true : true;
    });
    debugger
    //const currentDate: Date = new Date();
    //this.form.controls.transactionDate.patchValue();
    this.retriveStock();
  }

  intializeComponents() {
    this.paginationConfig = this.paginationService.updatePagination();
    this.state = this.paginationService.state;
    this.statePayments = this.paginationService.state;
  }

  public dataStateChange(state: DataStateChangeEvent): void {
    this.state = state;
    this.gridData = process(this.products, this.state);
  }

  retriveStock() {
    if (this.id > 0) {
      this.user = {};
      this.blockUI.start('Please Wait...');
      const url = 'stock/getStockById';
      const params = { 'id': this.id };
      this.dataService
        .getAllByGet(url, params)
        .pipe(finalize(() => this.blockUI.stop()))
        .subscribe((data: any) => {
          if (data != null) {
            if (!data.isError) {
              if ( data.returnObject.isApproved) { this.isView = true; this.form.disable();}
              this.user = data.returnObject;
              this.mapData(data.returnObject);
              this.calculateTotalAmount();
             
              
            }
            else { this.showErrorMessages(data.messages); }
          }
        });
      if (this.isView) this.form.disable();
      else this.form.enable();
    }
  }

  mapData(data: any) {
    const stockdetails = data.stockPurchaseDetails.map((o: any) => { return o.stockPurchaseId; });
    this.form.patchValue({ ...data });
    this.products = [...data.stockPurchaseDetails];
    this.gridData = process(this.products, this.state);
    this.chequeNos = [...data.stockPurchasePayment];
    this.gridDataPayments = process(this.chequeNos, this.statePayments);
    this.form.updateValueAndValidity();
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

  retriveUnitprice() {
    const productdata = this.formProduct.getRawValue();
    const productId = productdata.product != null ? parseInt(productdata.product['id']) : 0;
    this.loadedunitprice = [];
    this.blockUI.start('Please Wait...');
    const url = 'productData/getSellingPriceByItem';
    const params = { 'id': productId };
    this.dataService
      .getAllByGet(url, params)
      .pipe(finalize(() => this.blockUI.stop()))
      .subscribe((data: any) => {
        if (data != null) {
          if (!data.isError) {
            this.loadedunitprice = data.returnObject;
            this.formProduct.controls.unitPrice.patchValue(this.loadedunitprice.unitPrice);
            this.formProduct.controls.priceId.patchValue(this.loadedunitprice.id)
          }
          else { this.showErrorMessages(data.messages); }
        }
      });
  }


  navigateToRoute(isReroute: boolean) {
    if (isReroute) this.router.navigate(['./dashboard/stock/create']);
    else this.router.navigate(['./dashboard/stock']);
  }

  save() {
    debugger
    const url = 'stock/' + ((this.id === 0) ? 'addstock' : 'updatestock');
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

  mapSaveData() {
    debugger
    const data = this.form.getRawValue();
    return { ...data, stockPurchaseDetails: this.products, stockPurchasePayment: this.chequeNos };
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
    if (this.id > 0) this.mapData(this.user);
    else this.form.get('isActive')?.setValue(true);
  }

  getStatusString(): string { return (this.form.get('isActive')?.value === true) ? 'Active' : 'In-Active'; }

  addProduct() {
    if (this.formProduct.valid && !this.checkProductExists()) {
      const unitPrice: any = this.formProduct.controls.unitPrice.value
      const qty: any = this.formProduct.controls.qty.value

      let amt: number = 0;

      amt=unitPrice*qty;
      const productdata = this.formProduct.getRawValue();
      this.formProduct.controls.productId.patchValue(productdata.product != null ? parseInt(productdata.product['id']) : 0);
      this.formProduct.controls.productName.patchValue(productdata.product != null ? productdata.product['name'] : '');
      this.formProduct.controls.stockPurchaseId.patchValue(0);
      this.formProduct.controls.amount.patchValue(amt);
      this.products.push(this.formProduct.getRawValue());
      this.gridData = process(this.products, this.state);
      
      this.calculateTotalAmount();
      this.formReset(this.formProduct);
    }
    else { this.formValidation(this.formProduct); }
  }

  calculateTotalAmount() {
   
    let sum: number = 0;
    let totalPayment : number=0;
    const unitPrice: any = this.formProduct.controls.unitPrice.value
    const qty: any = this.formProduct.controls.qty.value
    const totPayAmy : any =this.form.controls.totalPayment.value;    
    this.products.forEach((amount: any) => { sum += amount.unitPrice * amount.qty }
    )
    this.form.controls.totalAmount.patchValue(sum);  
    this.form.controls.balancePayment.patchValue(sum-totPayAmy);
    
  }

  checkProductExists(): boolean {
    let value = false;
    const productdata = this.formProduct.getRawValue();
    const productId = productdata.product != null ? parseInt(productdata.product['id']) : 0;
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
  }

  //------------------------------ Payment----------------------------------


  public formPayment = new FormGroup({
    paymentType: new FormControl(null, [Validators.required]),
    paymentTypeId: new FormControl(0),
    paymentTypeName: new FormControl(''),
    chequeNo: new FormControl('', [Validators.required]),
    chequeDate: new FormControl('', [Validators.required]),
    amount: new FormControl('', [Validators.required]),
    note: new FormControl('', [Validators.required]),
    stockPurchaseId: new FormControl(0),

  });

  retrivePaymentTypes() {
    this.loadpaymentTypes = [];
    this.blockUI.start('Please Wait...');
    const url = 'masterdata/getPaymentTypes';
    this.dataService
      .getAllByGet(url)
      .pipe(finalize(() => this.blockUI.stop()))
      .subscribe((data: any) => {
        if (data != null) {
          if (!data.isError) { this.loadpaymentTypes = data.returnObject; }
          else { this.showErrorMessages(data.messages); }
        }
      });
  }


  public dataPaymentStateChange(state: DataStateChangeEvent): void {
    this.statePayments = state;
    this.gridData = process(this.chequeNos, this.statePayments);
  }

  checkpaymenttype() {
    const selectedPaymentType: any = this.formPayment.controls.paymentType.value;
    this.show = (selectedPaymentType.id === 2);
    this.resetvalues();
  }

  resetvalues() { this.formPayment.controls.amount.reset(); }

  addPayments() {
    if (this.formPayment.valid && !this.checkChequeNoExists() && this.show == true) {
      const paymentData = this.formPayment.getRawValue();
      this.formPayment.controls.paymentTypeId.patchValue(paymentData.paymentType != null ? parseInt(paymentData.paymentType['id']) : 0);
      this.formPayment.controls.paymentTypeName.patchValue(paymentData.paymentType != null ? (paymentData.paymentType['name']) : '');
      this.formPayment.controls.stockPurchaseId.patchValue(0);

      this.chequeNos.push(this.formPayment.getRawValue());
      this.gridDataPayments = process(this.chequeNos, this.statePayments);
      this.calculatePayment();
      this.formReset(this.formPayment);
    }
    else { this.formValidation(this.formPayment); }

    const payamount: any = this.formPayment.controls.amount.value;

    if (this.show == false && payamount > 0.00) {
      const paymentData = this.formPayment.getRawValue();
      this.formPayment.controls.paymentTypeId.patchValue(paymentData.paymentType != null ? parseInt(paymentData.paymentType['id']) : 0);
      this.formPayment.controls.paymentTypeName.patchValue(paymentData.paymentType != null ? (paymentData.paymentType['name']) : '');
      this.formPayment.controls.stockPurchaseId.patchValue(0);
      this.formPayment.controls.chequeNo.patchValue(null);
      this.formPayment.controls.chequeDate.patchValue(null);
      this.chequeNos.push(this.formPayment.getRawValue());
      this.gridDataPayments = process(this.chequeNos, this.statePayments);
      this.calculatePayment();
      this.formReset(this.formPayment);
    }
  }

  calculatePayment(){
    let sum: number = 0;
    const amount: any = this.formPayment.controls.amount.value;
    const totAmt:any=this.form.controls.totalAmount.value;
    this.chequeNos.forEach((paymentAmt: any) => { sum += paymentAmt.amount }

    )
    this.form.controls.totalPayment.patchValue(sum);
    this.form.controls.balancePayment.patchValue(totAmt-sum);
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

  deletePayment(cheqno: any) {
    const index = this.chequeNos.indexOf(cheqno);
    this.chequeNos.splice(index, 1);
    this.gridDataPayments = process(this.chequeNos, this.statePayments);
    this.calculatePayment();
  }

  approve() {
    debugger
    const url = 'stock/approvestock';
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

  chequeNosvalidate(): boolean {
    return this.chequeNos && this.chequeNos.length > 0;

  }
}