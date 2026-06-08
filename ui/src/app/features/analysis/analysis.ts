import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { InputPanel } from './components/input-panel/input-panel';
import { ResultPanel } from './components/result-panel/result-panel';

@Component({
  selector: 'app-analysis',
  imports: [CommonModule, InputPanel, ResultPanel],
  templateUrl: './analysis.html',
  styleUrl: './analysis.css',
})
export class Analysis {}
