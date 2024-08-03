import { Component, OnInit, Input } from '@angular/core';
import { formatDate, registerLocaleData } from '@angular/common'
import localeEn from '@angular/common/locales/en';

registerLocaleData(localeEn);

@Component({
    selector: 'app-bar-chart',
    templateUrl: './bar-chart.component.html',
    styleUrls: ['./bar-chart.component.scss']
})

export class BarChartComponent implements OnInit {

    @Input() barChartLabels: any[] = [];
    @Input() barChartData: any[] = [];

    constructor() { }

    public barChartOptions = {
        responsive: true,
        maintainAspectRatio: false,
        legend: { display: false },
        cornerRadius: 50,
        tooltips: {
            enabled: true,
            mode: 'index',
            intersect: false,
            borderWidth: 1,
            borderColor: '#eeeeee',
            backgroundColor: '#ffffff',
            titleFontColor: '#43436B',
            bodyFontColor: '#A1A1B5',
            footerFontColor: '#A1A1B5',
        },
        layout: { padding: 0 },
        scales: {
            xAxes: [
                {
                    barThickness: 12,
                    maxBarThickness: 10,
                    barPercentage: 0.5,
                    categoryPercentage: 0.5,
                    ticks: {
                        fontColor: '#A1A1B5',
                    },
                    gridLines: {
                        display: false,
                        drawBorder: false,
                    },
                },
            ],
            yAxes: [
                {
                    ticks: {
                        fontColor: '#A1A1B5',
                        beginAtZero: true,
                        min: 0,
                    },
                    gridLines: {
                        borderDash: [2],
                        borderDashOffset: [2],
                        color: '#eeeeee',
                        drawBorder: false,
                        zeroLineBorderDash: [2],
                        zeroLineBorderDashOffset: [2],
                        zeroLineColor: '#eeeeee',
                    },
                },
            ],
        },
    };
    public barChartType = 'bar';
    public barChartLegend = true;
    COLORS_SERIES = ['#6200EE', '#3700B3', '#FFA2BE'];

    ngOnInit() {
        // this.barChartLabels = this.chartData.data.map((r) => r.product);
        // this.barChartData = [{
        //     //label: 'Available ',
        //     data: data.map((r) => r.value),
        //     backgroundColor: this.COLORS_SERIES[0],
        //     fill: false,
        // }];
    
    }
}