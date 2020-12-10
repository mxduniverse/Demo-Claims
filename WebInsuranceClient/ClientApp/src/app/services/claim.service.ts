import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment } from '@environments/environment';
import { User } from '@app/_models';
import { AuthenticationService } from '@app/_services';

const baseUrl = `${environment.apiUrl}/claims`;


@Injectable({
  providedIn: 'root'
})
export class ClaimService {

  user: any;
  constructor(private http: HttpClient, private accountService: AuthenticationService) {
    this.user = accountService.currentUserValue;
  }

  getAll() {

    var userid = this.user.userId;
    return this.http.get(`${baseUrl}/?userid=${userid}`);
  }

  get(id) {
    return this.http.get(`${baseUrl}/${id}`);
  }

  create(data) {
    return this.http.post(baseUrl, data);
  }

  update(id, data) {
    return this.http.put(`${baseUrl}/${id}`, data);
  }

  delete(id) {
    return this.http.delete(`${baseUrl}/${id}`);
  }

  deleteAll() {
    return this.http.delete(baseUrl);
  }

  findByTitle(Description) {
    var userid = this.user.userId;
    return this.http.get(`${baseUrl}?userid=${userid}&description=${Description}`);
  }
}
