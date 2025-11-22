open System
open System.Threading

type ProgramState =
| Running
| Terminated

type ExplodeState =
| Visible
| Colisionado
| Pausado

type Missil = {
    X: int
    Y: int
}

type State = {
    AlienX: int
    AlienY: int
    Counter: int
    Misiles: Missil list
    Tick: int
    Width: int
    Height: int
    ProgramState: ProgramState
    EnemyX: int
    EnemyY: int
    EnemySpeed: float
    EnemyState: ExplodeState
    ColisionTime: int
    Puntaje: int
    MisilesEnemigos: Missil list
    UltimoDisparo: int
    AlienState: ExplodeState
    AlienColisionTime: int
}

let initState() =
    let width = Console.BufferWidth
    let height = Console.BufferHeight
    {
        Width = width
        Height = height
        AlienX = width/2
        AlienY = height/2
        Misiles=[]
        ProgramState = Running
        Counter = 0
        Tick = 0
        EnemyX = width-10
        EnemyY = 0
        EnemySpeed = Math.PI / 50.0
        EnemyState = Visible
        ColisionTime = 0
        Puntaje = 0
        MisilesEnemigos = []
        UltimoDisparo =0
        AlienState = Visible
        AlienColisionTime = 0
    }

let displayMessage x y color (mensaje:string) =
    Console.SetCursorPosition(x,y)
    Console.ForegroundColor <- color
    Console.Write mensaje

let displayJustifiedMessage x y color (mensaje:string) =
    let nuevaX = x-(mensaje.Length-1)
    displayMessage nuevaX y color mensaje


let displayAlien state =
    match state.AlienState with
    | Visible -> displayMessage state.AlienX state.AlienY ConsoleColor.Yellow "👽"
    | Colisionado -> displayMessage state.AlienX state.AlienY ConsoleColor.Yellow "💥"
    | _ -> ()
    state


let displayEnemy state =
    match state.EnemyState with
    | Visible -> displayMessage state.EnemyX state.EnemyY ConsoleColor.Yellow "👾"
    | Colisionado -> displayMessage state.EnemyX state.EnemyY ConsoleColor.Yellow "💥"
    | _ -> ()
    state

let displayMissil state =
    state.Misiles
    |> List.iter ( fun m ->
        displayMessage m.X m.Y ConsoleColor.Red "=>"
    )
    state

let displayMisilesEnemigos state =
    state.MisilesEnemigos
    |> List.iter ( fun m ->
        displayMessage m.X m.Y ConsoleColor.Yellow "<="
    )
    state

let displayCounter state =
    displayJustifiedMessage (state.Width-1) 0 ConsoleColor.Yellow $"{state.Counter}"
    state

let displayPuntaje state =
    displayMessage 0 0 ConsoleColor.Cyan $"Puntaje: {state.Puntaje}"
    state
let cleanAlien state =
    match state.AlienState with
    | Visible | Colisionado -> displayMessage state.AlienX state.AlienY ConsoleColor.Yellow "  "
    | _ -> ()
    state

let cleanEnemy state =
    match state.EnemyState with
    | Visible | Colisionado -> displayMessage state.EnemyX state.EnemyY ConsoleColor.Yellow "  "
    | _ -> ()
    state

let cleanMissil state =
    state.Misiles
    |> List.iter ( fun m ->
        displayMessage m.X m.Y ConsoleColor.Yellow "  "
    )
    state

let cleanMisilesEnemigos state =
    state.MisilesEnemigos
    |> List.iter ( fun m ->
        displayMessage m.X m.Y ConsoleColor.Yellow "  "
    )
    state
let dormirUnRato() =
    Thread.Sleep 40

let updateAlienKeyboard key state =
    match key with
    | ConsoleKey.UpArrow ->
        {state with AlienY = max 0 (state.AlienY-1)}
    | ConsoleKey.DownArrow ->
        { state with AlienY = min (state.Height-1) (state.AlienY+1)}
    | ConsoleKey.LeftArrow ->
        { state with AlienX = max 0 (state.AlienX-1)}
    | ConsoleKey.RightArrow ->
        { state with AlienX = min (state.Width-2) (state.AlienX+1)}
    | _ -> state


let updateScape key state =
    match key with
    | ConsoleKey.Escape ->
        {state with ProgramState = Terminated}
    | _ -> state

let updateMissilKeyboard key state =
    match key with
    | ConsoleKey.Spacebar ->
        let nuevoMisil = { X = state.AlienX+2; Y= state.AlienY}
        {state with Misiles = nuevoMisil :: state.Misiles}
    | _ -> state
