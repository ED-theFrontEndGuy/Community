"use strict";

export function drawBoard() {
    const board = document.getElementById("app");

    for (let i = 0; i < 5; i++) {
        let row = document.createElement("div");
        row.classList.add("row");

        for (let j=0; j < 5; j++) {
            let cell = document.createElement("div");
            cell.classList.add("cell");
            
            // cell.addEventListener("click", (e) => {
            //     cellUpdateFn(i, j, e);
            // });

            // cell.innerHTML = boardState[i][j] || "&nbsp;";
            row.appendChild(cell);
        }
        board.appendChild(row);
    }
}