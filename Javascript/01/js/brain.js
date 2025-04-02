import { DIRECTIONS } from "./EDirections.js";
import { drawBoard } from "./board.js";
import { clearBoard } from "./helpers.js";

export class GameBrain {
    #board = Array.from({ length: 5}, () => Array(5).fill(null));
    #activeBoard = Array.from({ length: 3}, () => Array(3).fill(null));
    #activeBoardAnchor = [1, 1];
    #playerX
    #playerO
    #currentPlayer

    constructor(playerX, playerO) {
        this.#playerX = playerX;
        this.#playerO = playerO;
        this.#currentPlayer = this.#playerX;
    }

    get activeBoardAnchor() {
        return this.#activeBoardAnchor;
    }

    get getPlayerX() {
        return this.#playerX;
    }

    get getPlayerO() {
        return this.#playerO;
    }

    resetGame() {
        this.#board = Array.from({ length: 5}, () => Array(5).fill(null));
        this.#activeBoard = Array.from({ length: 3}, () => Array(3).fill(null));
        this.#activeBoardAnchor = [1, 1];
        this.#playerX.resetPlayerStats();
        this.#playerO.resetPlayerStats();
        this.#currentPlayer = this.#playerX;
    }

    updateActiveBoard() {
        let [x, y] = this.#activeBoardAnchor;

        for (let i = 0; i < 3; i++) {
            for (let j = 0; j < 3; j++) {
                this.#activeBoard[i][j] = this.#board[x + i][y + j];
            }
        }

        return this.#activeBoard;
    }

    displayError(direction) {
        console.log(`Not allowed move. Move ${direction} out of bounds.`);
    }

    moveActiveBoard(direction) {
        if (this.#currentPlayer.piecesLeft <= 2) {
            switch (direction) {
                case DIRECTIONS.UP:
                    if (this.#activeBoardAnchor[0] > 0) {
                        this.#activeBoardAnchor[0] = this.#activeBoardAnchor[0] - 1;
                    } else {
                        this.displayError(direction);
                    }
                    break;
                case DIRECTIONS.DOWN:
                    if (this.#activeBoardAnchor[0] < 2) {
                        this.#activeBoardAnchor[0] = this.#activeBoardAnchor[0] + 1;
                    } else {
                        this.displayError(direction);
                    }
                    break;
                case DIRECTIONS.LEFT:
                    if (this.#activeBoardAnchor[1] > 0) {
                        this.#activeBoardAnchor[1] = this.#activeBoardAnchor[1] - 1;
                    } else {
                        this.displayError(direction);
                    }
                    break;
                case DIRECTIONS.RIGHT:
                    if (this.#activeBoardAnchor[1] < 2) {
                        this.#activeBoardAnchor[1] = this.#activeBoardAnchor[1] + 1;
                    } else {
                        this.displayError(direction);
                    }
                    break;
                default:
                    console.log(`Unknown direction ${direction}`);
            }

            this.switchActivePlayer();
        } else {
            console.log("Not able to move active board yet.");
        }
    }

    testPrintActiveBoard() {
        for (let i = 0; i < 3; i++) {
            console.log(this.#activeBoard[i]);
        }
    }

    handleResultValidation() {
        this.updateActiveBoard();
        // this.testPrintActiveBoard();

        if (this.checkTie()) {
            console.log("It's a tie!");
            const resultEvent = new CustomEvent("gameEnd", {
                detail: {
                    text: `It's a tie!`,
                },
                bubbles: true,
            });

            document.getElementById("announcement").dispatchEvent(resultEvent);

            this.resetGame();
        } else if (this.checkWin()) {
            console.log(`${this.#currentPlayer.symbol} wins!`);

            const resultEvent = new CustomEvent("gameEnd", {
                detail: {
                    text: `Player ${this.#currentPlayer.symbol} wins!`,
                },
                bubbles: true,
            });

            document.getElementById("announcement").dispatchEvent(resultEvent);
            this.#currentPlayer.increasePlayerWinCount();

            this.resetGame();
        }
    }
    
    checkWin() {
        const players = [this.#playerX, this.#playerO];

        for (let player of players) {
            if (this.checkRow(player) ||
                this.checkColumn(player) ||
                this.checkDiagonals(player)
            ) {
                this.#currentPlayer = player;
                return true;
            }
        }

        return false;
    }
    
    checkRow(player) {
        return this.#activeBoard.some(row => row.every(cell => cell === player.symbol));
    }
    
    checkColumn(player) {
        return [0, 1, 2].some(col => 
            this.#activeBoard.every(row => row[col] === player.symbol)
        );
    }
    
    checkDiagonals(player) {
        const mainDiagonalWin = [0, 1, 2].every(i => this.#activeBoard[i][i] === player.symbol);
        const antiDiagonalWin = [0, 1, 2].every(i => this.#activeBoard[i][2 - i] === player.symbol);
        
        return mainDiagonalWin || antiDiagonalWin;
    }
    
    checkTie() {
        const playerXwins = this.checkRow(this.#playerX) || this.checkColumn(this.#playerX);
        const playerOwins = this.checkRow(this.#playerO) || this.checkColumn(this.#playerO);

        return playerXwins && playerOwins;
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

    switchActivePlayer() {
        if (this.#currentPlayer === this.#playerX) {
            this.#currentPlayer = this.#playerO;
        } else {
            this.#currentPlayer = this.#playerX;
        }

        document.getElementById("current-player").innerHTML = `Turn: ${this.#currentPlayer.symbol}`;

        if (this.#currentPlayer.isAi) {
            this.#currentPlayer.makeAMove(this);
        }
    }

    get currentPlayer() {
        return this.#currentPlayer;
    }
}