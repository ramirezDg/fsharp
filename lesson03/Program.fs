open System

(* Preguntar al usuario 2 números y decir cual es mayor *)

(* let rec numberForTable () =
    printfn "Ingrese un número del 1 al 12"
    let input = Console.ReadLine()
    match (int input) with
    | num when num >= 1 && num <= 12 -> num
    | _ ->
        printfn "Entrada inválida. Por favor, ingrese un número del 1 al 12."
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

printMultiplicationTable (numberForTable ()) *)

(* Preguntar al usuario 2 números y decir cual es mayor *)

(* printf "Ingrese el primer número:"
let number1 = float (Console.ReadLine())
printf "Ingrese el segundo número:"
let number2 = float (Console.ReadLine())

let compareNumbers n1 n2 =
    match n1, n2 with
    | a, b when a > b -> printfn $"El número {a} es mayor que {b}."
    | a, b when a < b -> printfn $"El número {b} es mayor que {a}."
    | _ -> printfn "Ambos números son iguales."

compareNumbers number1 number2 *)


(* Juego de adivinanza de números *)

(* let winerNumber = Random().Next(1, 10) *)

open System

let winerNumber = 7

let rec game () =
    printf "Adivina el número (entre 1 y 10): "
    let input = int (Console.ReadLine())
    if input = winerNumber then
        printfn "¡Felicidades! Has adivinado el número."
    elif input < 1 || input > 10 then
        printfn "Número fuera de rango. Intenta de nuevo."
        game ()
    else
        printfn "Número incorrecto. Intenta de nuevo."
        game ()

game ()