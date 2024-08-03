import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { finalize } from 'rxjs';
import { BaseComponent } from 'src/shared/common/base.component';
import { DataService } from 'src/shared/services/data.service';
import { AuthTokenHandlerService } from 'src/shared/services/global/authenciation-token-handler';
import { NotificationService } from 'src/shared/services/global/notification-service';
import { RegX } from 'src/shared/utilities/regex.common';

@Component({
  selector: 'app-auth-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})

export class LoginComponent extends BaseComponent {
  @BlockUI() blockUI: NgBlockUI;
  constructor(private dataService: DataService, private router: Router
    , private authTokenService: AuthTokenHandlerService,
    public override notificationService: NotificationService) {
    super(notificationService);
  }

  public loginForm = new FormGroup({
    userName: new FormControl('', [Validators.required, Validators.pattern(RegX.EMAIL_REGEX)]),
    password: new FormControl('', [Validators.required])
  });

  signing() {
    const url = 'auth/login';
    if (this.loginForm.valid) {
      this.blockUI.start('Please Wait...');
      let data = this.loginForm.getRawValue();
      this.dataService
        .getAllByPost(url, data)
        .pipe(finalize(() => this.blockUI.stop()))
        .subscribe((data: any) => {
          this.handleResponse(data)
        });
    }
    else {
      this.notificationService.showWarningMsg('Please fill required fields.');
      this.formValidation(this.loginForm);
    }
  }

  handleResponse(data: any) {
    if (data != null) {
      if (data.canLogin) {
        this.authTokenService.setTokens(data);
        this.router.navigate(['/dashboard']);
      }
      else {
        this.notificationService.showErrorMsg(data.loginValidationMessage);
      }
    }
  }
}



