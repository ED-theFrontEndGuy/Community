"use strict";

import * as helpers from "./helpers.js";
import { GameBrain } from "./brain.js";
import { drawBoard } from "./board.js";
import { Player } from "./player.js";

let playerX = new Player("X");
let playerO = new Player("O");
let game = new GameBrain(playerX, playerO);

helpers.createBase();
helpers.createGameResetButton(game, drawBoard);
helpers.startTimer();

let opponentButton = document.getElementById("opponent-btn");
opponentButton.addEventListener("click", (e) => {
    playerO.toggleAI();

    let playerOtitle = document.getElementsByTagName("h1")[1];
    console.log(playerOtitle);
    
    let moveButtons = document.getElementsByClassName("move-btn");

    if (playerO.isAi) {
        playerOtitle.innerHTML = `AI ${playerO.symbol}`;
        Array.from(moveButtons).forEach(button => {
            button.classList.add("disabled");
        });
    } else {
        playerOtitle.innerHTML = `Player ${playerO.symbol}`
        Array.from(moveButtons).forEach(button => {
            button.classList.remove("disabled");
        });
    }
})

drawBoard(game);
