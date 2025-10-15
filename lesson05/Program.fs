(* open System
 *)
(*
let msg (text : string) =
    $"Hello {text}"

let getName() =
    Console.Write("Enter your name: ")
    Console.ReadLine()

(* let writeAge (age : int) =
    Console.WriteLine($"The doble age is: {age}") *)
let getAge() =
    Console.Write("Enter your age: ")
    Console.ReadLine() |> int

(* let dobleAge age =
    age * 2 *)

getName()
|> msg
|> Console.WriteLine
|> getAge
|> (fun (age : int) -> age * 2 )
|> (fun (age : int) -> Console.WriteLine($"The doble age is: {age}"))
|> Console.WriteLine
 *)

(* let table n h =
    let rec aux i =
        if i <= h then
            printfn $"{n} x {i} = {n * i}"
            aux (i + 1)
    aux 1

printfn "Enter a number: "
let n = Console.ReadLine() |> int
printfn "Enter the height of the table: "
let h = Console.ReadLine() |> int
table n h *)

(* open System
let tablaDelSiete n =
    let result = 7 * n
    $"7x{n}={result}"

let ciclo10 f=
    let rec loop n =
        if n <= 10 then
            f n
            loop (n+1)

    loop 1

ciclo10
    (fun n ->
        n |> tablaDelSiete |> Console.WriteLine
    ) *)


open System
let multiplicationTableF n h =
    let rec aux i =
        if i <= h then
            printfn $"{n} x {i} = {n * i}"
            aux (i + 1)
    aux 1


printfn "¿Which multiplication table do you want?: "
Console.ReadLine()
|> int
|> fun n ->
    printfn "Enter the height of the table: "
    multiplicationTableF n (Console.ReadLine() |> int)