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

drawBoard(game);
