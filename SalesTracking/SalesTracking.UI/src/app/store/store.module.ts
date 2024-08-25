import { NgModule } from "@angular/core";
import { EffectsModule } from "@ngrx/effects";
import { StoreModule } from "@ngrx/store";
import { authReducer } from "./auth/auth.reducer";
import { AuthEffects } from "./auth/auth.effects";
import { dashboardReducer } from './dashboard/dashboard.reducer'
import { DashboardEffects } from "./dashboard/dashboard.effects";

@NgModule({
    imports: [
        StoreModule.forRoot({
            auth: authReducer,
            dashboard: dashboardReducer
        }),
        EffectsModule.forRoot([AuthEffects, DashboardEffects]),
    ],
})
export class AppStoreModule { }