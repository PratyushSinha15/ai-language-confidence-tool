import { Component, Input } from '@angular/core';
import { DetectResponse } from '../../../../models/DetectResponse';

interface LanguageItem {
  name: string;
  percentage: number;
  color: string;
}

@Component({
  selector: 'app-result-panel',
  imports: [],
  templateUrl: './result-panel.html',
  styleUrl: './result-panel.css',
})
export class ResultPanel {
  @Input() 
  result: DetectResponse | null = null;

  colors: {[key:string]: string} = {
    English: '#5B4AD3',
    Hindi: '#22C55E',
    French: '#A855F7',
    Spanish: '#F59E0B',
    German: '#EF4444',
    Japanese: '#EC4899',
    default: '#64748B'
  };

  get breakdown(): LanguageItem[] {

    if(!this.result?.languageBreakdown){
      return [];
    }


    return Object.entries(
      this.result.languageBreakdown
    ).map(([language, percentage]) => {
      return {
        name: language,
        percentage: Number(percentage),
        color:
          this.colors[language] ??
          this.colors['default']
      };
      
    });

  }

}
