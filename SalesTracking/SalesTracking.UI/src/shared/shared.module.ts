import { NgModule } from "@angular/core";
import { CommonModule } from '@angular/common';
import { MaterialModule } from "./material/material.module";
import { KendoModule } from "./kendo/kenod.module";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { HttpClientModule } from "@angular/common/http";
import { FlexLayoutModule } from '@ngbracket/ngx-layout';
import { KpiComponent } from "./common-component/kpi-card-component/kpi-card.component";
import { BarChartComponent } from "./common-component/bar-chart-component/bar-chart.component";
import { NgChartsModule } from "ng2-charts";

const sharedModules: any[] = [
    MaterialModule,
    CommonModule,
    KendoModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    FlexLayoutModule,
    NgChartsModule
];

@NgModule({
    imports: [sharedModules],
    declarations: [KpiComponent , BarChartComponent],
    exports: [
        ...sharedModules, KpiComponent , BarChartComponent
    ]
})
export class SharedModule { }