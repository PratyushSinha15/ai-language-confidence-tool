import { Injectable,signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import {LoginRequest,RegisterRequest,AuthResponse} from '../../models/AuthModel'; 

@Injectable({
  providedIn: 'root',
})

export class AuthService {
  private apiUrl = 'http://localhost:5292/api/Auth';

currentUser = signal<AuthResponse | null>(
  this.getUser()
);
  // currentUser$ = this.currentUserSubject.asObservable();

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
    this.currentUser.set(response);
  }

  logout(){
    localStorage.removeItem('token');
    localStorage.removeItem('user');
    this.currentUser.set(null);
  }
  isLoggedIn(){
    return !!localStorage.getItem('token');
  }

  getToken(){
    return localStorage.getItem('token');
  }

  private getUser(){
    const user =localStorage.getItem('user');
    return user ? JSON.parse(user) as AuthResponse : null;
  }
}
