// ===========================================
// Fundamentos de F#
// ===========================================

// 1. Qué es F#
// Lenguaje funcional que soporta estilos imperativos y orientados a objetos.
// Parte del ecosistema .NET, seguro, conciso y con inmutabilidad por defecto.

// 2. Declaración de valores
let nombre = "Daniel"           // Valor inmutable
let mutable contador = 0        // Valor mutable (no recomendado por defecto)
contador <- contador + 1

// 3. Tipos
let edad = 24           // int
let altura = 1.76       // float
let activo = true       // bool
let peso: float = 78.5  // Tipo explícito

// 4. Funciones
let cuadrado x = x * x
let suma a b = a + b
let multiplicar (x: int) (y: int) : int = x * y

// 5. Estructuras de control
let esPar numero =
    if numero % 2 = 0 then
        "Es par"
    else
        "Es impar"

let calificacion nota =
    match nota with
    | 10 -> "Excelente"
    | 8 | 9 -> "Muy bien"
    | 6 | 7 -> "Bien"
    | _ -> "Necesita mejorar"

// 6. Colecciones
let numeros = [1; 2; 3; 4]                      // Lista inmutable
let arreglo = [|1; 2; 3|]                        // Array mutable
arreglo.[0] <- 10
let mapa = Map.ofList [ ("Colombia", "Bogotá"); ("Francia", "París") ]

// 7. Operaciones funcionales sobre colecciones
let cuadrados = numeros |> List.map (fun x -> x * x)
let pares = numeros |> List.filter (fun x -> x % 2 = 0)
let sumaNumeros = numeros |> List.reduce (+)

// 8. Tipos personalizados
let persona = ("Daniel", 24) // Tupla

type Persona = { Nombre: string; Edad: int }
let daniel = { Nombre = "Daniel"; Edad = 24 }

type Figura =
    | Circulo of radio: float
    | Rectangulo of ancho: float * alto: float

let area figura =
    match figura with
    | Circulo r -> System.Math.PI * r * r
    | Rectangulo (a, h) -> a * h

// 9. Módulos
module Matematica =
    let suma a b = a + b
    let resta a b = a - b

open Matematica
printfn "%d" (suma 3 4)

// 10. Ejemplo completo
type Producto = { Nombre: string; Precio: float }

let aplicarDescuento producto descuento =
    { producto with Precio = producto.Precio * (1.0 - descuento) }

let productos = [
    { Nombre = "Café"; Precio = 5.0 }
    { Nombre = "Vino"; Precio = 20.0 }
]

let productosConDescuento =
    productos |> List.map (fun p -> aplicarDescuento p 0.1)

productosConDescuento
|> List.iter (fun p -> printfn "%s: %.2f" p.Nombre p.Precio)