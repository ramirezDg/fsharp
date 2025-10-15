open System
type Suit =
    | Hearts
    | Diamonds
    | Clubs
    | Spades

type Card =
    | Number of int*Suit
    | Jack of Suit
    | Queen of Suit
    | King of Suit
    | Ace of Suit

let card = Ace Hearts
let cardTwo = King Clubs

let ochoPicas = Number (8, Spades)
let cincoCorazones = Number (5, Hearts)


let suitToEmoji suit =
    match suit with
    | Hearts -> "♥️"
    | Diamonds -> "♦️"
    | Clubs -> "♣️"
    | Spades -> "♠️"

let cardToString card =
    match card with
    | Number(n, suit) -> sprintf "%d%s" n (suitToEmoji suit)
    | Jack suit -> sprintf "J%s" (suitToEmoji suit)
    | Queen suit -> sprintf "Q%s" (suitToEmoji suit)
    | King suit -> sprintf "K%s" (suitToEmoji suit)
    | Ace suit -> sprintf "A%s" (suitToEmoji suit)

let printCard card =
    let cardStr = cardToString card
    printfn "┌─────┐"
    printfn "│ %-3s │" cardStr
    printfn "└─────┘"


(* printCard card
printCard cardTwo
 *)
(*
type MarketCurrency =
    | COP of decimal   // Peso colombiano
    | USD of decimal   // Dólar estadounidense
    | EUR of decimal   // Euro
    | JPY of decimal   // Yen japonés

let myWalletList = [
    COP 3_000_000m
    USD 1_000m
    EUR 800m
    JPY 5_000m
]
let convertCurrency (currency: MarketCurrency) =
    match currency with
    | USD x -> x
    | COP x -> x / 3_885.24m
    | EUR x -> x * 1.1744m
    | JPY x -> x * 68.47m

myWalletList
|> List.map convertCurrency
|> List.sum
|> fun value -> printf $"The total in Dollars is: $ %0.4f{value} USD"
 *)

type MarketCurrency =
    | COP of decimal   // Peso colombiano
    | USD of decimal   // Dólar estadounidense
    | EUR of decimal   // Euro
    | JPY of decimal   // Yen japonés

let myWalletList = [
    COP 3_000_000m
    USD 1_000m
    EUR 800m
    JPY 5_000m
]
let exchangeRates = [
    ("COP", 3_885.24m)
    ("EUR", 0.8520m)
    ("JPY", 155.00m)
]

let getRate code =
    exchangeRates
    |> List.tryFind (fun (c, _) -> c = code)
    |> Option.map snd
    |> Option.defaultValue 1.0m

let convertCurrency (currency: MarketCurrency) =
    match currency with
    | USD x -> x
    | COP x -> x / getRate "COP"
    | EUR x -> x * getRate "EUR"
    | JPY x -> x / getRate "JPY"


let total =
    myWalletList
    |> List.fold (fun acc currency ->
        let value = convertCurrency currency
        printfn $"The Convertion Is: {value}"
        acc + value
    ) 0.0m

printfn $"The total in Dollars is: $ %0.4f{total} USD"


(* TODO: Investigar optimización para no recorrer la lista multiples veces *)


(* Atlas Del Mundo *)
(*
    Colombia, USA, Argentina,
    España, Alemania, Azerbaiyan, Japon
    La base de datos debe tener el pais, la capital y el numero de habitantes
 *)

let atlas = [
    ("Colombia", "Bogota DC", 52_890_000)
    ("Estados Unidos", "Washington DC", 340_100_000)
    ("Argentina", "Buenos Aires", 45_700_000)
    ("España", "Madrid", 48_810_000)
    ("Alemania", "Berlin", 83_510_000)
    ("Azerbaiyan", "Baku", 10_200_000)
    ("Japon", "Tokyo", 124_000_000)
]

let imprimirLista paises =
    paises
    |> List.iter (fun (pais, capital, poblacion) ->
        printfn "%-15s %-15s %10d" pais capital poblacion
    )

(* let atlasOrdenadoLazy = lazy (
    atlas
    |> List.sortBy (fun (_, _, poblacion) -> poblacion)
    |> List.rev
) *)

(* imprimirLista atlasOrdenadoLazy.Value *)

(* let poblacionTotalLazy = lazy (
    atlas
    |> List.fold (fun acumulado (_, _, poblacion) -> acumulado + poblacion) 0
) *)

(* atlas
|> Seq.map ( fun (p,_,_) ->
    printfn "Loading... {p}"
    p
    )
|>  *)

// printfn $"Total de habitantes: %d{poblacionTotalLazy.Value}"
