open System
(* let getName() =
    printf "Entra tu nombre: "
    Console.ReadLine()

let getCity (x:string) =
    printf "Entra tu ciudad favorita: "
    Console.ReadLine()
    |> fun (city: string) -> $"Hola {x}, tu ciudad favorita es {city}"

let getColor (x : string) =
    printf "Entra tu color favorito: "
    Console.ReadLine()
    |> fun (color: string) -> $"{x} y tu color favorito es {color}"

(* getName() |> getCity |> getColor |> printfn "%s" *)

let main = getName >> getCity >> getColor

main() |> printfn "%s"

 *)
(* let showTitle () =
    printfn "-----------------------------------------"
    printfn "|   Juego de Adivinar el Número         |"
    printfn "-----------------------------------------"
let game (number: int ) =
    showTitle()
    printf "Ingresa el número de intentos que deseas tener (por ejemplo, 3): "
    let lives = Console.ReadLine() |> int
    let rec guesser ( live) =
        if live <= 0 then
                printfn "Lo siento, no te quedan más intentos. El número era %d." number
        else
        printf "Adivina el número (entre 1 y 10): "
        let input = Console.ReadLine() |> int
        match input with
        | n when n = number -> printfn "¡Felicidades! Adivinaste el número."
        | live when live <= 0 -> printfn "Lo siento, no te quedan más intentos. El número era %d." number
        | _ ->
            printfn "Entrada inválida."
            guesser(live - 1)
    guesser lives

let psudoRandonNumber () =
    let random = Random()
    let number = random.Next(1, 11)
    number


let rec continiuGame () =
    printf "¿Quieres jugar de nuevo? (s/n): "
    let response = Console.ReadLine()
    match response.ToLower() with
    | "s" -> true
    | "n" -> false
    | _ ->
        printfn "Respuesta inválida. Por favor ingresa 's' para sí o 'n' para no."
        continiuGame ()
let startGame = psudoRandonNumber >> game
let rec loop () =
    startGame()
    if continiuGame() then loop()
    else printfn "Gracias por jugar. ¡Hasta luego!"

loop() *)



(* let generador = new Random()

let getGanador() =
    generador.Next(1,11)

let showTitle() =
    printfn "Bienvenido al juego de la suerte 🎰"

let adivinar vidas ganador =
    let rec loop n =
        if n <=0 then
            printfn "GAME OVER☠️"
        else
            printf "Entra un numero entre 1 y 10: "
            let guess = Console.ReadLine() |> int
            if guess = ganador then
                printfn "GANASTE!!!🍾"
            else
                printfn "Intenta de nuevo"
                loop (n-1)

    loop vidas

let mainLoop f =
    let rec loop() =
        f()
        printf "Deseas jugar de nuevo?(s/n): "
        let a = Console.ReadLine()
        if a = "s" then
            loop()
    loop()

let gameLoop = getGanador >> adivinar 3

showTitle()
|> fun() -> gameLoop
|> mainLoop *)

(* let functionSuma (a:int) (b:int) : int=
    a + b

printfn $"La suma es {(functionSuma 2 4)}" *)

let juego input =
    if (int input) = 7 then
        printf "Ganaste"
    else
        printf "Intenta de nuevo"

let hacerJuego secreto =
    fun intento ->
        if intento = secreto then
            printfn "Ganador!!"
        else
            printfn "Intenta de nuevo"

let juego1 = hacerJuego 3

let juego2 = hacerJuego 6

juego1 2
juego1 3

juego1 5
juego1 6
