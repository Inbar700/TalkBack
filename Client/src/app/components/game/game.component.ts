import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { CellBoardModel, CellEnum } from 'src/app/data/cellBoardModel';
import { GameService } from 'src/app/services/game.service';

@Component({
  selector: 'app-game',
  templateUrl: './game.component.html',
  styleUrls: ['./game.component.css']
})

export class GameComponent implements OnInit {

  private updateBoardUrl='https://localhost:7014/Game/UpdateBoard';
  board: CellBoardModel[][]=[[]];

  constructor(private gameService: GameService, private http: HttpClient) { }

  ngOnInit(): void {
    this.gameService.getBoard().subscribe((data: CellBoardModel[][]) => { 
      this.board = data.map((row) => row.map((cell) => ({
          id: cell.id, 
          value: String.fromCharCode(parseInt(cell.value)), 
        }))
      ); 
      console.log("board", this.board); 
    }); 
  }

  onCellClick(row: number, col: number){
    this.http.post<CellBoardModel[][]>(this.updateBoardUrl, {row, col}).subscribe((board) => {
      this.board=board;
    })
  };

}
