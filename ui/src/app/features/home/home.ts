import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Hero } from '../../shared/components/hero/hero';
import { Footer } from '../../shared/components/footer/footer';


@Component({
  selector: 'app-home',
  imports: [CommonModule,  Hero],
  templateUrl: './home.html',
  styleUrl: './home.css',
})
export class Home {}
