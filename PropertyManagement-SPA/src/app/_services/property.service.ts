import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Property } from '../_models/Property';
import * as Rx from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class PropertyService {
  baseUrl = environment.apiUrl;
  selectEmitter = new Rx.BehaviorSubject<Number>(0);

constructor(private http: HttpClient) { }

  getProperties(): Observable<Property[]> {
    return this.http.get<Property[]>(this.baseUrl + 'properties');
  }

  getProperty(id): Observable<Property> {
    this.selectEmitter.next(id);
    if (id != 0) {
      return this.http.get<Property>(this.baseUrl + 'properties/' + id);
    }
    else {
      return this.http.get<Property>(this.baseUrl + 'properties/empty');
    }
  }

  updateProperty(id: number, property: Property) {
    return this.http.put(this.baseUrl + 'properties/' + id, property);
  }
}
