module App.MainMenu

open App.Types

let mostrarMenu x y =
    [
        MenuCommand.NewGame,"Nuevo Juego"
        MenuCommand.LoadGame,"Cargar Juego"
        MenuCommand.Exit,"Salir"
    ]
    |> Menu.mostrarMenu x y
