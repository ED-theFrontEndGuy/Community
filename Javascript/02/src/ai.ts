import { GameBrain } from "./brain";

export function aiMakeAMove(game: GameBrain): void {
    console.log("AI is making a move...");
    
    if (game.currentPlayer.piecesLeft <= 0) {
        console.log("AI has no more pieces left to place.");
        const resultEvent = new CustomEvent("gameEnd", {
            detail: {
                text: `It's a tie!`,
            },
            bubbles: true,
        });

        document.getElementById("announcement")!.dispatchEvent(resultEvent);

        
        return;
    }

    let move = findBestMove(game);
    if (move) {
        let [x, y] = move;
        game.board[x][y] = game.currentPlayer.symbol;

        let cell = document.getElementById("app")!.childNodes[x].childNodes[y] as HTMLElement;
        cell.innerHTML = game.currentPlayer.symbol;
        game.currentPlayer.deductPiecesLeft();

        // Validate board (but don't check full game win)
        game.handleResultValidation();

        // **Switch turns properly**
        game.switchActivePlayer(); // Ensures player X can move next
    }
}

function checkLocalWin(game: GameBrain, anchorX: number, anchorY: number, playerSymbol: string): boolean {
    let board = game.board;

    // Check rows
    for (let row = 0; row < 3; row++) {
        if (
            board[anchorX + row][anchorY] === playerSymbol &&
            board[anchorX + row][anchorY + 1] === playerSymbol &&
            board[anchorX + row][anchorY + 2] === playerSymbol
        ) {
            return true;
        }
    }

    // Check columns
    for (let col = 0; col < 3; col++) {
        if (
            board[anchorX][anchorY + col] === playerSymbol &&
            board[anchorX + 1][anchorY + col] === playerSymbol &&
            board[anchorX + 2][anchorY + col] === playerSymbol
        ) {
            return true;
        }
    }

    // Check diagonals
    if (
        board[anchorX][anchorY] === playerSymbol &&
        board[anchorX + 1][anchorY + 1] === playerSymbol &&
        board[anchorX + 2][anchorY + 2] === playerSymbol
    ) {
        return true;
    }

    if (
        board[anchorX][anchorY + 2] === playerSymbol &&
        board[anchorX + 1][anchorY + 1] === playerSymbol &&
        board[anchorX + 2][anchorY] === playerSymbol
    ) {
        return true;
    }

    return false;
}


function canWinNextMove(game: GameBrain, playerSymbol: string): [number, number] | null {
    let [anchorX, anchorY] = game.activeBoardAnchor;

    for (let x = anchorX; x < anchorX + 3; x++) {
        for (let y = anchorY; y < anchorY + 3; y++) {
            if (game.board[x][y] === null) {
                // Temporarily place the symbol
                game.board[x][y] = playerSymbol;
                
                // Check if this move wins the board
                if (checkLocalWin(game, anchorX, anchorY, playerSymbol)) {
                    game.board[x][y] = null; // Reset
                    return [x, y]; // Winning move found
                }

                game.board[x][y] = null; // Reset
            }
        }
    }
    return null;
}


function findBestMove(game: GameBrain): [number, number] | null {
    let availableMoves: [number, number][] = [];

    let [anchorX, anchorY] = game.activeBoardAnchor;

    for (let x = anchorX; x < anchorX + 3; x++) {
        for (let y = anchorY; y < anchorY + 3; y++) {
            if (game.board[x][y] === null) {
                availableMoves.push([x, y]);
            }
        }
    }

    // 1. Check for AI win
    let winningMove = canWinNextMove(game, game.currentPlayer.symbol);
    if (winningMove) return winningMove;

    // 2. Check for blocking opponent win
    let blockingMove = canWinNextMove(game, game.getPlayerX.symbol);
    if (blockingMove) return blockingMove;

    // 3. Prefer center
    let center: [number, number] = [anchorX + 1, anchorY + 1];
    if (game.board[center[0]][center[1]] === null) {
        return center;
    }

    // 4. Prefer strategic corners
    let strategicMoves: [number, number][] = [
        [anchorX, anchorY], [anchorX, anchorY + 2],
        [anchorX + 2, anchorY], [anchorX + 2, anchorY + 2]
    ];
    let openStrategicMoves = strategicMoves.filter(([x, y]) => game.board[x][y] === null);
    if (openStrategicMoves.length > 0) {
        return openStrategicMoves[Math.floor(Math.random() * openStrategicMoves.length)];
    }

    // 5. Random available move
    return availableMoves.length > 0 ? availableMoves[Math.floor(Math.random() * availableMoves.length)] : null;
}
