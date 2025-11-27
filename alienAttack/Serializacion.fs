module App.Serializacion
open System
open System.IO
open System.Text.Json
open App.Types

let fileNameGameState = "game_state.json"

type StateDto = {
    AlienX: int
    AlienY: int
    AlienState: string
    AlienVidas: int
    AlienColisionTime: int
    Counter: int
    Misiles: (int * int) list
    Tick: int
    Width: int
    Height: int
    ProgramState: string
    EnemyX: int
    EnemyY: int
    EnemySpeed: float
    EnemyState: string
    ColisionTime: int
    Puntaje: int
    MisilesEnemigos: (int * int) list
    UltimoDisparo: int
    EnemyAngle: float
}

let objectStateToString os =
    match os with
    | Visible -> "Visible"
    | Colisionado -> "Colisionado"
    | Pausado -> "Pausado"

let stringToObjectState s =
    match s with
    | "Visible" -> Visible
    | "Colisionado" -> Colisionado
    | "Pausado" -> Pausado
    | _ -> Visible

let programStateToString ps =
    match ps with
    | Running -> "Running"
    | Paused -> "Paused"
    | Terminated -> "Terminated"

let stringToProgramState s =
    match s with
    | "Running" -> Running
    | "Paused" -> Paused
    | "Terminated" -> Terminated
    | _ -> Running

let missilToTuple m = (m.X, m.Y)
let tupleToMissil (x, y) = { X = x; Y = y }

let stateToDto (s: State) : StateDto =
    {
        AlienX = s.AlienX
        AlienY = s.AlienY
        AlienState = objectStateToString s.AlienState
        AlienVidas = s.AlienVidas
        AlienColisionTime = s.AlienColisionTime
        Counter = s.Counter
        Misiles = List.map missilToTuple s.Misiles
        Tick = s.Tick
        Width = s.Width
        Height = s.Height
        ProgramState = programStateToString s.ProgramState
        EnemyX = s.EnemyX
        EnemyY = s.EnemyY
        EnemySpeed = s.EnemySpeed
        EnemyState = objectStateToString s.EnemyState
        ColisionTime = s.ColisionTime
        Puntaje = s.Puntaje
        MisilesEnemigos = List.map missilToTuple s.MisilesEnemigos
        UltimoDisparo = s.UltimoDisparo
        EnemyAngle = s.EnemyAngle
    }

let dtoToState (dto: StateDto) : State =
    {
        AlienX = dto.AlienX
        AlienY = dto.AlienY
        AlienState = stringToObjectState dto.AlienState
        AlienVidas = dto.AlienVidas
        AlienColisionTime = dto.AlienColisionTime
        Counter = dto.Counter
        Misiles = List.map tupleToMissil dto.Misiles
        Tick = dto.Tick
        Width = dto.Width
        Height = dto.Height
        ProgramState = stringToProgramState dto.ProgramState
        EnemyX = dto.EnemyX
        EnemyY = dto.EnemyY
        EnemySpeed = dto.EnemySpeed
        EnemyState = stringToObjectState dto.EnemyState
        ColisionTime = dto.ColisionTime
        Puntaje = dto.Puntaje
        MisilesEnemigos = List.map tupleToMissil dto.MisilesEnemigos
        UltimoDisparo = dto.UltimoDisparo
        EnemyAngle = dto.EnemyAngle
    }

let serializarEstadoJuego (estado: State) : string =
    let dto = stateToDto estado
    JsonSerializer.Serialize(dto)

let deserializarEstadoJuego (json: string) : State =
    let dto = JsonSerializer.Deserialize<StateDto>(json)
    dtoToState dto

let guardarEstadoJuego (estado: State) =
    let json = serializarEstadoJuego estado
    File.WriteAllText(fileNameGameState, json)

let cargarEstadoJuego () : State option =
    if File.Exists(fileNameGameState) then
        let json = File.ReadAllText(fileNameGameState)
        try
            Some (deserializarEstadoJuego json)
        with _ -> None
    else
        None
