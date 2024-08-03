import { Injectable } from '@angular/core';
import swal, { SweetAlertIcon } from 'sweetalert2';

@Injectable({ providedIn: 'root' })
export class NotificationService {

    public showWarningMsg(msg: string) {
        swal.fire({
            title: 'WARNING',
            text: msg,
            icon: 'warning',
            showClass: { popup: 'animate__animated animate__fadeInDown' },
            hideClass: { popup: 'animate__animated animate__fadeOutUp' }
        });
    }

    public showErrorMsg(msg: string) {
        swal.fire({
            title: 'ERROR',
            text: msg,
            icon: 'error',
            showConfirmButton: false,
            timer: 2000,
            timerProgressBar: true,
            showClass: { popup: 'animate__animated animate__fadeInDown' },
            hideClass: { popup: 'animate__animated animate__fadeOutUp' }
        });
    }

    public showSuccessMsg(msg: string) {
        swal.fire({
            title: 'SUCCESS',
            text: msg,
            icon: 'success',
            showClass: { popup: 'animate__animated animate__fadeInDown' },
            hideClass: { popup: 'animate__animated animate__fadeOutUp' }
        });
    }

    public showInfoMsg(msg: string) {
        swal.fire({
            title: 'INFO',
            text: msg,
            icon: 'info',
            showClass: { popup: 'animate__animated animate__fadeInDown' },
            hideClass: { popup: 'animate__animated animate__fadeOutUp' }
        });
    }

    public showCustomMsg(title:string , msg: string , icon : SweetAlertIcon) {
        swal.fire({
            title: title,
            text: msg,
            icon: icon,
            showClass: { popup: 'animate__animated animate__fadeInDown' },
            hideClass: { popup: 'animate__animated animate__fadeOutUp' }
        });
    }

}

