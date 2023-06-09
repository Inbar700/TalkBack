import { Component, OnInit } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  constructor(private jwtHelper: JwtHelperService) { }

  userName:any="";

  ngOnInit(): void {
   this.userName=localStorage.getItem("userName");
  }

  isUserAuthenticated=(): boolean =>{
    const token=localStorage.getItem("jwt");

    if(token && !this.jwtHelper.isTokenExpired(token)){
      return true;
    }

    return false;
  }

  logOut=()=> {
    localStorage.removeItem("jwt");
    localStorage.removeItem("userName");
    localStorage.removeItem("refreshToken");
  }
}
