import { Guid } from 'guid-typescript';

export interface ConnectedClientModel {
  chatClientId: Guid;
  connectionId: string;
  name: string;
}
