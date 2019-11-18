import { Injectable } from '@angular/core';
import { Property } from '../_models/Property';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { PropertyService } from '../_services/property.service';
import { AlertifyService } from '../_services/alertify.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { AuthService } from '../_services/auth.service';

@Injectable()
export class PropertyEditResolver implements Resolve<Property> {
    constructor(private propertyService: PropertyService, private routerL: Router, private alertify: AlertifyService) {}

    resolve(route: ActivatedRouteSnapshot) : Observable<Property> {
        return this.propertyService.getProperty(this.propertyService.selectEmitter.value).pipe(
            catchError(error => {
                this.alertify.error('Problem retrieving your data');
                this.routerL.navigate(['/member']);
                return of(null);
            })
        );
    }
}