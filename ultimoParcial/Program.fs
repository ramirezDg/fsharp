open System
let factorial (number:int) = 
    [1..number]
    |> List.fold (fun acc x -> acc * x) 1

// Ejemplo de uso
let result = factorial 5  // result será 120

let diccionario = 
    [
        "mesa", "table"
        "lapiz", "pencil"
        "sobre", "on"
        "la", "the"
        "amarillo", "yellow"
        "esta", "is"
    ]
    |> Map.ofList

let traducirPalabra (palabra: string) = 
    diccionario
    |> Map.tryFind palabra
    |> Option.defaultValue palabra

let traducirFrase (frase: string) = 
    frase.ToLower().Split(' ')
    |> Array.map traducirPalabra
    |> String.concat " "

traducirFrase "La lapiz amarillo mesa esta sobre la mesa"
|>Console.WriteLine


let generator = new Random()

let rec generatorNumerroUnico n numeros = 
    let nuevoNumero = generator.Next(1, n + 1)
    if numeros |> Set.contains nuevoNumero then
        generatorNumerroUnico n numeros
    else
        nuevoNumero


