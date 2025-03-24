export class GameBrain {
    // #board = [[], [], [], [], []];
    #board = Array.from({ length: 5}, () => Array(5).fill(null));
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
        let x = this.#activeBoard[0];
        let y = x + 3;
        if (this.checkWin(x, y, this.#currentPlayer.symbol)) {

            console.log(`${this.#currentPlayer.symbol} wins!`);
            // alert(`${this.#currentPlayer.symbol} wins!`);

            return "win";
        }
        
        if (this.checkTie()) {
            console.log("It's a tie!");
            alert("It's a tie!");
            return "tie";
        }
    }
    
    checkWin(x, y, symbol) {
        console.log(`row: ${x}`);
        console.log(`column: ${y}`);
        
        return (
            this.checkRow(x, symbol) ||
            this.checkColumn(y, symbol) ||
            this.checkDiagonals(symbol)
        );
    }
    
    checkRow(row, symbol) {
        // return this.#board[row].every(cell => cell === symbol);
        console.log(symbol);
        let col = this.#activeBoard[1];

        console.log(this.#activeBoard[1]);
        
        let tryout = this.#board[row];
        console.log(tryout);
        console.log(this.#board[row]);
        
    

        // for (let i of tryout) {
        //     console.log(i);
            
        // }
        
        
        return tryout.toSpliced(0, row).toSpliced(row+2,col).every(cell => cell === symbol);
    }
    
    checkColumn(col, symbol) {
        return this.#board.every(row => row[col] === symbol);
    }
    
    checkDiagonals(symbol) {
        const mainDiagonalWin = this.#board.every((row, idx) => row[idx] === symbol);
        const antiDiagonalWin = this.#board.every((row, idx) => row[4 - idx] === symbol);
        
        return mainDiagonalWin || antiDiagonalWin;
    }
    
    checkTie() {
        return this.#board.every(row => row.every(cell => cell !== null));
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