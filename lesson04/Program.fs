open System

let loop winner lives =
    let rec game (live : int)  =
        if live = 0 then
            printfn $"👽 GAME OVER 👽\nThe winning number was: {winner}"
        else
            printf "Enter a number (1 - 10): "
            let input = Console.ReadLine() |> int
            match input = winner with
            | true -> printfn "😎😎 WINNER 😎😎"
            | _ ->
                printfn "Try again! 👌"
                game (live - 1)
    game lives

printfn "Guessing Game"
printfn ""
printf "How many lives do you want?: "
let rnd = System.Random()
let winner = rnd.Next(1, 11)
let guess = loop winner

let lives = Console.ReadLine() |> int

lives |> guess // pipelyne
