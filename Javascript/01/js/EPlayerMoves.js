export const EPlayerMoves = Object.freeze({
    PLACE_PIECE: "PLACE_PIECE",
    MOVE_BOARD: "MOVE_BOARD",
    MOVE_PIECE: "MOVE_PIECE",

    toString(playerMove) {
        return playerMove.toLowerCase();
    }
});