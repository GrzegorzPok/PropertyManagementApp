import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Property } from '../_models/Property';
import * as Rx from 'rxjs';
import { Message } from '../_models/message';


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

  getMyProperties(userId: number): Observable<Property[]> {
    return this.http.get<Property[]>(this.baseUrl + 'properties/' + userId + '/my');
  }
  
  getRentedProperties(userId: number): Observable<Property[]> {
    return this.http.get<Property[]>(this.baseUrl + 'properties/' + userId + '/rented');
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

  setMainPhoto(propertyId: any, id: number) {
    return this.http.post(this.baseUrl + 'properties/' + propertyId + '/photos/' + id + '/setMain', {});
  }

  deletePhoto(propertyId: any, id: number) {
    return this.http.delete(this.baseUrl + 'properties/' + propertyId + '/photos/' + id);
  }

  sendRent(id: number, userId: any) {
    return this.http.post(this.baseUrl + 'properties/' + id + '/rent/' + userId, {});
  }

  getMessages(id: number, isInbox: boolean) {
    if (isInbox) {
      return this.http.get<Message[]>(this.baseUrl + 'messages/users/' + id + '/sent');
    }
    else {
      return this.http.get<Message[]>(this.baseUrl + 'messages/users/' + id + '/received');
    }
  }

  getUnreadMessages(id: number) {
    return this.http.get<Message[]>(this.baseUrl + 'messages/users/' + id + '/unread');
  }

  getMessageThread(propertyid: any) {
    return this.http.get<Message[]>(this.baseUrl + 'messages/thread/' + propertyid);
  }

  sendMessage(id: any, message: Message) {
    return this.http.post(this.baseUrl + 'messages/' + id, message);
  }

  deleteMessage(Id: number) {
    return this.http.post(this.baseUrl + 'messages/' + Id + '/delete', {});
  }
}
