"use strict";

export class GameBrain{
    #board = [[], [], [], [], []];
    currentPlayer = "X";
    winningConditions = [
        [[1,1], [1,2], [1,3]],
        [[2,1], [2,2], [2,3]],
        [[3,1], [3,2], [3,3]],
        [[1,1], [2,1], [3,1]],
        [[1,2], [2,2], [3,2]],
        [[1,3], [2,3], [3,3]],
        [[1,1], [2,2], [3,3]],
        [[1,3], [2,2], [3,1]]
    ];
    activeBoard = [
        [[1,1], [1,2], [1,3]],
        [[2,1], [2,2], [2,3]],
        [[3,1], [3,2], [3,3]]
    ]

    makeAMove(x, y) {
        if (this.#board[x][y] === undefined) {
            this.#board[x][y] = this.currentPlayer;
            
            if (this.handleResultValidation()) {
                alert(`${this.currentPlayer} wins!`);
                // this.resetBoard();
                return;
            }
            
            this.currentPlayer = this.currentPlayer === "X" ? "O" : "X";
        }
    }

    handleResultValidation() {
        for (let condition of this.winningConditions) {
            const [[ax, ay], [bx, by], [cx, cy]] = condition;
        
            let A = this.#board[ax][ay];
            let B = this.#board[bx][by];
            let C = this.#board[cx][cy];
        
            if (A && A === B && B === C) {
                return true;
            }
        }
        return false;
    }

    get board() {
        return this.#board;
    }
}