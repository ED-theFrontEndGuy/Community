export class GameBrain {
    #board = Array.from({ length: 5}, () => Array(5).fill(null));
    #activeBoard = Array.from({ length: 3}, () => Array(3).fill(null));
    #activeBoardAnchor = [0, 0];
    #playerX
    #playerO
    #currentPlayer
    #moveActiveBoardAllowed = false;


    constructor(playerX, playerO) {
        this.#playerX = playerX;
        this.#playerO = playerO;
    }

    get activeBoardAnchor() {
        return this.#activeBoardAnchor;
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

    testPrintActiveBoard() {
        for (let i = 0; i < 3; i++) {
            console.log(this.#activeBoard[i]);
        }
    }

    handleResultValidation() {
        this.updateActiveBoard();
        this.testPrintActiveBoard();
        

        if (this.checkWin()) {
            console.log(`${this.#currentPlayer.symbol} wins!`);

            return "win";
        }
        
        if (this.checkTie()) {
            console.log("It's a tie!");
            alert("It's a tie!");
            return "tie";
        }
    }
    
    checkWin() {
        return (
            this.checkRow() ||
            this.checkColumn() ||
            this.checkDiagonals()
        );
    }
    
    checkRow() {
        return this.#activeBoard.some(row => row.every(cell => cell === this.#currentPlayer.symbol));
    }
    
    checkColumn() {
        return [0, 1, 2].some(col => 
            this.#activeBoard.every(row => row[col] === this.#currentPlayer.symbol)
        );
    }
    
    checkDiagonals() {
        const mainDiagonalWin = [0, 1, 2].every(i => this.#activeBoard[i][i] === this.#currentPlayer.symbol);
        const antiDiagonalWin = [0, 1, 2].every(i => this.#activeBoard[i][2 - i] === this.#currentPlayer.symbol);
        
        return mainDiagonalWin || antiDiagonalWin;
    }
    
    checkTie() {
        return this.#activeBoard.every(row => row.every(cell => cell !== null));
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