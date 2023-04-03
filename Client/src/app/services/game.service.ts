import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { CellBoardModel } from '../data/cellBoardModel';

@Injectable({
  providedIn: 'root'
})
export class GameService {
  private getBoardUrl ='https://localhost:7014/Game/GetBoard';

  constructor(private http: HttpClient) { }

  //CellBoardModel[][]

  getBoard(): Observable<any> {
    return this.http.get<any>(this.getBoardUrl);
    //.pipe(map((res: any)=> Object.values(res)));
  }

}
