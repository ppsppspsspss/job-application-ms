import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  currentUser: BehaviorSubject<any> = new BehaviorSubject(null);

  constructor(private http: HttpClient, private router: Router) { this.loadCurrentUser(); }

  baseUrl = "http://localhost:5171/api/auth"

  jwtHelperService = new JwtHelperService();

  signIn(user: any){
    return this.http.post(this.baseUrl + "/sign-in", user, {
      responseType: 'text'
    });
  }

  isSignedIn(){
    return localStorage.getItem("access-token") ? true : false;
  }

  setToken(token: string){
    localStorage.setItem("access-token", token);
    this.loadCurrentUser();
  }

  loadCurrentUser(){
    const token = localStorage.getItem("access-token");
    const userInfo = token != null ? this.jwtHelperService.decodeToken(token) : null;
    const data = userInfo ? {
      userID: userInfo.userID,
      fullname: userInfo.fullname,
      phone: userInfo.phone,
      email: userInfo.email,
      password: userInfo.password,
      role: userInfo.role
    } : null;
    this.currentUser.next(data);
  }

  logOut(){
    localStorage.removeItem("access-token");
    this.router.navigateByUrl('/');
  }

}

