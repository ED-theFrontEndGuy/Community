export class Player {
    #symbol
    #piecesLeft = 4;

    constructor(symbol) {
        this.#symbol = symbol
    }

    get piecesLeft() {
        return this.#piecesLeft;
    }

    get symbol() {
        return this.#symbol;
    }

    makeAMove(board, x, y, e) {
        console.log(board[x][y])
        if (board[x][y] === undefined && this.#piecesLeft > 0) {
            board[x][y] = this.#symbol;

            e.target.innerHTML = board[x][y] || this.#symbol;
            this.#piecesLeft--;
            console.log(this.#piecesLeft)
            console.log(board[x][y])
        }
    }
}