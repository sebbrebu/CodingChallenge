import { Component } from '@angular/core';
import { PaymentsenseCodingChallengeApiService } from '../services/paymentsense-coding-challenge-api.service';

@Component({
    selector: 'app-country-list',
    templateUrl: './country-list.component.html',
    styleUrls: ['./country-list.component.css']
})

export class CountryListComponent { 
    p: number = 1;
    public countries;

    constructor(private paymentsenseCodingChallengeApiService: PaymentsenseCodingChallengeApiService) {        
        this.paymentsenseCodingChallengeApiService.getCountries().subscribe(result => { 
            this.countries = result;
        });
    }    
}