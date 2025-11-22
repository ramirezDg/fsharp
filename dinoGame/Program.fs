open System
open System.Threading

(* States *)

type StateSystemDisplay = {
    Foreground: ConsoleColor
    Background: ConsoleColor
    CursorLeft: int
    CursorTop: int
    WindowTitle: string
}

type StateGame = {
    DinoPosX: int
    DinoPosY: int
    StartPosX: int
    StartPosY: int
    MoveDeltaX: int
    DisplayW: int
    DisplayH: int
    IsJumping: bool
    JumpStep: int
}

type GameElement = {
    Emoji: string
    PosX: int
    PosY: int
}

let getCurrentState () = {
    Foreground = Console.ForegroundColor
    Background = Console.BackgroundColor
    CursorLeft = Console.CursorLeft
    CursorTop = Console.CursorTop
    WindowTitle = Console.Title
}

let stateDisplayGame = {
    Foreground = ConsoleColor.White
    Background = ConsoleColor.Black
    CursorLeft = 0
    CursorTop = 0
    WindowTitle = "Dino Game"
}
let setState (state: StateSystemDisplay) =
    Console.ForegroundColor <- state.Foreground
    Console.BackgroundColor <- state.Background
    Console.SetCursorPosition(state.CursorLeft, state.CursorTop)
    Console.Title <- state.WindowTitle

let sleepFun (time: int option) =
    let t = defaultArg time 35
    Thread.Sleep(t)
let initStateSystem = getCurrentState()

let getStringLength (s: string) = s.Length
(* colition elements *)
let cactus = "🌵"
let nube = "☁️"
let dino = "🦖"
let floor = "_"
let elementosJuego = [
    { Emoji = cactus; PosX = 20; PosY = 10 }
    { Emoji = nube;   PosX = 40; PosY = 3  }
    { Emoji = floor; PosX = 40; PosY = 3 }
]
(* movement function *)
let getFloorElement displayW displayH =
    let floorY = (displayH / 2) + (displayH / 5)
    { Emoji = String.replicate displayW "_"; PosX = 0; PosY = floorY }

let setPositionDino key (state: StateGame) =
    let dinoLen = getStringLength dino
    match key with
    | ConsoleKey.LeftArrow  ->
        { state with DinoPosX = max 0 (state.DinoPosX - state.MoveDeltaX) }
    | ConsoleKey.RightArrow ->
        { state with DinoPosX = min (state.DisplayW - dinoLen) (state.DinoPosX + state.MoveDeltaX) }
    | ConsoleKey.UpArrow when not state.IsJumping ->
        { state with IsJumping = true; JumpStep = 0 }
    | _ -> state

let setPositionDinoWithJump key (state: StateGame) =
    let dinoLen = getStringLength dino
    match key with
    | ConsoleKey.LeftArrow  -> { state with DinoPosX = max 0 (state.DinoPosX - state.MoveDeltaX) }
    | ConsoleKey.RightArrow -> { state with DinoPosX = min (state.DisplayW - dinoLen) (state.DinoPosX + state.MoveDeltaX) }
    | ConsoleKey.UpArrow when not state.IsJumping -> { state with IsJumping = true; JumpStep = 0 }
    | _ -> state

let jumpHeight = 4
let jumpSteps = 8

let updateJump (state: StateGame) =
    let floorY = (state.DisplayH / 2) + (state.DisplayH / 5)
    if state.IsJumping then
        let progress = state.JumpStep
        let half = jumpSteps / 2
        let newY =
            if progress < half then
                floorY - (jumpHeight * progress / half)
            else
                floorY - (jumpHeight * (jumpSteps - progress) / half)
        if progress >= jumpSteps then
            { state with DinoPosY = floorY; IsJumping = false; JumpStep = 0 }
        else
            { state with DinoPosY = newY; JumpStep = state.JumpStep + 1 }
    else
        { state with DinoPosY = floorY }

let rec displayUi (state: StateGame) =
    let state =
        if state.IsJumping then updateJump state
        else state
    Console.Clear()
    let floorElem = getFloorElement state.DisplayW state.DisplayH
    Console.SetCursorPosition(floorElem.PosX, floorElem.PosY)
    Console.Write(floorElem.Emoji)
    Console.SetCursorPosition(state.DinoPosX, state.DinoPosY)
    Console.Write(dino)
    if Console.KeyAvailable then
        let key = Console.ReadKey(true).Key
        let newState =
            if state.IsJumping then setPositionDinoWithJump key state
            else setPositionDino key state
        sleepFun (Some 20)
        displayUi newState
    else
        sleepFun (Some 35)
        displayUi state

[<EntryPoint>]
let main argv =
    Console.CursorVisible <- false
    setState stateDisplayGame
    let floorY = (Console.WindowHeight / 2) + (Console.WindowHeight / 5)
    let initialState = {
        DinoPosX = 0
        DinoPosY = floorY
        StartPosX = 0
        StartPosY = floorY
        MoveDeltaX = 2
        DisplayW = Console.WindowWidth
        DisplayH = Console.WindowHeight
        IsJumping = false
        JumpStep = 0
    }
    displayUi initialState
    0
