import { Component } from '@angular/core';
import { PaymentsenseCodingChallengeApiService } from '../services/paymentsense-coding-challenge-api.service';
import { ActivatedRoute } from '@angular/router';

@Component({
    selector: 'app-country-details',
    templateUrl: './country-details.component.html',
    styleUrls: ['./country-details.component.css']
})

export class CountryDetailsComponent { 
    
    public countryDetails;

    constructor(private paymentsenseCodingChallengeApiService: PaymentsenseCodingChallengeApiService,
        private route: ActivatedRoute) {}

    ngOnInit() {
        let name;
        this.route.paramMap.subscribe(params => {              
            name = params.get('name');            
        });

        this.paymentsenseCodingChallengeApiService.getCountryByName(name).subscribe(result => { 
            this.countryDetails = result;
        });
    }

    getKeys(obj){
        return Object.keys(obj)
    }
}