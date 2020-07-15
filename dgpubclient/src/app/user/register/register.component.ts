import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { User } from '../models/user';
import { UserService } from 'src/app/services/user.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  public registerForm: FormGroup;
  user: User;
  public msgs: any[] = [];

  constructor(private fb: FormBuilder,
    private userService: UserService,
    private router: Router) {
    this.user = new User();
  }

  ngOnInit(): void {
    this.registerForm = this.fb.group({
      name: ['', [Validators.required]],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      confirmPassword: ['', [Validators.required, Validators.minLength(6)]],
    });
  }

  register() {
    if (this.registerForm.dirty && this.registerForm.valid) {
      let p = Object.assign({}, this.user, this.registerForm.value);

      this.userService.register(p)
        .subscribe(
          result => { this.onSaveComplete(result) },
          fail => { this.onError(fail) }
        );
    }
  }

  
  onSaveComplete(response: any): void {
    this.registerForm.reset();
    this.msgs = [];

    localStorage.setItem('dgpub.token', response.access_token);
    localStorage.setItem('dgpub.user', JSON.stringify(response.user));

    this.router.navigate(['/create']);
  }

  onError(fail: any) {    
    this.msgs = fail.error.errors;
  }

}
