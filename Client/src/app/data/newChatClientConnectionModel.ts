import { Guid } from 'guid-typescript';

export interface NewChatClientConnectionModel {
    ChatClientId: Guid,
    ConnectionId: string,
    Name: string
  };