import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  constructor(private authService: AuthService) {
  }

  ngOnInit() {
    if (!this.authService.hasAccount)
      this.authService.login();
    else
      this.authService.redirectByRole();
  }

  onLoginClick() {
    this.authService.login();
  }
}
