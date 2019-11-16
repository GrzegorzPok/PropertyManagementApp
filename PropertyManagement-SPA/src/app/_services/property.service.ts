import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Property } from '../_models/Property';


@Injectable({
  providedIn: 'root'
})
export class PropertyService {
  baseUrl = environment.apiUrl;

constructor(private http: HttpClient) { }

  getProperties(): Observable<Property[]> {
    return this.http.get<Property[]>(this.baseUrl + 'properties');
  }

  getProperty(id): Observable<Property> {
    return this.http.get<Property>(this.baseUrl + 'properties/' + id);
  }

}
