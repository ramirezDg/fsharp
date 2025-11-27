module App.Navegador

open System
open App.Types
open App.Juego

type NavigatorState=
| ShowMainMenu
| ShowJuego
| ShowPausa
| ShowGameOver
| Terminated

type State = {
    NavigatorState: NavigatorState
    JuegoState: App.Types.State option
}

let initState() =
    {
        NavigatorState = ShowMainMenu
        JuegoState = None
    }

let showMainMenu state =
    Console.Clear()
    Console.CursorVisible <- false
    let anchoConsola = Console.WindowWidth
    let calcX (texto:string) =
        let largo = texto.Length * 8
        max 0 ((anchoConsola - largo) / 2)
    let xAlien = calcX "Alien"
    let xAttack = calcX "Attack"
    Utils.displayMessageGigante xAlien 1 ConsoleColor.DarkYellow "Alien"
    Utils.displayMessageGigante xAttack 8 ConsoleColor.DarkRed "Attack"

    let menuWidth = 30 
    let xMenu = max 0 ((anchoConsola - menuWidth) / 2)
    let yMenu = 18
    match MainMenu.mostrarMenu xMenu yMenu with
    | MenuCommand.NewGame ->
        {state with NavigatorState = ShowJuego; JuegoState = Some (Juego.initState())}
    | MenuCommand.LoadGame ->
        let loadedState = App.Serializacion.cargarEstadoJuego ()
        match loadedState with
        | Some js ->
            let jsRunning = { js with ProgramState = ProgramState.Running }
            {state with NavigatorState = ShowJuego; JuegoState = Some jsRunning}
        | None -> {state with NavigatorState = ShowJuego; JuegoState = Some (Juego.initState())}
    | MenuCommand.Exit ->
        {state with NavigatorState = Terminated}

let showJuego state =
    Console.Clear()
    Console.CursorVisible <- false
    let initialJuegoState =
        match state.JuegoState with
        | Some js -> js
        | None -> Juego.initState()
    let finalJuegoState = Juego.mostrarJuego(initialJuegoState)
    match finalJuegoState.ProgramState with
    | ProgramState.Running -> {state with NavigatorState = ShowJuego; JuegoState = Some finalJuegoState}
    | ProgramState.Paused -> {state with NavigatorState = ShowPausa; JuegoState = Some finalJuegoState}
    | ProgramState.Terminated -> {state with NavigatorState = ShowGameOver; JuegoState = None}

let showGameOver state =
    Console.Clear()
    Console.CursorVisible <- false
    let anchoConsola = Console.WindowWidth
    let altoConsola = Console.WindowHeight
    let texto = "Game Over"
    let largo = texto.Length * 8 // assuming 8 chars per ASCII art letter
    let xGameOver = max 0 ((anchoConsola - largo) / 2)
    let yGameOver = 2
    Utils.displayMessageGigante xGameOver yGameOver ConsoleColor.DarkMagenta texto

    let menuWidth = 40
    let menuHeight = 8
    let menuX = max 0 ((anchoConsola - menuWidth) / 2)
    let menuYOffset = 4
    let menuY = max 0 ((altoConsola - menuHeight) / 2 + menuYOffset)
    match FinJuego.mostrarMenu menuX menuY with
    | GameOverCommand.NewGame ->
        {state with NavigatorState = ShowJuego}
    | GameOverCommand.Exit ->
        {state with NavigatorState = Terminated}

let showPause state =
    Console.Clear()
    Console.CursorVisible <- false
    match MenuPausa.mostrarMenu 20 10 with
    | PauseCommand.Continue ->
        let juegoState =
            state.JuegoState
            |> Option.map (fun js -> { js with ProgramState = ProgramState.Running })
        { state with NavigatorState = ShowJuego; JuegoState = juegoState }
    | PauseCommand.SaveGame ->
        match state.JuegoState with
        | Some js ->
            App.Serializacion.guardarEstadoJuego js
            {state with NavigatorState = ShowMainMenu; JuegoState = None}
        | None ->
            {state with NavigatorState = Terminated}
    | PauseCommand.Exit ->
        {state with NavigatorState = Terminated}

let updateState state =
    match state.NavigatorState with
    | ShowMainMenu -> showMainMenu state
    | ShowJuego -> showJuego state
    | ShowPausa -> showPause state
    | ShowGameOver -> showGameOver state
    | _ -> state

let rec mainLoop state =
    let newState = updateState state
    if newState.NavigatorState <> Terminated then
        mainLoop newState

let mostrar() =
    initState()
    |> mainLoop
