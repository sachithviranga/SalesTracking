import { Injectable } from '@angular/core';

@Injectable()
export class StorageHandlerService {

    public getLocalStorage(name: string): string | null{
        return localStorage.getItem(name);
    }

    public setLocalStorage(name: string, value: string) {
        localStorage.setItem(name, value);
    }

    public removeLocalStorage(name: string): void {
        localStorage.removeItem(name);
    }

    public getSessionStorage(name: string): string | null {
        return sessionStorage.getItem(name);
    }

    public setSessionStorage(name: string, value: string) {
        sessionStorage.setItem(name, value);
    }

    public removeSessionStorage(name: string): void {
        sessionStorage.removeItem(name);
    }
}