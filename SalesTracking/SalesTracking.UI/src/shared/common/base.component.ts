import { Component } from "@angular/core";
import { FormGroup } from "@angular/forms";
import { NotificationService } from 'src/shared/services/global/notification-service';

@Component({
    selector: 'base',
    template: '',
    styleUrls: []
})
export class BaseComponent {

    constructor(public notificationService: NotificationService){

    }

    formValidation(formGroup: FormGroup) {
        (<any>Object).values(formGroup.controls).forEach((control: FormGroup) => {
            control.markAsTouched();
            control.updateValueAndValidity();
            if (control.controls) { this.formValidation(control); }
        });
    }

    formReset(formgroup: FormGroup) {
        for (let controller in formgroup.controls) {
            formgroup.get(controller)?.reset(undefined);
            formgroup.get(controller)?.markAsUntouched();
            formgroup.get(controller)?.markAsPristine();
            formgroup.get(controller)?.updateValueAndValidity();
        }
    }

    showErrorMessages(messages:any){
        if(messages && messages !== null){
            messages.forEach((m:any)=>{
              this.notificationService.showErrorMsg(m.description);
            });
          }
    }
}