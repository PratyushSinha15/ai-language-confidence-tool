import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router} from '@angular/router';
import { Auth } from '../../../core/services/auth';
import { RegisterRequest } from '../../../models/AuthModel';

@Component({
  selector: 'app-register',
  imports: [FormsModule],
  templateUrl: './register.html',
  styleUrl: './register.css',
})
export class Register {

  form:RegisterRequest = {
    username:'',
    email:'',
    password:'',
    firstName:'',
    lastName:''
  };

  loading = false;

  constructor(
    private authService:Auth,
    private router:Router
  ){}

  register(){
    this.loading = true;
    this.authService.register(this.form).subscribe({
      next:(respose)=>{
        console.log(respose);
        this.router.navigate(['/auth/login']);
      },
      error:(err)=>{
        console.error(err);
        this.loading = false;
      }
    });
  }
}
