import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { JwtModule } from '@auth0/angular-jwt';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FormsModule } from '@angular/forms';
import { LoginComponent } from './components/login/login.component';
import { HomeComponent } from './components/home/home.component';
import { UsersComponent } from './components/users/users.component';
import { AuthGuard } from './services/auth.guard';
import { RegisterComponent } from './components/register/register.component';


import { GameComponent } from './components/game/game.component';


export function tokenGetter() {
  return localStorage.getItem('jwt');
}

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    HomeComponent,
    UsersComponent,
    RegisterComponent,
    GameComponent,
    
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        allowedDomains: ['localhost:7299'],
        disallowedRoutes: [],
      },
    }),
  ],
  providers: [AuthGuard], //providers: [AuthGuard],
  bootstrap: [AppComponent],
})
export class AppModule {}
