import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Hero } from '../../shared/components/hero/hero.component';



@Component({
  selector: 'app-home',
  imports: [CommonModule,  Hero],
  standalone:true,
  templateUrl: './home.component.html',
  styleUrl: './home.component.css',
})
export class Home {}
