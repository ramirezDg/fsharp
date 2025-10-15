open System

let rec adivinar ganador vidas =
    if vidas = 0 then
        printfn "GAME OVER👽"
    else
        printf "Entra un numero: "
        let guess = Console.ReadLine()
        if guess = ganador then
            printfn "Ganaste!! 🍾🥂"
        else
            printfn "Lo siento, intenta de nuevo"
            adivinar ganador (vidas-1)

printf "¿Cuántos intentos quieres tener? "
let vidas = int (Console.ReadLine())

let ganador = "6"

let juego = adivinar ganador
juego vidas
