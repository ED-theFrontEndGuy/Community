export class GameBrain {
    #board = [[], [], [], [], []];
    #playerX
    #playerO
    #moveActiveBoardAllowed = false;
    #currentPlayer

    #activeBoard = [1,1]

    constructor(playerX, playerO) {
        this.#playerX = playerX;
        this.#playerO = playerO;
    }

    handleResultValidation() {
        // for (let condition of this.winningConditions) {
        //     const [[ax, ay], [bx, by], [cx, cy]] = condition;
        
        //     let A = this.#board[ax][ay];
        //     let B = this.#board[bx][by];
        //     let C = this.#board[cx][cy];
        
        //     if (A && A === B && B === C) {
        //         return true;
        //     }
        // }
        // return false;
    }

    set moveActiveBoardAllowed(flag) {
        this.#moveActiveBoardAllowed = true;
    }

    get activeBoard() {
        return this.#activeBoard;
    }

    get board() {
        return this.#board;
    }

    getCell(x, y) {
        return this.#board[x][y];
    }

    get currentPlayer() {
        if (this.#currentPlayer === this.#playerX) {
            this.#currentPlayer = this.#playerO;
        } else {
            this.#currentPlayer = this.#playerX;
        }

        return this.#currentPlayer;
    }
}