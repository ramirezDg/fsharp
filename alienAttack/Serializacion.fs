module App.Serializacion
open System
open System.IO
open System.Text.Json
open App.Types

let fileNameGameState = "game_state.json"

let serializarEstadoJuego (estado: State) : string =
    JsonSerializer.Serialize(estado)

let deserializarEstadoJuego (json: string) : State =
    JsonSerializer.Deserialize<State>(json)

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
