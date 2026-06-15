import { Component, EventEmitter, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-input-panel',
  standalone:true,
  imports: [FormsModule],
  templateUrl: './input-panel.html',
  styleUrl: './input-panel.css',
})
export class InputPanel {
  text="";
  @Output()
  analyzeText = new EventEmitter<string>();
  analyze(){
    this.analyzeText.emit(this.text);
  }
  
}
