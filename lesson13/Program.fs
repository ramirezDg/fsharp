open System
// Template de consola interactivo y visualmente atractivo
type StateSystem = {
    Foreground: ConsoleColor
    Background: ConsoleColor
    CursorLeft: int
    CursorTop: int
    WindowTitle: string
}

let getCurrentState () = {
    Foreground = Console.ForegroundColor
    Background = Console.BackgroundColor
    CursorLeft = Console.CursorLeft
    CursorTop = Console.CursorTop
    WindowTitle = Console.Title
}

let setState (state: StateSystem) =
    Console.ForegroundColor <- state.Foreground
    Console.BackgroundColor <- state.Background
    Console.SetCursorPosition(state.CursorLeft, state.CursorTop)
    Console.Title <- state.WindowTitle

let initStateSystem = getCurrentState()

let printCentered (msg: string) (color: ConsoleColor) =
    let x = (Console.WindowWidth - msg.Length) / 2
    let y = Console.CursorTop
    Console.SetCursorPosition(x, y)
    Console.ForegroundColor <- color
    Console.WriteLine msg

let showColorOptions () =
    Console.WriteLine "\nColores disponibles:"
    for color in System.Enum.GetValues(typeof<ConsoleColor>) do
        let c = color :?> ConsoleColor
        Console.ForegroundColor <- c
        Console.WriteLine ($"{int c}: {c}")
    Console.ResetColor()

let rec askColor (prompt: string) : ConsoleColor =
    Console.Write ($"{prompt} (número): ")
    let input = Console.ReadLine()
    match System.Int32.TryParse(input) with
    | true, v when v >= 0 && v <= 15 ->
        enum<ConsoleColor>(v)
    | _ ->
        Console.ForegroundColor <- ConsoleColor.Red
        Console.WriteLine "Entrada inválida. Intente de nuevo."
        Console.ResetColor()
        askColor prompt

// Inicio del programa
Console.Clear()
printCentered "╔══════════════════════════════════════╗" ConsoleColor.Cyan
printCentered "║   Personaliza tu consola en F# :)   ║" ConsoleColor.Cyan
printCentered "╚══════════════════════════════════════╝" ConsoleColor.Cyan

showColorOptions()
let bgColor = askColor "Elige el color de fondo"
let fgColor = askColor "Elige el color de texto"

Console.BackgroundColor <- bgColor
Console.Clear()
Console.ForegroundColor <- fgColor

printCentered "" ConsoleColor.Gray
printCentered "★ Ejemplo de texto centrado ★" fgColor
printCentered "" ConsoleColor.Gray
printCentered "Colores aplicados correctamente!" fgColor
printCentered "" ConsoleColor.Gray

// Ejemplo de varios colores
let demoMsg = "Paleta de colores: "
printCentered demoMsg fgColor
for color in System.Enum.GetValues(typeof<ConsoleColor>) do
    let c = color :?> ConsoleColor
    Console.ForegroundColor <- c
    Console.Write ($"{c} ")
Console.WriteLine ""


// Función para mover un emoji con las flechas
let moveEmoji () =
    let emoji = "👾"
    Console.CursorVisible <- false
    let clearAt (x, y) =
        Console.SetCursorPosition(x, y)
        Console.Write(" ")
    let drawAt (x, y) =
        Console.SetCursorPosition(x, y)
        Console.Write(emoji)
    let rec loop x y =
        drawAt (x, y)
        if Console.KeyAvailable then
            let key = Console.ReadKey(true)
            clearAt (x, y)
            match key.Key with
            | ConsoleKey.LeftArrow when x > 0 -> loop (x - 1) y
            | ConsoleKey.RightArrow when x < Console.WindowWidth - 2 -> loop (x + 1) y
            | ConsoleKey.UpArrow when y > 0 -> loop x (y - 1)
            | ConsoleKey.DownArrow when y < Console.WindowHeight - 1 -> loop x (y + 1)
            | ConsoleKey.Escape -> ()
            | _ -> loop x y
        else
            loop x y
    loop (Console.WindowWidth / 2) (Console.WindowHeight / 2)
    Console.CursorVisible <- true

printCentered "\nAhora puedes mover el emoji con las flechas (ESC para salir)" ConsoleColor.Yellow
moveEmoji()

// Restaurar estado inicial
Console.ForegroundColor <- initStateSystem.Foreground
Console.BackgroundColor <- initStateSystem.Background
Console.Title <- initStateSystem.WindowTitle

printCentered "\nPresiona ENTER para salir..." ConsoleColor.DarkGray
Console.ReadLine() |> ignore
