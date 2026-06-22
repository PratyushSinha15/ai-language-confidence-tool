import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-hero',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './hero.component.html',
  styleUrl: './hero.component.css'
})
export class Hero {
  confidenceScore = 85;

  sampleText = 'Hello Bonjour नमस्ते';

  detectedLanguages = [
    { name: 'English', percentage: 40 },
    { name: 'French', percentage: 30 },
    { name: 'Hindi', percentage: 30 }
  ];

  showPreview = true;
}