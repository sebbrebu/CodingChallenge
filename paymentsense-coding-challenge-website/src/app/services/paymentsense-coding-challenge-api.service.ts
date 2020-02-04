import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class PaymentsenseCodingChallengeApiService {
  constructor(private httpClient: HttpClient) {}

  public getHealth(): Observable<string> {
    return this.httpClient.get('https://localhost:5001/health', { responseType: 'text' });
  }

  public getCountries(): Observable<Object> {
    return this.httpClient.get('https://localhost:5001/PaymentsenseCodingChallenge');
  }

  public getCountryByName(name): Observable<Object> {    
    return this.httpClient.get('https://localhost:5001/PaymentsenseCodingChallenge/' + name);
  }
}
