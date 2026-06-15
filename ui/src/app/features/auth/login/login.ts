import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink} from '@angular/router';
import { Auth } from '../../../core/services/auth';
import { LoginRequest } from '../../../models/AuthModel';

@Component({
  selector: 'app-login',
  imports: [FormsModule, RouterLink],
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class Login {
  form:LoginRequest = {
    username:'',
    password:''
  };

  loading = false;
  errorMessage = '';

  constructor(
    private authService:Auth,
    private router:Router
  ){}
  
  login(){
    this.loading = true;
    this.authService.login(this.form).subscribe({
      next:(respose)=>{
        console.log(respose);
        this.authService.saveAuth(respose);
        this.router.navigate(['/analysis']);
      },
      error:(err)=>{
        console.error(err);
        this.errorMessage = 'Invalid username or password';
        this.loading = false;
      },
      complete:()=>{
        this.loading = false;
      }
    });
  }

}
