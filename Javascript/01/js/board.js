"use strict";

import { showWinner } from "./helpers.js";

export function drawBoard(game) {
    const board = document.getElementById("app");

    for (let x = 0; x < 5; x++) {
        let row = document.createElement("div");
        row.classList.add("row");

        for (let y=0; y < 5; y++) {
            let cell = document.createElement("div");
            cell.classList.add("cell");
            cell.innerHTML = game.board[x][y] || `${x}${y}`;

            row.appendChild(cell);
        }

        board.appendChild(row);
    }

    setListenersToActiveBoard(game, board);

    return board;
}

function setListenersToActiveBoard(game, board) {
    let [x, y] = game.activeBoardAnchor;
    
    for (let i = x; i < x+3; i++) {
        let rowNode = board.childNodes[i];
        
        for (let j = y; j < y+3; j++) {
            let cellNode = rowNode.childNodes[j];

            cellNode.classList.add("active");
            cellNode.addEventListener("click", (e) => {
                let currentPlayer = game.currentPlayer;
                currentPlayer.makeAMove(game, i, j, e);


                if (game.handleResultValidation() === "win") {
                    const resultEvent = new CustomEvent("gameEnd", {
                        detail: {
                            text: `Player ${currentPlayer.symbol} wins!`,
                        },
                    });

                    cellNode.dispatchEvent(resultEvent);
                }
            });

            cellNode.addEventListener("gameEnd", (e) => {
                showWinner(e);
            });
        }
    }
}