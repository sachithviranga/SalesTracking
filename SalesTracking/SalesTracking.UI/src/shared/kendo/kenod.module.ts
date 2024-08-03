import { NgModule } from "@angular/core";
import { GridModule , ExcelModule} from '@progress/kendo-angular-grid';
import { IntlModule } from '@progress/kendo-angular-intl';

const kendoModules: any[] = [
    GridModule,
    ExcelModule,
    IntlModule
];

@NgModule({
    imports: kendoModules,
    exports: kendoModules
})
export class KendoModule { }