"use strict";
import * as helpers from "./helpers.js";
import { GameBrain } from "./brain.js";
import { drawBoard } from "./board.js";

helpers.createMainDiv();
let game = new GameBrain();

function cellUpdateFn(x, y, e) {
    game.makeAMove(x, y);
    e.target.innerHTML = game.board[x][y] || "";
}

drawBoard(game.board, game.activeBoard, cellUpdateFn);
