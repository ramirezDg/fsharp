open System
let printKeyInfo (keyInfo: ConsoleKeyInfo) =
    let modifiers =
        [ if keyInfo.Modifiers.HasFlag(ConsoleModifiers.Control) then yield "Ctrl"
          if keyInfo.Modifiers.HasFlag(ConsoleModifiers.Shift) then yield "Shift"
          if keyInfo.Modifiers.HasFlag(ConsoleModifiers.Alt) then yield "Alt" ]
        |> String.concat " + "
    let prefix = if modifiers <> "" then modifiers + " + " else ""
    match keyInfo.Key with
    | ConsoleKey.LeftArrow -> printfn $"{prefix}⬅️ Left Arrow"
    | ConsoleKey.RightArrow -> printfn $"{prefix}➡️ Right Arrow"
    | ConsoleKey.UpArrow -> printfn $"{prefix}⬆️ Up Arrow"
    | ConsoleKey.DownArrow -> printfn $"{prefix}⬇️ Down Arrow"
    | ConsoleKey.Escape -> printfn $"{prefix}⎋ Escape"
    | ConsoleKey.Enter -> printfn $"{prefix}⏎ Enter"
    | ConsoleKey.Spacebar -> printfn $"{prefix}␣ Space"
    | ConsoleKey.Tab -> printfn $"{prefix}⇥ Tab"
    | ConsoleKey.Backspace -> printfn $"{prefix}⌫ Backspace"
    | ConsoleKey.Delete -> printfn $"{prefix}⌦ Delete"
    | ConsoleKey.Home -> printfn $"{prefix}↖ Home"
    | ConsoleKey.End -> printfn $"{prefix}↘ End"
    | ConsoleKey.PageUp -> printfn $"{prefix}⇞ Page Up"
    | ConsoleKey.PageDown -> printfn $"{prefix}⇟ Page Down"
    | ConsoleKey.Insert -> printfn $"{prefix}Insert"
    | ConsoleKey.F1 -> printfn $"{prefix}F1"
    | ConsoleKey.F2 -> printfn $"{prefix}F2"
    | ConsoleKey.F3 -> printfn $"{prefix}F3"
    | ConsoleKey.F4 -> printfn $"{prefix}F4"
    | ConsoleKey.F5 -> printfn $"{prefix}F5"
    | ConsoleKey.F6 -> printfn $"{prefix}F6"
    | ConsoleKey.F7 -> printfn $"{prefix}F7"
    | ConsoleKey.F8 -> printfn $"{prefix}F8"
    | ConsoleKey.F9 -> printfn $"{prefix}F9"
    | ConsoleKey.F10 -> printfn $"{prefix}F10"
    | ConsoleKey.F11 -> printfn $"{prefix}F11"
    | ConsoleKey.F12 -> printfn $"{prefix}F12"
    | _ -> printfn $"{prefix}Other key: {keyInfo.Key}"

printfn "Press a key: "
while true do
    let keyInfo = Console.ReadKey(true)
    printKeyInfo keyInfo
