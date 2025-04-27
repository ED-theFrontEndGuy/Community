import * as helpers from "./helpers";
import { GameBrain } from "./brain";


export function drawBoard(game: GameBrain): HTMLElement {
    const board = document.getElementById("app") as HTMLElement;
    const playerDiv = document.getElementById("current-player") as HTMLElement;
    playerDiv.innerHTML = `Turn: ${game.currentPlayer.symbol}`;

    for (let x = 0; x < 5; x++) {
        let row = document.createElement("div");
        row.classList.add("row");

        for (let y=0; y < 5; y++) {
            let cell = document.createElement("div");
            cell.classList.add("cell");
            cell.innerHTML = game.board[x][y] || "";

            if (game.board[x][y] !== null) {
                cell.addEventListener("click", (e: MouseEvent) => {
                    let currentPlayer = game.currentPlayer;
                    currentPlayer.makeAMove(game, x, y, e);
                },
                {once: true},
                );
            }

            row.appendChild(cell);
        }

        board.appendChild(row);
    }

    helpers.drawPlayerPanels(game);

    setListenersToActiveBoard(game, board);

    return board;
}


function setListenersToActiveBoard(game: GameBrain, board: HTMLElement): void {
    let [x, y] = game.activeBoardAnchor;
    
    for (let i = x; i < x+3; i++) {
        let rowNode = board.childNodes[i] as HTMLElement;
        
        for (let j = y; j < y+3; j++) {
            let cellNode = rowNode.childNodes[j] as HTMLElement;

            cellNode.classList.add("active");
            cellNode.addEventListener("click", (e: MouseEvent) => {
                let currentPlayer = game.currentPlayer;
                currentPlayer.makeAMove(game, i, j, e);
            });
        }
    }
}