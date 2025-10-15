open System

let rec numberForTable () =
    printfn "Ingrese un número del 1 al 9"
    let input = Console.ReadLine()
    match (int input) with
    | num when num >= 1 && num <= 9 -> num
    | _ ->
        printfn "Entrada inválida. Por favor, ingrese un número del 1 al 9."
        numberForTable ()

let rec printMultiplicationTable number =
    printfn $"Tabla de multiplicar del {number}:"
    let rec loop i =
        if i <= 12 then
            printfn $"{number} x {i} = {number * i}"
            loop (i + 1)
        else
            printfn ""
    loop 1

printMultiplicationTable (numberForTable ())

