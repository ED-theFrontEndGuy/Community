"use strict"

export function getInitialBoard(boardState, cellUpdateFn) {
    let board = document.createElement("div");
    board.classList.add("board");

    for (let i = 0; i < 3; i++) {
        let row = document.createElement("div");
        row.classList.add("row");

        for (let j=0; j < 3; j++) {
            let cell = document.createElement("div");
            cell.classList.add("cell");
            
            cell.addEventListener("click", (e) => {
                cellUpdateFn(i, j, e);
            });

            cell.innerHTML = boardState[i][j] || "&nbsp;";
            row.appendChild(cell);
        }
        board.appendChild(row);
    }

    return board;
}