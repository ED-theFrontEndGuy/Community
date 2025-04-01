import { DIRECTIONS } from "./EDirections.js";
import { drawBoard } from "./board.js";


export function createBase() {
    let timerDiv = document.createElement("div");
    let appDiv = document.createElement("div");
    let announcementDiv = document.createElement("div");
    let currentPlayerDiv = document.createElement("div");
    
    timerDiv.id = "timer";
    appDiv.id = "app";
    announcementDiv.id = "announcement";
    currentPlayerDiv.id = 'current-player';
    
    document.body.appendChild(timerDiv);
    document.body.appendChild(currentPlayerDiv);
    document.body.appendChild(announcementDiv);
    document.body.appendChild(appDiv);

    document.body.addEventListener("gameEnd", (e) => {
        console.log("Im here");
        
        showWinner(e);
    });
}

function clearBoard() {
    let board = document.getElementById("app");
    let stats = document.getElementsByClassName("stats")[0];
    let announcement = document.getElementById("announcement");
        
    announcement.innerHTML = "";
    board.innerHTML = "";
    stats.innerHTML = "";
}

export function createGameResetButton(game, drawBoard) {
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

function showWinner(e) {
    let target = document.getElementById("announcement");    
    target.innerHTML = e.detail.text
}

export function drawPlayerPanels(game) {
    let div = document.createElement("div");
    div.classList.add("stats");
    let player1Panel = drawPlayerPanel(game, game.getPlayerX);
    let player2Panel = drawPlayerPanel(game, game.getPlayerO);
    
    div.appendChild(player1Panel);
    div.appendChild(player2Panel);
    
    document.body.prepend(div);
}

function drawPlayerPanel(game, player) {    
    let div = document.createElement("div");
    let playerHeader = document.createElement('h1');
    let winCount = document.createElement("p");
    
    playerHeader.innerHTML = `Player ${player.symbol}`;
    winCount.innerHTML = player.playerWinCount;
    
    div.appendChild(playerHeader);
    div.appendChild(winCount);
    div.append(...playerBoardMoveButtons(game));

    return div;
}

function playerBoardMoveButtons(game) {
    let upBtn = createButton(game, DIRECTIONS.UP);
    let downBtn = createButton(game, DIRECTIONS.DOWN);
    let leftBtn = createButton(game, DIRECTIONS.LEFT);
    let rightBtn = createButton(game, DIRECTIONS.RIGHT);

    return new Array(upBtn, leftBtn, rightBtn, downBtn);
}

function createButton(game, DIRECTION) {
    let button = document.createElement("button");
    button.innerHTML = DIRECTIONS.toString(DIRECTION);

    button.addEventListener("click", (e) => {
        let board = document.getElementById("app");
        let stats = document.getElementsByClassName("stats")[0];
        
        board.innerHTML = "";
        stats.innerHTML = "";
        game.moveActiveBoard(DIRECTION);
        game.handleResultValidation();
        drawBoard(game);
    });

    return button;
}
