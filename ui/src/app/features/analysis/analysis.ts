import { Component, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { InputPanel } from './components/input-panel/input-panel';
import { ResultPanel } from './components/result-panel/result-panel';
import { Analysis as AnalysisService } from '../../core/services/analysis';
import { DetectResponse } from '../../models/DetectResponse';

@Component({
  selector: 'app-analysis',
  imports: [CommonModule, InputPanel, ResultPanel],
  templateUrl: './analysis.html',
  styleUrl: './analysis.css',
})
export class Analysis {
  inputText = '';

  // result: DetectResponse | null = {
  //   id: 1,
  //   inputText: "Hello Bonjour नमस्ते",
  //   language: "English",
  //   confidence: 85,
  //   languageBreakdown: {
  //     English: 50,
  //     Hindi: 30,
  //     French: 20
  //   },
  //   explanation: "Test response"
  // };
  result: DetectResponse | null = null;
  isLoading = false;

  constructor(
    private analysisService: AnalysisService,
    private cdr: ChangeDetectorRef
  ){}

  analyze(text:string):void{
    if (!text.trim()) {
      return;
    }

    this.isLoading = true;

    this.analysisService.detectLanguage({
      inputText: text
    }).subscribe({
      next:(response)=>{
        this.result = {...response};
        console.log('Language detection result:', this.result);
        this.isLoading=false;
        this.cdr.detectChanges();
      },
      error:(error)=>{
        console.error(
          'Error occurred while detecting language:',
          error
        );
        this.isLoading=false;
      }
    });

}
  
}
