import { Component, Input, OnChanges, SimpleChanges } from '@angular/core';
import { DetectResponse } from '../../../../models/DetectResponse';
import { CommonModule } from '@angular/common';

interface LanguageItem {
  name: string;
  percentage: number;
  color: string;
}

@Component({
  selector: 'app-result-panel',
  standalone:true,
  imports: [CommonModule],
  templateUrl: './result-panel.component.html',
  styleUrl: './result-panel.component.css',
})
export class ResultPanel implements OnChanges {
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

  breakdown: LanguageItem[] = [];

  ngOnChanges(changes: SimpleChanges){
    if(changes['result']){
      console.log(
        "ResultPanel received:",
        changes['result'].currentValue
      );
      this.result = changes['result'].currentValue;

      if(this.result?.languageBreakdown){
        this.breakdown = Object.entries(
          this.result.languageBreakdown
        ).map(([language, percentage])=>({
          name:language,
          percentage:Number(percentage),
          color:
          this.colors[language] ??
          this.colors['default']
        }));
      }
    }
  }

}
