import { aiMakeAMove } from "./ai.js";

export class Player {
    #symbol
    #piecesLeft = 4;
    #winCount = 0;
    #selectedNode = null;
    #selectedX = null;
    #selectedY = null;
    #isAi = false;

    constructor(symbol) {
        this.#symbol = symbol;
    }

    get piecesLeft() {
        return this.#piecesLeft;
    }

    get symbol() {
        return this.#symbol;
    }

    get playerWinCount() {
        return this.#winCount;
    }

    get isAi() {
        return this.#isAi;
    }

    deductPiecesLeft() {
        this.#piecesLeft--;
    }

    toggleAI() {
        this.#isAi = this.#isAi ? false : true;
    }

    makeAMove(game, x, y, e) {
        if (this.#isAi) {
            aiMakeAMove(game);
            this.deductPiecesLeft();

            return true;
        }

        if (game.board[x][y] === null && this.#piecesLeft >= 0 && this.#piecesLeft > 0) {
            game.board[x][y] = this.#symbol;
            e.target.innerHTML = game.board[x][y] || this.#symbol;
            console.log(this.#selectedNode);
            

            if (this.#selectedNode !== null) {
                this.#selectedNode.innerHTML = "";
                this.#selectedNode.classList.remove("selected");
                this.#selectedNode = null;
                game.board[this.#selectedX][this.#selectedY] = null;
            }

            // for (let i = 0; i < game.board[0].length; i++) {
            //     console.log(game.board[i]);
                
            // }

            this.#piecesLeft--;

            game.handleResultValidation();
            game.switchActivePlayer();

            return true;
        } else if (game.board[x][y] === this.#symbol && this.#piecesLeft <= 2 && this.#selectedNode === null) {
            this.#selectedX = x;
            this.#selectedY = y;
            this.#selectedNode = e.target;
            this.#selectedNode.classList.add("selected");
            this.#piecesLeft++;
        } else {
            console.log(`Moves left ${this.#piecesLeft}`);
        }

        game.handleResultValidation(e);

        return false;
    }

    increasePlayerWinCount() {
        this.#winCount++;
    }

    resetPlayerStats() {
        this.#piecesLeft = 4;
    }
}