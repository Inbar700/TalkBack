import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { ConnectedClientModel } from '../data/connectedClientModel';
import { NewChatClientConnectionModel } from '../data/newChatClientConnectionModel';
import { buildNewChatClientConnectionModel } from '../utils/utils';

@Injectable({
  providedIn: 'root',
})
export class UsersService {
  private getConnectedUsersUrl =
    'https://localhost:7014/Contacts/GetConnectedUsers';

  constructor(private http: HttpClient) {}

  getConnectedUsers(): Observable<ConnectedClientModel[]> {
    return this.http.get<ConnectedClientModel[]>(this.getConnectedUsersUrl);
  }

}
