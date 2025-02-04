import { Component, OnInit } from '@angular/core';
import { Router } from "@angular/router";

@Component({
  selector: 'app-menu-login',
  templateUrl: './menu-login.component.html',
  styleUrls: ['./menu-login.component.css']
})

export class MenuLoginComponent implements OnInit {
  public token;
  public user;
  public nome: string = "";

  constructor(private router: Router) {
    this.token = localStorage.getItem('dgpub.token');
    this.user = JSON.parse(localStorage.getItem('dgpub.user'));
   }
  

  ngOnInit() {
    if(this.user)
        this.nome = this.user.nome;
  }

  usuarioLogado(): boolean {
    return this.token !== null;
  }

  logout() {
    localStorage.removeItem('dgpub.token');
    localStorage.removeItem('dgpub.user');
    this.router.navigateByUrl('/');
  }
}

