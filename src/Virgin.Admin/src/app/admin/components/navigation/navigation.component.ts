import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/core/services/auth.service';
import { Constants } from 'src/app/constants';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.scss']
})
export class NavigationComponent implements OnInit {

  Routes = Constants.Routes;

  constructor(private authService: AuthService) { }

  ngOnInit() {
  }

  onLogoutClick() {
    this.authService.logout();
  }
}
