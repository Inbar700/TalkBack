import { Guid } from "guid-typescript";
import { ChatMessageModel } from "../data/chatMessageModel";
import { DisplayChatMesageModel } from "../data/displayChatMesageModel";
import { NewChatClientConnectionModel } from "../data/newChatClientConnectionModel";

export const getClientId =() => {
    return JSON.parse(window.atob(localStorage.getItem('jwt')!.split('.')[1])).userId;
};

export const getUserName=()=> {
    return localStorage.getItem('userName');
}

export const buildNewChatClientConnectionModel = (chatClientId: Guid, signalrHubConnectionId: string, 
    name: string): NewChatClientConnectionModel=> {
    return {
        ChatClientId: chatClientId,
        ConnectionId: signalrHubConnectionId,
        Name: name
    };
};

export const buildChatMessageModel = (hubConnectionId: string, message: string, fromUser: string): ChatMessageModel => {
    return {
        ConnectionId: hubConnectionId,
        Message: message,
        FromUser: fromUser
    };
};

export const buildDisplayChatMessageModel = (date: Date, message: string): DisplayChatMesageModel => {
    return {
        Date: date,
        Message: message
    };
};