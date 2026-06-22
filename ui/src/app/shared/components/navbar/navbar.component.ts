import { CommonModule } from '@angular/common';
import { Component, inject} from '@angular/core';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { AuthService } from '../../../core/services/auth.service';


@Component({
  selector: 'app-navbar',
  imports: [CommonModule, RouterLink, RouterLinkActive],
  standalone:true,
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css',
})
export class Navbar {
  private authService= inject(AuthService);
  private router= inject(Router);

  currentUser= this.authService.currentUser;

  getInitials(name?:string){
    if(!name) return '';
    return name
      .trim()
      .split(/\s+/)
      .slice(0,2)
      .map(word=>word[0].toUpperCase())
      .join('');
  }

  logout(){
    this.authService.logout();
    this.router.navigate(['/auth/login']);
  }
}
