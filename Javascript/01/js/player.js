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

    makeAMove(game, x, y, e) {
        if (game.board[x][y] === null && this.#piecesLeft > 0) {
            game.board[x][y] = this.#symbol;
            e.target.innerHTML = game.board[x][y] || this.#symbol;
            this.#piecesLeft--;

            return true;
        }

        return false;
    }
}