import { GameBrain } from "./brain";

export class Player {
    #symbol: string;
    #piecesLeft: number = 4;
    #winCount: number = 0;
    #selectedNode: HTMLElement | null = null;
    #selectedX: number | null = null;
    #selectedY: number | null = null;
    #isAi = false;

    constructor(symbol: string) {
        this.#symbol = symbol;
    }

    get piecesLeft(): number {
        return this.#piecesLeft;
    }

    get symbol(): string {
        return this.#symbol;
    }

    get playerWinCount(): number {
        return this.#winCount;
    }

    get isAi(): boolean {
        return this.#isAi;
    }

    deductPiecesLeft(): void {
        this.#piecesLeft--;
    }

    toggleAI(): void {
        this.#isAi = this.#isAi ? false : true;
    }

    makeAMove(game: GameBrain, x: number, y: number, e: Event): boolean {
        if (game.board[x][y] === null && this.#piecesLeft >= 0 && this.#piecesLeft > 0) {
            game.board[x][y] = this.#symbol;
            (e.target as HTMLElement).innerHTML = game.board[x][y] || this.#symbol;
            console.log(this.#selectedNode);
            

            if (this.#selectedNode !== null) {
                this.#selectedNode.innerHTML = "";
                this.#selectedNode.classList.remove("selected");
                this.#selectedNode = null;

                if (this.#selectedX !== null && this.#selectedY !== null) {
                    game.board[this.#selectedX][this.#selectedY] = "";
                }
            }

            this.#piecesLeft--;

            game.handleResultValidation();
            game.switchActivePlayer();

            return true;
        } else if (game.board[x][y] === this.#symbol && this.#piecesLeft <= 2 && this.#selectedNode === null) {
            this.#selectedX = x;
            this.#selectedY = y;
            this.#selectedNode = e.target as HTMLElement;
            this.#selectedNode.classList.add("selected");
            this.#piecesLeft++;
        } else {
            console.log(`Moves left ${this.#piecesLeft}`);
        }

        game.handleResultValidation();

        return false;
    }

    increasePlayerWinCount() {
        this.#winCount++;
    }

    resetPlayerStats() {
        this.#piecesLeft = 4;
    }
}