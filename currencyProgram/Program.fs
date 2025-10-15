(* open System
let generador = new Random(723)

let tirarMoneda() =
    generador.Next(0,2)

let parseUserChoice choice =
    match choice with
    | "cara" -> 0
    | "sello" -> 1
    | _ -> 2

let rec coinGame attempts wins =
    if attempts = 0 then
        printfn "Juego terminado! Tu puntaje fue: %d" wins
    else
        let guess =
            let rec loop () =
                printf "Quieres cara o sello ? "
                match Console.ReadLine() |> parseUserChoice with
                | 0 | 1 as valid -> valid
                | _ ->
                    printfn "Entrada invalida"
                    loop()
            loop()
        let moneda = tirarMoneda()
        match guess = moneda with
        | true ->
            printfn "Ganaste!!!"
            coinGame (attempts - 1) (wins + 1)
        | false ->
            printfn "Lo siento eres un perdedor"
            coinGame (attempts - 1) wins

printf "¿Cuántas veces quieres jugar? "
let attempts =
    match (Console.ReadLine() |> int) with
    | n when n > 0 -> n
    | _ ->
        printfn "Entrada inválida, usando 10 intentos por defecto."
        10

coinGame attempts 0 *)


open System

//
// El juego tiene 2 etapas.
// Primera etapa: OFF
// -Se gana con 7
// -Se pierde con 2
// - Si sale 4,5,6,8,9,10, ese es el juego.
// se pasa a la etapa ON
//
// - Se gana con el Juego (X)
// - Se pierde con 7
//
let generador = new Random()
let tirarDado() =
    generador.Next(1,7)
let tirarDados() =
    tirarDado()+tirarDado()
let lanzarDados() =
    printf "Presiona enter para lanzar..."
    Console.ReadLine() |> ignore
    tirarDados()
let showWelcome() =
    printfn "Bienvenido al Casino!!🎰"
let jugarOff (_,_,ganancia) dados =
    match dados with
    | 7 ->
        printfn "Winner, winner, chicken dinner!!"
        false,0,ganancia+1
    | 2 ->
        printfn "Perdiste, lo siento1"
        false,0,ganancia-1
    | 3 | 11 | 12 ->
        printfn "No pasa nada"
        false,0,ganancia
    | x ->
        printfn $"Pasamos a la fase ON, con el {x}"
        true,x,ganancia

let jugarOn (_,winner,ganancia) dados =
    match dados with
    | 7 ->
        printfn "Lo siento, perdiste! regresando a OFF"
        false,0,ganancia-1
    | x when x = winner ->
        printfn "Winner, winner, chicken dinner! regresando a OFF"
        false,0,ganancia+1
    | _ ->
        printfn "No pasa nada"
        true,winner,ganancia

let evaluarJuego (gamePhase,winner,ganancia) dados =
    match gamePhase with
    | false -> jugarOff (gamePhase,winner,ganancia) dados
    | true -> jugarOn (gamePhase,winner,ganancia) dados

let rec gameLoopInit (gamePhase:bool,winner:int,ganancia:int)=
    let dados = lanzarDados()
    printfn $"Salió el {dados}"
    let newState = evaluarJuego (gamePhase,winner,ganancia) dados
    let (nextPhase, nextWinner, nextGanancia) = newState
    printfn $"Tu balance es {nextGanancia}"
    if not nextPhase then
        printf "¿Deseas seguir jugando? (s/n): "
        match Console.ReadLine() with
        | s when s.Trim().ToLower() = "s" -> gameLoopInit newState
        | _ -> printfn $"¡Gracias por jugar! Tu balance final es {nextGanancia}"
    else
        gameLoopInit newState


let rec pedirCantidad () =
    printf "¿Cuánto dinero deseas ingresar para apostar? "
    match Int32.TryParse(Console.ReadLine()) with
    | (true, n) when n > 0 -> n
    | _ ->
        printfn "Cantidad inválida. Debe ser un número positivo."
        pedirCantidad()

let cantidadInicial = pedirCantidad()

let rec gameLoop (gamePhase:bool,winner:int,ganancia:int)=
    if ganancia <= 0 then
        printfn "¡Te has quedado sin dinero! El juego ha terminado."
        printfn "Tu balance final es 0"
    else
        let dados = lanzarDados()
        printfn $"Salió el {dados}"
        let newState = evaluarJuego (gamePhase,winner,ganancia) dados
        let (nextPhase, nextWinner, nextGanancia) = newState
        let nextGanancia = if nextGanancia < 0 then 0 else nextGanancia
        printfn $"Tu balance es {nextGanancia}"
        if not nextPhase then
            printf "¿Deseas seguir jugando? (s/n): "
            match Console.ReadLine() with
            | s when s.Trim().ToLower() = "s" -> gameLoop (nextPhase, nextWinner, nextGanancia)
            | _ -> printfn $"¡Gracias por jugar! Tu balance final es {nextGanancia}"
        else
            gameLoop (nextPhase, nextWinner, nextGanancia)

gameLoop (false,0,cantidadInicial)
