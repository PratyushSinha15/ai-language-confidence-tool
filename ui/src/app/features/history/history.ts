import { Component, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HistoryItem } from '../../models/HistoryModel';
import { Analysis } from '../../core/services/analysis';

@Component({
  selector: 'app-history',
  imports: [ CommonModule],
  templateUrl: './history.html',
  styleUrl: './history.css',
})
export class History {

  history:HistoryItem[]=[];
  isLoading= false;

  errorMessage= '';
  constructor(
    private analysisService:Analysis,
    private cdr: ChangeDetectorRef
  ){}

  ngOnInit(): void {
    this.loadHistory();
  }

  loadHistory():void {
    this.isLoading=true;

    this.analysisService.getHistory().subscribe({
      next:(response)=>{
        this.history=response;
        this.isLoading=false;
        this.cdr.detectChanges();
      },
      error: (error) =>{
        console.error('Error while loading History: ',error);
        this.errorMessage='Unable to load history';
        this.isLoading=false;
      }
    });
  }

}
