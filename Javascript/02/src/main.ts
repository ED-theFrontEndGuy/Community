import './style.css'
import * as helpers from "./helpers";
import { GameBrain } from "./brain";
import { drawBoard } from "./board";
import { Player } from "./player";

let playerX = new Player("X");
let playerO = new Player("O");
let game = new GameBrain(playerX, playerO);

helpers.createBase();
helpers.createGameResetButton(game, drawBoard);
helpers.startTimer();

let opponentButton = document.getElementById("opponent-btn") as HTMLButtonElement;

opponentButton?.addEventListener("click", () => {
    playerO.toggleAI();

    let playerOtitle = document.getElementsByTagName("h1")[1]; 
    let moveButtons = document.getElementsByClassName("move-btn");

    if (playerO.isAi) {
        playerOtitle.innerHTML = `AI ${playerO.symbol}`;
        Array.from(moveButtons).forEach(button => {
            (button as HTMLButtonElement).classList.add("disabled");
        });
    } else {
        playerOtitle.innerHTML = `Player ${playerO.symbol}`
        Array.from(moveButtons).forEach(button => {
            (button as HTMLButtonElement).classList.remove("disabled");
        });
    }
})

drawBoard(game);
