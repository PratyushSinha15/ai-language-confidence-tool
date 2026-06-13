import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { DetectRequest } from '../../models/DetectRequest';
import { DetectResponse } from '../../models/DetectResponse';
import { Observable } from 'rxjs/internal/Observable';

@Injectable({
  providedIn: 'root',
})
export class Analysis {
  private apiUrl = 'http://localhost:5292/api/language';

  constructor(private http: HttpClient) {}

  detectLanguage(request: DetectRequest): Observable<DetectResponse>{
    return this.http.post<DetectResponse>(
      `${this.apiUrl}/detect`,
      request
    );
  }
}
