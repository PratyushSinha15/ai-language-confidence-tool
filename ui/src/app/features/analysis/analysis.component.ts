import { Component, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { InputPanel } from './components/input-panel/input-panel.component';
import { ResultPanel } from './components/result-panel/result-panel.component';
import { Analysis as AnalysisService } from '../../core/services/analysis.service';
import { DetectResponse } from '../../models/DetectResponse';

@Component({
  selector: 'app-analysis',
  imports: [CommonModule, InputPanel, ResultPanel],
  standalone:true,
  templateUrl: './analysis.component.html',
  styleUrl: './analysis.component.css',
})
export class Analysis {
  inputText = '';

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

  clear(){
    this.inputText='';
    this.result=null;
    this.isLoading=false;
  }
}
