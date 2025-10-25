//
// Segundo Parcial Programacion I
//
//

//
// Pregunta I
//

//1.a
// Hacer una función que tome un parametro n (int),
// que imprima en pantalla, una secuencia de n número impares.
// empezando por 1. (Ojo, implementar con listas)
//

open System
let imprimirImpares n =
    [1..n]
    |> Seq.map(fun e -> 2 * e - 1)
    |> Seq.iter Console.WriteLine
//
// 1.b
//  Dada la siguiente base de datos
//

imprimirImpares 100


let atlas = [
    "Colombia","Bogotá",100
    "USA", "Whashington DC", 10000
    "Francia", "Paris", 4000
    "Japon", "Tokyo", 8000
]
//
// Crear una funcion, que tome una lista como parametro
// ordene la lista por PIB
// Y mueste en pantalla, solo el nombre de pais.
//
// Hacer un programa con esa funcion.

let mostrarListaOrdenada lista =
    lista
    |> Seq.sortBy (fun (_,_,pib) -> pib)
    |> Seq.iter (fun (nombre,_,_) -> printfn $"{nombre}")
    //|> Seq.map (fun (nombre:string,_,_)-> nombre)
    //|> Seq.iter Console.WriteLine

atlas
|> mostrarListaOrdenada

//1.c
// Definir una base de datos de cuentas bancarias usando records
// Debe contener: Nombre de Banco, Moneda, y Saldo
//
// La moneda solo puede ser: Pesos, Dolares, Euros
//
// La base de datos debe tener mínimo 3 bancos, y debe usar cada
// una de las monedas.
//
type Moneda =
| Euros
| Pesos
| Dolares

type Cuentas= {
    Banco: string
    Moneda: Moneda
    Saldo: decimal
}

let bancos = [
    {
        Banco = "Bancolombia"
        Moneda = Pesos
        Saldo = 4_000_000_000m
    }
    {
        Banco = "JP Morgan"
        Moneda = Dolares
        Saldo = 850_000m
    }
    {
        Banco = "UBS"
        Moneda = Euros
        Saldo = 750_000m
    }
]

//
//Pregunta II
//
// Usando la base de datos de 1.c, y la siguiente tabla de conversion:
// 1 Dolar = 4000 persos
// 1 Euro = 1.18 dolares
//

// 2.a
//  Hacer un programa, que liste la base de datos de las cuentas
//  bancarias en ordern de saldo equivalente en dolares, de mayor
//  a menor. Y que al final imprima el saldo total de todas
//  las cuentas en dolares.
//

let convertirADolares moneda valor =
    match moneda with
    | Dolares -> valor
    | Pesos -> valor/4_000m
    | Euros -> valor*1.18m

bancos
|> Seq.sortBy ( fun r -> convertirADolares r.Moneda r.Saldo)
|> Seq.rev
|> Seq.iter (fun r ->
    printfn $"{r.Banco} {r.Moneda} {r.Saldo}"
)

bancos
|> Seq.map ( fun r -> convertirADolares r.Moneda r.Saldo)
|> Seq.sum
|> Console.WriteLine

//
// 2.b
//  Crear un programa para manejar una base de datos de amigos
//  La base de datos, lleva: Nombre, Apellido y Apodo.
//  Ojo: El Apodo es opcional.
//
//  El programa debe pedir los valors al usuario de la base de datos
//  y preguntar si quiere agregar mas. Cuando termine, lista
//  las columnas de la base datos, en formato pretty. Ojo con la
//  columna de apodo, si no tiene apodo la persona, debe salir
//  el mensaje: "No tiene"

type Amigo = {
    Nombre: string
    Apellido: string
    Apodo: string option
}

let obtenerDatosDeAmigo() =
    printf "Entra el nombre: "
    let nombre = Console.ReadLine()
    printf "Entra el apellido: "
    let apellido = Console.ReadLine()
    printf "Entra el apodo: "
    let r = Console.ReadLine()
    let apodo =
        if r = "" then
            None
        else
            Some r
    {
        Nombre = nombre
        Apellido = apellido
        Apodo = apodo
    }

let rec obtenerDatos lista =
    let amigo = obtenerDatosDeAmigo()
    let nuevaLista = amigo :: lista
    printf "Quieres agregar otro amigo (s/n)"
    let r = Console.ReadLine()
    if r = "n" then
        nuevaLista
    else
        obtenerDatos nuevaLista

obtenerDatos []
|> Seq.iter (fun r ->
    let apodo = r.Apodo |> Option.defaultValue "No tiene"
    printfn $"%-15s{r.Nombre} %-15s{r.Apellido} %-15s{apodo}"
)
