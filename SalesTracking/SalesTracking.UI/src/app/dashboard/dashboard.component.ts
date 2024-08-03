import { Component } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import { LogoutHandlerService } from 'src/shared/services/global/logout-handler.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent {

  parentTitle = '';
  childTitle = '';
  url = '/dashboard';

  constructor(private router: Router, private logout : LogoutHandlerService) {
    this.router.events.subscribe((event) => {
      if (event instanceof NavigationEnd) {
        this.updatePageTitle(event);
      }
    });

  }

  updatePageTitle(event: any) {
    this.parentTitle = this.titleCaseWord('dashborad');
    this.childTitle = '';
    this.url = '/dashboard';
    const routes = event.url.split('/');
    if (routes.length > 2) {
      this.url = this.url + '/' + routes[2];
      this.parentTitle = this.titleCaseWord(routes[2] === 'home' ? 'dashborad' : routes[2]);
      if (routes.length > 3 && routes[3] !== 'landing') {
        if (routes[3].includes('id')) { this.childTitle = routes[3].includes('isView=true') ? 'View' : 'Edit'; }
        else { this.childTitle = 'Add'; }
      }
    }
  }

  titleCaseWord(word: string) {
    if (!word) return word;
    return word[0].toUpperCase() + word.substr(1).toLowerCase();
  }

  navigate(isDashboard =false){ if(isDashboard) this.router.navigate(['/dashboard']); else this.router.navigate([this.url]); }

  logOut(){ this.logout.logout();}
}

