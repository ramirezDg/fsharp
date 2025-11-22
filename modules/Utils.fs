module App.Utils
open System

let sleep() =
    Threading.Thread.Sleep 40

let displayMessage x y color (msg : string) =
    Console.SetCursorPosition(x, y)
    Console.ForegroundColor <- color
    Console.WriteLine msg
