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
  selector: 'app-dashboard-user-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.scss']
})
export class UserCreateComponent extends BaseComponent {
  @BlockUI() blockUI: NgBlockUI;
  public form = new FormGroup({
    id: new FormControl(0),
    firstName: new FormControl('', [Validators.required]),
    lastName: new FormControl(''),
    email: new FormControl('', [Validators.required, Validators.pattern(RegX.EMAIL_REGEX)]),
    password: new FormControl(''),
    confirmpassword: new FormControl(''),
    phoneNumber: new FormControl('', [Validators.required]),
    isActive: new FormControl(true),
    userRole: new FormControl([], [Validators.required])
  });
  loadedRoles: any[] = [];
  id = 0;
  isView = false;
  user: any = {};
  isValidatorRemoved = false;

  constructor(private router: Router,
    private dataService: DataService,
    public override notificationService: NotificationService,
    private route: ActivatedRoute) {
    super(notificationService);
    this.retriveRoles();
  }

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.id = params['id'] ? parseInt(params['id']) : 0;
      if (params['id']) this.isView = params['isView'] ? params['isView'] === 'false' ? false : true : true;
    });
    this.addRemoveControllerValidators();
    this.retriveUser();
  }

  retriveUser() {
    if (this.id > 0) {
      this.user = {};
      this.blockUI.start('Please Wait...');
      const url = 'userData/getUserByUserId';
      const params = { 'id': this.id };
      this.dataService
        .getAllByGet(url, params)
        .pipe(finalize(() => this.blockUI.stop()))
        .subscribe((data: any) => {
          if (data != null) {
            if (!data.isError) { 
              this.user = data.returnObject;
              this.mapUser(data.returnObject); }
            else { this.showErrorMessages(data.messages); }
          }
        });
      if (this.isView) this.form.disable();
      else this.form.enable();

      if (this.id > 0) { this.form.controls.email.disable(); }
    }
  }

  mapUser(data: any) { debugger
    const userRole = data.userRole.map((o: any) => { return o.roleId; });
    this.form.patchValue({ ...data, userRole: userRole });
    this.form.updateValueAndValidity();
  }

  addRemoveControllerValidators() {
    if (this.id > 0) {
      this.form.controls.password.removeValidators([Validators.required, Validators.minLength(6), Validators.maxLength(40)]);
      this.form.controls.confirmpassword.removeValidators([Validators.required]);
      this.form.removeValidators([Validation.match('password', 'confirmpassword')]);
      this.isValidatorRemoved = true;
    }
    else {
      this.form.controls.password.addValidators([Validators.required, Validators.minLength(6), Validators.maxLength(40)]);
      this.form.controls.confirmpassword.addValidators([Validators.required]);
      this.form.addValidators([Validation.match('password', 'confirmpassword')]);
    }
    this.form.updateValueAndValidity();
  }

  retriveRoles() { 
    this.loadedRoles = [];
    this.blockUI.start('Please Wait...');
    const url = 'roledata/getroles';
    this.dataService
      .getAllByGet(url)
      .pipe(finalize(() => this.blockUI.stop()))
      .subscribe((data: any) => {
        if (data != null) {
          if (!data.isError) { this.loadedRoles = data.returnObject; }
          else { this.showErrorMessages(data.messages); }
        }
      });
  }

  navigateToRoute(isReroute: boolean) {
    if (isReroute) this.router.navigate(['./dashboard/user/create']);
    else this.router.navigate(['./dashboard/user']);
  }

  save() {
    const url = 'userdata/' + ((this.id === 0) ? 'adduser' : 'updateuser');
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

  mapSaveData() {
    const data = this.form.getRawValue();
    const userRole = data.userRole?.map(a => { return { roleId: a, userId: this.id }; });
    return { ...data, userRole: userRole };
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
    if (this.id > 0) this.mapUser(this.user);
    else this.form.get('isActive')?.setValue(true);
  }

  getStatusString(): string { return (this.form.get('isActive')?.value === true) ? 'Active' : 'In-Active'; }

  getSelectedRoleName(): string { 
    let name = '';
    const selectedRoles = this.form.get('userRole')?.value;
    if (selectedRoles && selectedRoles !== null && selectedRoles.length > 0) {
      const filteredRoles = this.loadedRoles.filter(a => a.id === selectedRoles[0]);
      name = filteredRoles[0]['roleName'];
    }
    return name;
  }

}

