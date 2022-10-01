import { Injectable } from '@angular/core';
import {  HttpClient } from '@angular/common/http';
//import { environment } from 'src/environments/environment';
import { IClient } from '../Interfaces/client';

@Injectable({
  providedIn: 'root'
})
export class BasicService {

  private http: HttpClient;
  private BaseUrl:string;
  constructor(private _http: HttpClient) {
     // eslint-disable-next-line no-underscore-dangle
    this.http=_http;
    this.BaseUrl='https://localhost:44382/api/queries'
  }

  //#region client
  getAllClients()
  {
  return this.http.get(this.BaseUrl + '/AllClients');
  }
  insertClient(_new: IClient){
    return this.http.post(this.BaseUrl + '/CreateClient',_new);
  }
  getClient(id){
    return this.http.get(this.BaseUrl + '/GetClient?id='+ id);
  }
  updateClient(obj: IClient){
    return this.http.post(this.BaseUrl + '/UpdateClient',obj);
  }
  deleteClient(id){
    return this.http.post(this.BaseUrl + '/DeleteClient',{Id:id});
  }
  //#endregion


}