let updateKeyboard state =
    if Console.KeyAvailable then
        let tecla = Console.ReadKey(true)
        state
        |> updateScape tecla.Key
        |> updateAlienKeyboard tecla.Key
        |> updateMissilKeyboard tecla.Key
    else
        state

let updateTick state =
    {state with Tick = state.Tick+1}

let updateClock state =
    if state.Tick % 25 = 0 then
        {state with Counter = state.Counter+1}
    else
        state


let updateMissilAnimation state =
    let misiles =
        state.Misiles
        |> Seq.map ( fun m ->
            { m with X = m.X+1}
        )
        |> Seq.filter ( fun m -> m.X < state.Width-1)
        |> Seq.toList
    {state with Misiles=misiles}

let updateMisilesEnemigos state =
    let misiles =
        state.MisilesEnemigos
        |> Seq.map ( fun m ->
            { m with X = m.X-1}
        )
        |> Seq.filter ( fun m -> m.X >=0 )
        |> Seq.toList
    {state with MisilesEnemigos=misiles}


let dispararMisilEnemigo state =
    if state.Tick - state.UltimoDisparo >= 6 then
        let nuevoMisil = { X = state.EnemyX-2; Y = state.EnemyY }
        {state with MisilesEnemigos = nuevoMisil :: state.MisilesEnemigos;UltimoDisparo = state.Tick}
    else
        state
let updateEnemy state =
    if state.EnemyState = Visible then
        let newY = - float state.Height /2.0 * (Math.Cos (state.EnemySpeed*float state.Tick)- 1.0 )
        {state with EnemyY = min (state.Height-1) (int newY)}
        |> dispararMisilEnemigo
    else
        state

let updateCollision state =
    match state.EnemyState, state.AlienState with
    | Visible, Visible ->
        let enemyHit =
            state.Misiles |> List.exists (fun m -> m.Y = state.EnemyY && (m.X+1) = state.EnemyX)
        let alienHit =
            state.MisilesEnemigos |> List.exists (fun m -> m.Y = state.AlienY && (m.X-1) = state.AlienX)
        match enemyHit, alienHit with
        | true, false ->
            let nuevosMisiles = state.Misiles |> List.filter (fun m -> not (m.Y = state.EnemyY && (m.X+1) = state.EnemyX))
            {state with Misiles = nuevosMisiles; EnemyState=Colisionado; ColisionTime=state.Tick; ProgramState=Terminated; Puntaje=state.Puntaje+1}
        | false, true ->
            let nuevosMisilesEnemigos = state.MisilesEnemigos |> List.filter (fun m -> not (m.Y = state.AlienY && (m.X-1) = state.AlienX))
            {state with MisilesEnemigos = nuevosMisilesEnemigos; AlienState=Colisionado; AlienColisionTime=state.Tick; ProgramState=Terminated}
        | true, true ->
            let nuevosMisiles = state.Misiles |> List.filter (fun m -> not (m.Y = state.EnemyY && (m.X+1) = state.EnemyX))
            let nuevosMisilesEnemigos = state.MisilesEnemigos |> List.filter (fun m -> not (m.Y = state.AlienY && (m.X-1) = state.AlienX))
            {state with Misiles = nuevosMisiles; EnemyState=Colisionado; ColisionTime=state.Tick; MisilesEnemigos=nuevosMisilesEnemigos; AlienState=Colisionado; AlienColisionTime=state.Tick; ProgramState=Terminated; Puntaje=state.Puntaje+1}
        | false, false -> state
    | Colisionado, _ ->
        if state.Tick - state.ColisionTime >= 25 then
            {state with EnemyState = Pausado}
        else
            state
    | _, Colisionado ->
        if state.Tick - state.AlienColisionTime >= 25 then
            {state with AlienState = Pausado}
        else
            state
    | Pausado, _ | _, Pausado -> state


let updateState state =
    state
    |> updateTick
    |> updateClock
    |> updateEnemy
    |> updateMissilAnimation
    |> updateMisilesEnemigos
    |> updateCollision
    |> updateKeyboard

let updateScreen state =
    state
    |> displayAlien
    |> displayCounter
    |> displayMissil
    |> displayMisilesEnemigos
    |> displayEnemy
    |> displayPuntaje
    |> ignore

let clearObjects state =
    state
    |> cleanAlien
    |> cleanMissil
    |> cleanMisilesEnemigos
    |> cleanEnemy
    |> ignore
let rec mainLoop state =
    let newState = updateState state
    clearObjects state
    updateScreen newState
    dormirUnRato()
    if newState.ProgramState = Running then
        mainLoop newState

Console.CursorVisible <- false
Console.Clear()


initState()
|> mainLoop

Console.Clear()
Console.CursorVisible <- true
