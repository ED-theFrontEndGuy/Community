export const DIRECTIONS = Object.freeze({
    UP: "UP",
    DOWN: "DOWN",
    LEFT: "LEFT",
    RIGHT: "RIGHT",

    toString(direction: string) {
        return direction.toLowerCase();
    }
});