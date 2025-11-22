module App.Utils
open System
let dormirUnRato() =
    System.Threading.Thread.Sleep 40

let displayMessage x y color (mensaje:string) =
    Console.SetCursorPosition(x,y)
    Console.ForegroundColor <- color
    Console.Write mensaje

let printLetra x y color letra =
    letra
    |> Array.iteri (fun i c -> displayMessage x (y+i) color c)

let displayMessageGigante x y color (mensaje:string) =
    mensaje.ToUpper()
    |> Seq.map Letras.encontrarLetra
    |> Seq.iteri (fun i l -> printLetra (x+i*7) y color l)
    