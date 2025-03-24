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

export function showWinner(e) {
    let target = document.getElementById("announcement");
    // target.innerHTML = e.result
    
    target.innerHTML = e.detail.text
}



export function test() {
    let row = ["X", "X", "X", "X", "X"];
    // console.log(row.every(cell => cell === "X"));

    let row2 = ["X", "X", "O", "X", "X"];
    // console.log(row2.every(cell => cell === "X"));

    let arr = Array.from({ length: 5}, () => Array(5))
    // for (let i of arr) {
    //     console.log(i);
    // }

    console.log("X" === "X");
    

}