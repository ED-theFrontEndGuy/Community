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
    let button = document.createElement("button");
    button.innerHTML = "Reset";
    button.onclick = function() {
        console.log("Resetting game...");
        
        game.resetGame();

        let board = document.getElementById("app");
        board.innerHTML = "";

        drawBoard(game);

        let announcement = document.getElementById("announcement");
        announcement.innerHTML = "";
    };

    document.body.appendChild(button);
}

export function showWinner(e) {
    let target = document.getElementById("announcement");
    
    target.innerHTML = e.detail.text
}
