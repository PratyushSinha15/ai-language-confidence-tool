import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import {LoginRequest,RegisterRequest,AuthResponse} from '../../models/AuthModel'; 

@Injectable({
  providedIn: 'root',
})

export class Auth {
  private apiUrl = 'http://localhost:5292/api/Auth';

  private currentUserSubject =new BehaviorSubject<AuthResponse | null>(this.getUser());
  currentUser$ = this.currentUserSubject.asObservable();

  constructor(
    private http:HttpClient
  ){}

  register(data:RegisterRequest):Observable<AuthResponse>{
    return this.http.post<AuthResponse>(
      `${this.apiUrl}/register`,
      data
    );
  }

  login(data:LoginRequest):Observable<AuthResponse>{
    return this.http.post<AuthResponse>(
      `${this.apiUrl}/login`,
      data
    );
  }

  saveAuth(response:AuthResponse){
    localStorage.setItem(
      'token',
      response.token
    );

    localStorage.setItem(
      'user',
      JSON.stringify(response)
    );
    this.currentUserSubject.next(response);
  }

  logout(){
    localStorage.removeItem('token');
    localStorage.removeItem('user');
    this.currentUserSubject.next(null);
  }
  isLoggedIn(){
    return !!localStorage.getItem('token');
  }

  getToken(){
    return localStorage.getItem('token');
  }

  private getUser(){
    const user =localStorage.getItem('user');
    return user ? JSON.parse(user) : null;
  }
}
