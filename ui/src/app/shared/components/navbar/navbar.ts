import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { Auth } from '../../../core/services/auth';
import { AuthResponse } from '../../../models/AuthModel';


@Component({
  selector: 'app-navbar',
  imports: [CommonModule, RouterLink, RouterLinkActive],
  templateUrl: './navbar.html',
  styleUrl: './navbar.css',
})
export class Navbar implements OnInit {

  getInitials(name?:string){
    if(!name) return '';
    return name
      .trim()
      .split(/\s+/)
      .slice(0,2)
      .map(word=>word[0].toUpperCase())
      .join('');
  }
  currentUser: AuthResponse | null = null;
  isLoggedIn = false;

  constructor(
    private authService: Auth,
    private router: Router
  ){}

  ngOnInit(){
    this.authService.currentUser$.subscribe(user=>{
      this.currentUser=user;
      this.isLoggedIn=!!user;
    });
  }

  logout(){
    this.authService.logout();
    this.router.navigate(['/auth/login']);
  }
}
