module App.MenuPausa

open App.Types

let mostrarMenu x y =
    [
        PauseCommand.Continue, "Continuar Juego"
        PauseCommand.SaveGame, "Grabar Juego"
        PauseCommand.Exit, "Salir"
    ]
    |> Menu.mostrarMenu x y
