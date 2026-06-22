import { Component, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink} from '@angular/router';
import { AuthService } from '../../../core/services/auth.service';
import { LoginRequest } from '../../../models/AuthModel';

@Component({
  selector: 'app-login',
  imports: [CommonModule, FormsModule, RouterLink],
  standalone:true,
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
})
export class Login {
  form:LoginRequest = {
    username:'',
    password:''
  };

  loading = signal(false);
  errorMessage = signal('');

  constructor(
    private authService:AuthService,
    private router:Router
  ){}
  
  login(){
    this.loading.set(true);
    this.errorMessage.set;
    this.authService.login(this.form).subscribe({
      next:(respose)=>{
        console.log(respose);
        this.authService.saveAuth(respose);
        this.router.navigate(['/analysis']);
      },
      error:(err)=>{
        console.error(err);
        this.errorMessage.set('Invalid username or password');
        console.log(this.errorMessage);
        this.loading.set(false);
      }
    });
  }

}
