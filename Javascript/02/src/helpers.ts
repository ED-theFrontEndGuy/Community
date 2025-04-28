import { DIRECTIONS } from "./EDirections";
import { drawBoard } from "./board";
import { GameBrain } from "./brain";
import { Player } from "./player";


export function createBase(): void {
    let timerDiv = document.createElement("div");
    let appDiv = document.createElement("div");
    let announcementDiv = document.createElement("div");
    let currentPlayerDiv = document.createElement("div");
    let switchOpponentDiv = document.createElement("div");
    let switchOpponentBtn = document.createElement("button");
    
    timerDiv.id = "timer";
    appDiv.id = "app";
    announcementDiv.id = "announcement";
    currentPlayerDiv.id = 'current-player';
    switchOpponentDiv.id = 'opponent-btn-div';
    switchOpponentBtn.id = 'opponent-btn';
    switchOpponentBtn.innerHTML = "Switch Opponent";
    
    document.body.appendChild(switchOpponentDiv);
    switchOpponentDiv.appendChild(switchOpponentBtn);
    document.body.appendChild(timerDiv);
    document.body.appendChild(currentPlayerDiv);
    document.body.appendChild(announcementDiv);
    document.body.appendChild(appDiv);

    document.body.addEventListener("gameEnd", (e: Event) => {
        showWinner(e as CustomEvent);
    });
}

export function startTimer(): number {
    let sec = 0; // Start from 0
    const timerElement = document.getElementById("timer");
    timerElement!.innerHTML = "00:00";

    const timer = setInterval(function () {
        let minutes = Math.floor(sec / 60);
        let seconds = sec % 60;

        timerElement!.innerHTML = 
            (minutes < 10 ? "0" : "") + minutes + ":" + 
            (seconds < 10 ? "0" : "") + seconds;

        sec++;
    }, 1000);

    return timer;
}


export function clearBoard(): void {
    let board = document.getElementById("app");
    let stats = document.getElementsByClassName("stats")[0];
    let announcement = document.getElementById("announcement");
        
    announcement!.innerHTML = "";
    board!.innerHTML = "";
    stats.innerHTML = "";
}

export function createGameResetButton(game: GameBrain, drawBoard: Function): void {
    let div = document.createElement("div");
    let button = document.createElement("button");

    div.appendChild(button);
    div.classList.add("btn-container")

    button.classList.add("btn-reset");
    button.innerHTML = "Reset";
    button.onclick = function() {
        console.log("Resetting game...");
        
        game.resetGame();

        clearBoard();
        drawBoard(game);
    };

    document.body.appendChild(div);
}

function showWinner(e: CustomEvent): void {
    let target = document.getElementById("announcement");    
    target!.innerHTML = e.detail.text
}

export function drawPlayerPanels(game: GameBrain): void {
    let div = document.createElement("div");
    div.classList.add("stats");
    
    let player1Panel = drawPlayerPanel(game, game.getPlayerX);
    let player2Panel = drawPlayerPanel(game, game.getPlayerO);
    
    div.appendChild(player1Panel);
    div.appendChild(player2Panel);
    
    document.body.prepend(div);
}

function drawPlayerPanel(game: GameBrain, player: Player): HTMLElement {    
    let div = document.createElement("div");
    let playerHeader = document.createElement('h1');
    let winCount = document.createElement("p");
    
    if (player.isAi) {
        playerHeader.innerHTML = `AI ${player.symbol}`;
    } else {
        playerHeader.innerHTML = `Player ${player.symbol}`;
    }
    winCount.innerHTML = player.playerWinCount.toString();
    
    div.appendChild(playerHeader);
    div.appendChild(winCount);
    div.append(...playerBoardMoveButtons(game));

    return div;
}

function playerBoardMoveButtons(game: GameBrain): HTMLElement[] {
    let upBtn = createButton(game, DIRECTIONS.UP);
    let downBtn = createButton(game, DIRECTIONS.DOWN);
    let leftBtn = createButton(game, DIRECTIONS.LEFT);
    let rightBtn = createButton(game, DIRECTIONS.RIGHT);

    return new Array(upBtn, leftBtn, rightBtn, downBtn);
}

function createButton(game: GameBrain, DIRECTION: string): HTMLElement {
    let button = document.createElement("button");
    button.innerHTML = DIRECTIONS.toString(DIRECTION);
    button.classList.add("move-btn");
    if (game.getPlayerO.isAi) {
        button.classList.add("disabled");
    }

    button.addEventListener("click", () => {
        let board = document.getElementById("app");
        let stats = document.getElementsByClassName("stats")[0];
        
        board!.innerHTML = "";
        stats.innerHTML = "";
        game.moveActiveBoard(DIRECTION);
        game.handleResultValidation();
        drawBoard(game);
    });

    return button;
}
