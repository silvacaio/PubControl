import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './user/login/login.component';
import { CreateComponent } from './tab/create/create.component';
import { ManagerComponent } from './tab/manager/manager.component';
import { RegisterComponent } from './user/register/register.component';
import { AuthService } from './services/auth.service';
import { CloseComponent } from './tab/close/close.component';

const routes: Routes = [
  { path: '', component: LoginComponent },
  { path: 'login',  component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'create', canActivate: [AuthService], component: CreateComponent },
  { path: 'manager/:id', canActivate: [AuthService], component: ManagerComponent },
  { path: 'close/:id', canActivate: [AuthService], component: CloseComponent },
];

@NgModule({  
  imports:
    [    
      RouterModule.forRoot(routes),      
    ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
