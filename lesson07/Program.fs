open System

let randomGenerate = new Random()

let rollDice () =
    randomGenerate.Next(2, 7)

let twoDice () = rollDice() + rollDice()

(*
    El juego tiene 2 etapas.
    Primera etapa: OFF
    - Se gana con 7
    - Se gan con 2
    - Si sale 4,5,6,8,9,10 ese es el juego
    Se pasa a la etapa ON
    - Se gana con el juego (x)
    - Se pierde con 7

 *)

let gameStage () =
    printfn "[OFF]"
    let rec looGame () =
        let turn = twoDice()
        printfn $"[{turn}]"
        match turn with
        | 7 ->
            printfn "[Winner]"
            looGame()
        | 2 ->
            printfn "[Loser]"
            looGame()
        | 3 | 11 | 12 ->
            printfn "[Continue]"
            looGame()
        | _ ->
            printfn "[ON]"
    looGame()

printfn "Welcome to the Craps Game!"
printfn ""
let rec crapsGame() =
    printf "Press Enter to play.\n"
    let key = Console.ReadKey(true)
    match key.Key with
    | ConsoleKey.Enter -> gameStage()
    | _ ->
        printfn "\n <Incorrect Input> \n"
        crapsGame()

crapsGame()


let functionTuple () =
    let (name, lastname,age,document) 
