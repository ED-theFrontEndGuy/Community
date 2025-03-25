"use strict";


export function createMainDiv() {
    let appDiv = document.createElement("div");
    appDiv.id = "app";
    let announcementDiv = document.createElement("div");
    announcementDiv.id = "announcement"
    document.body.appendChild(announcementDiv);
    document.body.appendChild(appDiv);

    announcementDiv.addEventListener("gameEnd", (e) => {
        showWinner(announcementDiv, e);
    });
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

        let board = document.getElementById("app");
        let stats = document.getElementsByClassName("stats")[0];
        
        board.innerHTML = "";
        stats.innerHTML = "";

        drawBoard(game);
        drawPlayerPanels(game);

        let announcement = document.getElementById("announcement");
        announcement.innerHTML = "";
    };

    document.body.appendChild(div);
}

export function showWinner(e) {
    let target = document.getElementById("announcement");
    
    target.innerHTML = e.detail.text
}

export function drawPlayerPanels(game) {
    let div = document.createElement("div");
    div.classList.add("stats");
    let player1Panel = drawPlayerPanel(game.getPlayerX);
    let player2Panel = drawPlayerPanel(game.getPlayerO);
    
    div.appendChild(player1Panel);
    div.appendChild(player2Panel);
    
    document.body.prepend(div);
}

function drawPlayerPanel(player) {    
    let div = document.createElement("div");
    let playerHeader = document.createElement('h1');
    let winCount = document.createElement("p");
    
    playerHeader.innerHTML = `Player ${player.symbol}`;
    winCount.innerHTML = player.playerWinCount;
    
    div.appendChild(playerHeader);
    div.appendChild(winCount);
    div.append(...playerBoardMoveButtons());

    return div;
}

function playerBoardMoveButtons(player) {
    let upBtn = createButton("up");
    let leftBtn = createButton("left");
    let rightBtn = createButton("right");
    let downBtn = createButton("down");
    return new Array(upBtn, leftBtn, rightBtn, downBtn);
}

function createButton(text) {
    let button = document.createElement("button");
    button.classList.add("btn-reset");
    button.innerHTML = text;

    return button;
}
