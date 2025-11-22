module App.Menu
open System

open App.Types
open App.Utils

type MenuState =
| Active
| Terminated

type State<'c> = {
    MenuState: MenuState
    X: int
    Y: int
    CursorX: int
    CursorIndex: int
    Commands: ('c*string) list
}

let initState x y commands =
    {
        MenuState = Active
        X = x
        Y = y
        CursorX = x-2
        CursorIndex = 0
        Commands = commands
    }
    

let updateCursorScreen oldState newState =
    displayMessage oldState.CursorX (oldState.CursorIndex+oldState.Y) ConsoleColor.DarkGreen "  "
    displayMessage newState.CursorX (newState.CursorIndex+newState.Y) ConsoleColor.DarkGreen "☠️"
let updateMenuScreen state =
    state.Commands
    |> List.iteri (fun i (_,c) ->
        displayMessage state.X (state.Y+i) ConsoleColor.DarkGreen c
    )

let updateMenuKeyboard key state =
    match key with
    | ConsoleKey.Enter ->
        {state with MenuState = Terminated}
    | ConsoleKey.UpArrow ->
        {state with CursorIndex = max 0 (state.CursorIndex-1)}
    | ConsoleKey.DownArrow ->
        {state with CursorIndex = min (state.Commands.Length-1) (state.CursorIndex+1)}
    | _ -> state
let updateKeyboard state =
    if Console.KeyAvailable then
        let tecla = Console.ReadKey true
        updateMenuKeyboard tecla.Key state
    else
        state

let updateState state =
    state
    |> updateKeyboard

let updateScreen oldState newState =
    updateMenuScreen newState
    updateCursorScreen oldState newState

let rec mainLoop state =
    let newState = updateState state
    updateScreen state newState
    if newState.MenuState = Active then
        dormirUnRato()
        mainLoop newState
    else
        state
let mostrarMenu x y commandos =
    initState x y commandos
    |> mainLoop
    |> fun state ->
        let i = state.CursorIndex
        fst state.Commands[i]
