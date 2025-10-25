open System

(* [1..20]
|> Seq.map (fun l -> 2*l)
|> Seq.sum
|> Console.WriteLine *)


(* En F# reduce se llama en realidad fold *)
[1..20]
|> Seq.map (fun l -> l * 2)
|> Seq.fold (fun acumulador elemento -> acumulador + elemento ) 0 // Esto es Seq.sum
|> Console.WriteLine


[1m..20m]
|> Seq.fold (fun acc i-> acc * i ) 1m
|> Console.WriteLine


let rec getEntero (msg : string) (errMsg : string) =
    printfn $"{msg}"
    let input = Console.ReadLine()
    match Decimal.TryParse input with
    | true,x -> x
    | false,_ ->
        printfn $"{errMsg}"
        getEntero msg errMsg

let rec getFloat (msg : string) (errMsg : string) =
    printfn $"{msg}"
    let input = Console.ReadLine()
    match Double.TryParse input with
    | true, x -> x
    | false, _ ->
        printfn $"{errMsg}"
        getFloat msg errMsg

let factorial number : decimal =
    [1m.. number]
    |> Seq.fold (fun acc i-> acc * i ) 1m

getEntero "Entre un float: " "Error"
|> factorial
|> Console.WriteLine


(* Fibonacci *)

let fibo n =
    [0..n]
    |> Seq.fold (fun (a,b) e -> (b, a + b)) (0,1)
    |> snd
    |> Console.WriteLine

(* fibo 10 *)

[0..10]
|> Seq.fold (fun (a,b) e -> (b, a + b)) (0,1)
|> snd
|> Console.WriteLine
