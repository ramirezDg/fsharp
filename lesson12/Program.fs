open System

(* [1..20]
|> Seq.fold ( fun (a,b) _ -> (b, a + b) ) (0,1)
|> snd
|> Console.WriteLine *)


let printFibonnacci n =
    [1..(n - 2)]
    |> Seq.fold ( fun acc _ ->
        match  acc with
        | a :: b :: resto -> ( a + b) :: acc
        | _ -> acc
    ) [1;0]
    |> Seq.rev

printFibonnacci 20
|> Seq.iter (fun i -> printf $"{i} ")


(* LIST COMPRENHENSIONS *)

type Palo =
| Diamantes
| Picas
| Treboles
| Corazones

type Carta =
| K of Palo
| Q of Palo
| J of Palo
| As of Palo
| Numero of int*Palo

let baraja = [
    for palo in [Diamantes; Picas; Treboles; Corazones] do
        for valor in [2..10] do
            Numero (valor, palo)
        K palo
        Q palo
        J palo
        As palo
]


(* Listas *)

(*
    Es muy lento acceder a un elemento de una lista
*)

let listData = [1..10]

let ultimoElemento = listData |> List.last


(* Nuevo Tipo De Dato *)

(* Array *)

let arrayData = [|1..10|]

let arrayaEndElement = arrayData |> Array.last


(* Ventajas y Desventajas *)

(*
    List -> Ventaja:  Agregar un nuevo elemento es bastante reapido Toma Tiempo O(1) Tiempo Constante
    List -> Ventaja: Agrega un elemento no incrementa la memoraia, en mas de lo que el nuevo elemento requiere
    List -> Desventaja: una lista de n elementos consume mas meoria que un array de n elementos

    Array -> Ventaja: Accede a cualquier elemento del array es super rapido Toma Tiempo O(1) Tiempo Constante
    Array -> Desventaja: Agregar un nuevo elemento, duplica el uso de memoria y es muy lento. Tiempo Tipo O(2*n)

    List: [4;8;12:16]

*)
