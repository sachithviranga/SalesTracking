import { Injectable } from '@angular/core';
import { PagerSettings } from '@progress/kendo-angular-grid';
import { State } from '@progress/kendo-data-query';

@Injectable()
export class PaginationConfigService {

    public paginationConfig: PagerSettings = { pageSizes: [5, 50, 100] };

    public state: State = {
        filter: {
            logic: 'and',
            filters: []
        },
        skip: 0,
        take: 10
    };

    public updatePagination(): PagerSettings {
        if (window.innerWidth >= 1280) {
            this.paginationConfig.buttonCount = 15;
        }
        if (window.innerWidth < 1280) {
            this.paginationConfig.buttonCount = 7;
        }
        if (window.innerWidth < 960) {
            this.paginationConfig.buttonCount = 3;
            this.paginationConfig.type = "numeric";
            this.paginationConfig.info = true;
            this.paginationConfig.previousNext = true;
        }
        if (window.innerWidth < 800) {
            this.paginationConfig.info = false;
        }
        if (window.innerWidth < 600) {
            this.paginationConfig.buttonCount = 2;
            this.paginationConfig.type = "input";
            this.paginationConfig.previousNext = false;
        }

        return this.paginationConfig;
    }
}
