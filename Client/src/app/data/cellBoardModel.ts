export enum CellEnum{
    empty= ' ',
    player1= 'X',
    player2= 'O'
}
export interface CellBoardModel{
    id: number,
    value: string,
}