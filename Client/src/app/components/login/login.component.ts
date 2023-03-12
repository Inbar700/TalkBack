import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthenticatedResponse } from 'src/app/interfaces/authenticationResponse';
import { LoginModel } from 'src/app/interfaces/login';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  invalidLogin: boolean=false;
  credentials: LoginModel = {username:'', password:''};

  constructor(private router: Router, private http: HttpClient) { }

  ngOnInit(): void {
    
  }

  login = ( form: NgForm) => {
    if (form.valid) {
      this.http.post<AuthenticatedResponse>("https://localhost:7299/api/access/login", this.credentials, {
        headers: new HttpHeaders({ "Content-Type": "application/json"})
      }).subscribe({
          next: (response: AuthenticatedResponse) => {
          const token = response.token;
          const refreshToken=response.refreshToken;
          localStorage.setItem("jwt", token); 
          localStorage.setItem("userName", this.credentials.username);
          localStorage.setItem("refreshToken", refreshToken);
          this.invalidLogin = false; 
          this.router.navigate(["/"]);
        },
        error: (err: HttpErrorResponse) => this.invalidLogin = true
      })
    }
  }
}
