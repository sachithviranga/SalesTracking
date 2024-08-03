import { Component } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { DataService } from 'src/shared/services/data.service';
import { NotificationService } from 'src/shared/services/global/notification-service';
import { BaseComponent } from 'src/shared/common/base.component';
import { finalize } from 'rxjs';

@Component({
  selector: 'app-dashboard-role-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.scss']
})
export class RoleCreateComponent extends BaseComponent {
  @BlockUI() blockUI: NgBlockUI;
  public form = new FormGroup({
    id: new FormControl(0),
    roleName: new FormControl('', [Validators.required]),
    isActive: new FormControl(true),
    roleClaim: new FormArray<FormGroup>([]),
    modules: new FormArray<FormGroup>([]),
  });

  id = 0;
  isView = false;
  role: any = {};
  isValidatorRemoved = false;
  modules: any = [];
  public collapseSts: boolean = true;

  constructor(private router: Router,
    private dataService: DataService,
    public override notificationService: NotificationService,
    private route: ActivatedRoute,
    public fb: FormBuilder) {
    super(notificationService);
  }

  retriveModules() {
    this.modules = [];
    this.blockUI.start('Please Wait...');
    const url = 'masterData/getModules';
    this.dataService
      .getAllByGet(url)
      .pipe(finalize(() => this.blockUI.stop()))
      .subscribe((data: any) => {
        if (data != null) {
          if (!data.isError) {
            this.modules = data.returnObject;
            this.retrivedata();
          }
          else { this.showErrorMessages(data.messages); }
        }
      });
  }

  createFormGroups() {
    this.form.controls.modules = new FormArray<FormGroup>([]);
    this.modules.forEach((module: any) => {
      this.form.controls.modules.push(this.newModuleFormGroup(module));
    });
    if (this.isView) this.form.disable();
    else this.form.enable();
  }

  newModuleFormGroup(module: any): FormGroup {
    const moduleFormGroup = this.fb.group({
      id: new FormControl(module.id),
      moduleName: new FormControl(module.moduleName),
      claims: new FormArray<FormGroup>([])
    });

    module.claim.forEach((claim: any) => {
      moduleFormGroup.controls.claims.push(this.newClaimFormGroup(claim));
    });

    return moduleFormGroup;
  }

  newClaimFormGroup(claim: any): FormGroup {
    let value = false;
    if (this.id > 0 && this.role.roleClaim && this.role.roleClaim.length > 0) {
      const selected = this.role.roleClaim.filter((s: any) => { return s.claimId === claim.id; });
      if (selected && selected.length > 0) {
        value = true;
      }
    }
    return this.fb.group({
      isActive: new FormControl(value),
      claimName: new FormControl(claim.claimName),
      claimId: new FormControl(claim.id)
    });
  }

  moduleFromArray(): FormArray {
    return this.form.controls.modules as FormArray;
  }

  claimFormArray(i: number): FormArray {
    return this.moduleFromArray().at(i).get('claims') as FormArray;
  }

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.id = params['id'] ? parseInt(params['id']) : 0;
      if (params['id']) this.isView = params['isView'] ? params['isView'] === 'false' ? false : true : true;
    });
    this.retriveModules();
  }

  retrivedata() {
    if (this.id > 0) {
      this.role = {};
      this.blockUI.start('Please Wait...');
      const url = 'roleData/getRoleById';
      const params = { 'id': this.id };
      this.dataService
        .getAllByGet(url, params)
        .pipe(finalize(() => this.blockUI.stop()))
        .subscribe((data: any) => {
          if (data != null) {
            if (!data.isError) {
              this.role = data.returnObject;
              this.mapData(data.returnObject);
            }
            else { this.showErrorMessages(data.messages); }
          }
        });
      if (this.isView) this.form.disable();
      else this.form.enable();
    }
    else this.createFormGroups();
  }

  mapData(data: any) {
    this.createFormGroups();
    this.form.patchValue({ ...data });
    this.form.updateValueAndValidity();
  }

  navigateToRoute(isReroute: boolean) {
    if (isReroute) this.router.navigate(['./dashboard/role/create']);
    else this.router.navigate(['./dashboard/role']);
  }

  save() { debugger
    const url = 'roledata/' + ((this.id === 0) ? 'addrole' : 'updaterole');
    if (this.form.valid && this.isClaimSelected()) {
      this.blockUI.start('Please Wait...');
      this.dataService
        .getAllByPost(url, this.mapSaveData())
        .pipe(finalize(() => this.blockUI.stop()))
        .subscribe((data: any) => { this.handleResponse(data) });
    }
    else {
      if (!this.form.valid) {
        this.notificationService.showWarningMsg('Please fill required fields.');
        this.formValidation(this.form);
      }
      else {
        this.notificationService.showWarningMsg('Please select at least one cliam.');
      }
    }
  }

  isClaimSelected(): boolean {
    let valid = false;
    let data = this.form.getRawValue();
    data.modules.forEach(module => {
      module['claims'].forEach((claim: any) => {
        if (claim.isActive) {
          valid = true;
          return;
        }
      });
    });
    return valid;

  }

  mapSaveData() { debugger
    let data = this.form.getRawValue();
    const roleClaims: any = [];
    data.modules.forEach(module => {
      module['claims'].forEach((claim: any) => {
        if (claim.isActive) { roleClaims.push(claim); }
      });
    });
    data = { ...data, roleClaim: roleClaims };
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
    if (this.id > 0) this.mapData(this.role);
    else {
      this.createFormGroups();
      this.form.get('isActive')?.setValue(true);
    }
  }

  getStatusString(): string { return (this.form.get('isActive')?.value === true) ? 'Active' : 'In-Active'; }


}

