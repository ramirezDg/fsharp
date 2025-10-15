open System

type Suit =
    | Hearts
    | Diamonds
    | Spades
    | Clubs

type Card =
    | Ace of Suit
    | King of Suit
    | Queen of Suit
    | Jack of Suit
    | Number of int * Suit

let cardValue (card: Card) : int =
    match card with
    | Ace _ -> 14
    | King _ -> 13
    | Queen _ -> 12
    | Jack _ -> 11
    | Number (n, _) -> n

let printCard (card: Card) : string =
    let suitToString s =
        match s with
        | Hearts -> "Hearts"
        | Diamonds -> "Diamonds"
        | Spades -> "Spades"
        | Clubs -> "Clubs"
    match card with
    | Ace s -> $"Ace of {suitToString s}"
    | King s -> $"King of {suitToString s}"
    | Queen s -> $"Queen of {suitToString s}"
    | Jack s -> $"Jack of {suitToString s}"
    | Number (n, s) -> $"{n} of {suitToString s}"

let rec askSuit () : Suit =
    printfn "Choose a suit: [H]earts, [D]iamonds, [S]pades, [C]lubs:"
    let input = Console.ReadLine().Trim().ToUpper()
    match input with
    | "H" -> Hearts
    | "D" -> Diamonds
    | "S" -> Spades
    | "C" -> Clubs
    | _ ->
        printfn "Invalid input. Please try again."
        askSuit()

let rec askCard () : Card =
    printf "Enter card value (2-10, J, Q, K, Ace): "
    let valueInput = Console.ReadLine().Trim().ToUpper()
    match valueInput with
    | "ACE" -> Ace (askSuit())
    | "K" -> King (askSuit())
    | "Q" -> Queen (askSuit())
    | "J" -> Jack (askSuit())
    | n when (n |> Int32.TryParse |> fst && (int n >= 2 && int n <= 10)) ->
        Number (int n, askSuit())
    | _ ->
        printfn "Invalid card value. Please try again."
        askCard()

let rec getHand (currentHand: Card list) : Card list =
    printfn $"\n--- Card #{List.length currentHand + 1} ---"
    let newCard = askCard()
    let updatedHand = newCard :: currentHand
    printf "Do you want to add another card? (Y/N): "
    let resp = Console.ReadLine().Trim().ToUpper()
    match resp with
    | "S" -> getHand updatedHand
    | _ -> updatedHand

let valueCard card =
    match card with
    | Number (x, _) -> x
    | Ace _ -> 14
    | King _ -> 13
    | Queen _ -> 12
    | Jack _ -> 11

[<EntryPoint>]
let main argv =
    printfn "\n=== Poker Hand Builder ==="
    printfn "Ingrese las cartas que desee.\n"

    let hand = getHand [] |> List.rev

    let sortedHand = hand |> List.sortByDescending valueCard

    printfn "\n========================================"
    printfn "Your hand sorted from HIGHEST to LOWEST:"

    sortedHand
    |> List.iteri (fun i card ->
        printfn $"  {i + 1}. {printCard card}"
    )
    printfn "========================================\n"

    0


(* Repaso *)

(*

    -- let
    -- let rec
    -- if - then - else
    -- match with
    -- fun (lambdas)
    -- |>
    -- type (Discrimnminated unions)
    -- (,,,,) Tuplas
    -- [;;;;] Listas
    -- Records -> estructura de datos que podemos mezclar con diferentes tipo, son mas convenientes que las tuplas

 *)

 (* RECORDS *)


