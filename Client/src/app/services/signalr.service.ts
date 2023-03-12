import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { HttpTransportType, HubConnection, HubConnectionBuilder, LogLevel } from '@microsoft/signalr';
import { buildChatMessageModel, buildDisplayChatMessageModel } from '../utils/utils';
import { DisplayChatMesageModel } from '../data/displayChatMesageModel';
import { firstValueFrom, lastValueFrom } from 'rxjs';
import { Guid } from 'guid-typescript';
import { NewChatClientConnectionModel } from '../data/newChatClientConnectionModel';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class SignalrService {
  public hubConnection!: HubConnection;
  public messages: DisplayChatMesageModel[]= [];
  public clientsList: NewChatClientConnectionModel[]=[];

  public fromUser: string="";
  public inviteMsg: string="";

  constructor(private http: HttpClient,private router: Router) { }

  public startSignalrConnection(connectionUrl: any) {
    return new Promise<any>((resolve, reject) => {
      this.hubConnection = new HubConnectionBuilder()
        .withUrl(connectionUrl, { 
          withCredentials: false,
        accessTokenFactory: () => localStorage.getItem('jwt')!,
       })
        .configureLogging(LogLevel.Debug)
        .build();

      this.hubConnection.start()
        .then(() => {
          console.log('in');
          resolve(this.hubConnection.connectionId);
        })

        .then(()=>this.getSenderName())

        .catch((error) => {
          reject(error);
        });

        this.hubConnection.onclose((error) => {
          console.log("hubConnection.onclose()", error);
        });
  
        this.hubConnection.onreconnecting((error) => {
          console.log("hubConnection.onreconnecting()", error);
        });
  
        this.hubConnection.onreconnected((error) => {
          console.log("hubConnection.onreconnected()", error);
        });
    })
  }

  public addListenersMsg() {
    this.hubConnection.on("GetMessage", (message: string, sendToUser: string) => {
      this.messages=[...this.messages,buildDisplayChatMessageModel(new Date(), message)];
      console.log("messages array: ", this.messages);
    });
  }

  public addListenerInvitation(){
    this.hubConnection.on("InvitePlayer", (message: string, sendToUser: string)=> {
      this.inviteMsg=message;
      console.log("invitation message: ", this.inviteMsg);
      window.alert(this.inviteMsg);
      setTimeout(()=>{
        this.router.navigateByUrl("/game");
      },5000);
    })
  }

  public getSenderName=()=>{
    this.hubConnection.invoke('getSenderName')
      .then((data)=>{
        console.log("this is the data: ", data);
        this.fromUser=data;
      })
  }

  public getName(): string{
    return this.fromUser;
  }
 
  public sendMessageToUser(sendMessageToUserUrl: string, message: string, sendToConnId: any, fromUser:string){
    firstValueFrom(this.http.post(sendMessageToUserUrl, buildChatMessageModel(sendToConnId, message, fromUser)))
      .then((response) => {
        console.log(response);
      })
      .catch((error) => {
        console.log("Failed to send message: ", error);
        alert("Failed to send message: "+ error);
      })   
  }

  public invitePlayer(invitePlayerUrl: string, message: string, sendToUser: any, fromUser:string){
    firstValueFrom(this.http.post(invitePlayerUrl,buildChatMessageModel(sendToUser, message, fromUser)))
      .then((response) =>{
        console.log(response);
      })
      .catch((error) =>{
        console.log("Failed to send message: ", error);
        alert("Failed to send message: "+ error);
      })
  }
}
