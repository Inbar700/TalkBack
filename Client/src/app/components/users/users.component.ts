import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { SignalrService } from 'src/app/services/signalr.service';
import {
  buildNewChatClientConnectionModel,
  getClientId,
  getUserName,
} from 'src/app/utils/utils';
import { firstValueFrom, lastValueFrom } from 'rxjs';
import { UsersService } from 'src/app/services/users.service';
import { ConnectedClientModel } from 'src/app/data/connectedClientModel';
import { NewChatClientConnectionModel } from 'src/app/data/newChatClientConnectionModel';
import { Router } from '@angular/router';


@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css'],
})
export class UsersComponent implements OnInit {
  private signalrConnectionUrl = 'https://localhost:7014/userHub';
  private addClientUrl = 'https://localhost:7014/Contacts/AddChatClient';
  private sendMessageToUserUrl = 'https://localhost:7014/Contacts/SendMessageToUser';
  private invitePlayerUrl='https://localhost:7014/Contacts/InvitePlayer';

  chatClientId = getClientId();
  chatMessage: string = '';
  inviteMessage: string='';
  userName = getUserName();
  users: ConnectedClientModel[] = [];
  sendToUser: any='';
  fromUser: any='';

  constructor(
    public signalRService: SignalrService,
    private userService: UsersService,
    private http: HttpClient,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.startSignalrConnection();
  }

  startSignalrConnection(): void {
    this.signalRService
      .startSignalrConnection(this.signalrConnectionUrl)
      .then((signalrHubConnectionId) => {
        firstValueFrom(
          this.http.post(
            this.addClientUrl,
            buildNewChatClientConnectionModel(
              this.chatClientId,
              signalrHubConnectionId,
              this.userName!
            )
          )
        )
          .then((response) => {
            this.userService.getConnectedUsers().subscribe((users) => {
              this.users = users;
              this.signalRService.addListenersMsg();
              this.signalRService.addListenerInvitation();
            });
            console.log(
              'Signalr started successfully with connectionId: ' +
                signalrHubConnectionId +
                ' And ready to get messages'
            );
          })
          .catch((error) => {
            console.log('Error while adding new chat client:', error);
            alert('Error while adding new chat client:' + error);
            console.log('chatClientId: ' + this.chatClientId);
            console.log('signalrHubConnectionId: ' + signalrHubConnectionId);
          });
      })
      .catch((error) => {
        console.log('Error while establishing signalr connection:', error);
        alert('Error while establishing signalr connection:' + error);
      });
  }

  sendMessageToUser(sendToUser: any): void {
    this.sendToUser=sendToUser;
    this.fromUser=this.signalRService.getName();
    console.log("fromUser: ", this.fromUser);
    console.log("check sendToUser: ", this.sendToUser);
    this.signalRService.sendMessageToUser(
      this.sendMessageToUserUrl,
      this.chatMessage,
      this.sendToUser,
      this.fromUser
    );
  }

  invitePlayer(sendToUser: any): void{
    this.sendToUser=sendToUser;
    this.fromUser=this.signalRService.getName();
    this.inviteMessage=(`${this.fromUser} is inviting you to play`) ;
    console.log("fromUser: ", this.fromUser);
    console.log("check sendToUser: ", this.sendToUser);
    this.signalRService.invitePlayer(
      this.invitePlayerUrl,
      this.inviteMessage,
      this.sendToUser,
      this.fromUser
    );
    this.invitePlayerConfirmation();
  }

  invitePlayerConfirmation(): void{
    this.router.navigateByUrl("/game");
     }
}
