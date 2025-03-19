"use strict";

function isActiveCell(i, j, activeBoard) {
    return activeBoard.some(row => row.some(cell => cell[0] === i && cell[1] === j));
}

export function drawBoard(boardState, activeBoard, cellUpdateFn) {
    const board = document.getElementById("app");

    for (let i = 0; i < 5; i++) {
        let row = document.createElement("div");
        row.classList.add("row");

        for (let j=0; j < 5; j++) {
            let cell = document.createElement("div");
            cell.classList.add("cell");

            if (isActiveCell(i, j, activeBoard)) {
                cell.classList.add("active");
                cell.addEventListener("click", (e) => {
                    cellUpdateFn(i, j, e);
                });
            }
            

            cell.innerHTML = boardState[i][j] || `${i}${j}`;
            row.appendChild(cell);
        }
        board.appendChild(row);
    }

    return board;
}
