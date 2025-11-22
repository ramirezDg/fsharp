module App.Menu
open App.Utils
open System

type MenuState =
| Active
| Terminated

type State = {
    MenuState :  MenuState
    X : int
    Y : int
    CursorX: int
    CursorY: int
    Commands: string list
}
let initialState() =
    {
        MenuState = Active
        X = 10
        Y = 10
        CursorX = 8
        CursorY = 10
        Commands = ["New Game"; "Load Game"; "Credits"; "Exit"]
    }

let updateKeyboardMenu key  state =
    match key with
    | ConsoleKey.Enter -> { state with MenuState = Terminated}
    | ConsoleKey.UpArrow -> { state with CursorY =  state.CursorY - 1}
    | ConsoleKey.DownArrow -> { state with CursorY =  state.CursorY + 1}
    | _ -> state

let updateKeyBoard state =
    if Console.KeyAvailable then
        let option = Console.ReadKey true
        updateKeyboardMenu option.Key state
    else
        state

let updateState state =
    state
    |> updateKeyBoard

let updateMenuScreen state =
    state.Commands
    |> List.iteri ( fun index e ->
            Utils.displayMessage state.X ( state.Y + index ) ConsoleColor.Red e
        )

let updateCursorScreen oldState newState =
    Utils.displayMessage oldState.CursorX oldState.CursorY ConsoleColor.Yellow "  "
    Utils.displayMessage newState.CursorX newState.CursorY ConsoleColor.Yellow "💀"

let updateScreen oldState newState =
    updateMenuScreen newState
    updateCursorScreen oldState newState

let rec mainLoop state =
    let newState = updateState state
    updateScreen state newState
    if newState.MenuState = Active then
        sleep()
        mainLoop newState


let mostrarMenu () =
    initialState()
    |> mainLoop
