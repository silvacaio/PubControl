import { Component, OnInit } from '@angular/core';
import { ReactiveFormsModule, FormGroup, FormBuilder, Validators, FormControlName } from '@angular/forms';
import { User } from '../models/user';
import { UserService } from '../../services/user.service'
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  public loginForm: FormGroup;
  user: User;
  public msgs: any[] = [];

  constructor(private fb: FormBuilder,
    private userService: UserService,
    private router: Router) {
    this.user = new User();
  }

  ngOnInit(): void {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]]
    });
  }

  login() {
    if (this.loginForm.dirty && this.loginForm.valid) {
      let p = Object.assign({}, this.user, this.loginForm.value);

      this.userService.login(p)
        .subscribe(
          result => { this.onSaveComplete(result) },
          fail => { this.onError(fail) }
        );
    }
  }

  
  onSaveComplete(response: any): void {
    this.loginForm.reset();
    this.msgs = [];

    localStorage.setItem('dgpub.token', response.access_token);
    localStorage.setItem('dgpub.user', JSON.stringify(response.user));

    this.router.navigate(['/create']);
  }

  onError(fail: any) {    
    this.msgs = fail.error.errors;
  }

}
