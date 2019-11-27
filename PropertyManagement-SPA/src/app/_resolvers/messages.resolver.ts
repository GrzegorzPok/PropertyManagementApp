import { Injectable } from '@angular/core';
import { Property } from '../_models/Property';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { PropertyService } from '../_services/property.service';
import { AlertifyService } from '../_services/alertify.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Message } from '../_models/message';
import { AuthService } from '../_services/auth.service';

@Injectable()
export class MessagesResolver implements Resolve<Message[]> {
    constructor(private propertyService: PropertyService, private routerL: Router, 
        private alertify: AlertifyService, private authService: AuthService) {}

    resolve(route: ActivatedRouteSnapshot): Observable<Message[]> {
        console.log('dasdasd');
        return this.propertyService.getMessages(this.authService.decodedToken.nameid, true).pipe(
            catchError(error => {
                this.alertify.error('Problem retrieving messages');
                this.routerL.navigate(['/home']);
                return of(null);
            })
        );
    }
}