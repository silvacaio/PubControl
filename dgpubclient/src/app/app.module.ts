import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './user/login/login.component';
import { MenuLoginComponent } from './shared/menu-login/menu-login.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { UserService } from './services/user.service';
import { ErrorInterceptor } from './services/error.handler.service';
import { CreateComponent } from './tab/create/create.component';
import { ManagerComponent } from './tab/manager/manager.component';
import { ItemService } from './tab/services/item.service';
import { TabService } from './tab/services/tab.service';
import { CloseComponent } from './tab/close/close.component';
import { RegisterComponent } from './user/register/register.component';
import { AuthService } from './services/auth.service';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    MenuLoginComponent,
    CreateComponent,    
    ManagerComponent, CloseComponent, RegisterComponent,    
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,  
  ],
  providers: [
    UserService,
    ItemService,
    TabService,
    AuthService,
    {
        provide: HTTP_INTERCEPTORS,
        useClass: ErrorInterceptor,
        multi: true
      }    
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
