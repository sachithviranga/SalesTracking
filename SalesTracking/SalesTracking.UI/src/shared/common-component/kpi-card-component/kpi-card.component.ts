import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-common-kpi-component',
  templateUrl: './kpi-card.component.html',
  styleUrls: ['./kpi-card.component.scss']
})

export class KpiComponent {

    @Input() query: object;
    @Input() title: string;
    @Input() duration: number;
    @Input() progress: boolean;
    @Input() difference: string;    

    public result = 0;
    public postfix = null;
    public prefix = null;
    public diffValue = null;
  
}
