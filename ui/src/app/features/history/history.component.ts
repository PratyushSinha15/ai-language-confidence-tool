import { Component, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HistoryItem } from '../../models/HistoryModel';
import { Analysis } from '../../core/services/analysis.service';

@Component({
  selector: 'app-history',
  imports: [ CommonModule],
  standalone:true,
  templateUrl: './history.component.html',
  styleUrl: './history.component.css',
})
export class History {

  history= signal<HistoryItem[]>([]);
  isLoading= signal(false);

  errorMessage= signal('');
  constructor(
    private analysisService:Analysis
    // private cdr: ChangeDetectorRef
  ){}

  ngOnInit(): void {
    this.loadHistory();
  }

  loadHistory():void {
    this.isLoading.set(true);

    this.analysisService.getHistory().subscribe({
      next:(response)=>{
        this.history.set(response);
        this.isLoading.set(false);
        // this.cdr.detectChanges();
      },
      error: (error) =>{
        console.error('Error while loading History: ',error);
        this.errorMessage.set('Unable to load history');
        this.isLoading.set(false);
      }
    });
  }

}
