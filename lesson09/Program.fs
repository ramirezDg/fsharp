open System

type Country = {
    Name: string // Fields
    Capital: string
    Population: int
}

let db = [
    { Name = "Colombia"; Capital = "Bogota DC"; Population = 52_890_000 }
    { Name = "Estados Unidos"; Capital = "Washington DC"; Population = 340_100_000 }
    { Name = "Argentina"; Capital = "Buenos Aires"; Population = 45_700_000 }
    { Name = "España"; Capital = "Madrid"; Population = 48_810_000 }
    { Name = "Alemania"; Capital = "Berlin"; Population = 83_510_000 }
    { Name = "Azerbaiyan"; Capital = "Baku"; Population = 10_200_000 }
    { Name = "Japon"; Capital = "Tokyo"; Population = 124_000_000 }
]
(* List.Find *)
(* Seq.Find  -> lazy - peresoso*)

(* DRY -> Dont repeat your self*)
(*

 *)
let searchCountry (list: Country list) (country: string) =
    list
    |> Seq.filter (fun countryinput -> String.Equals(countryinput.Name, country, StringComparison.OrdinalIgnoreCase))
    |> Seq.tryHead

printfn "Pais a buscar: "
Console.ReadLine()
|> searchCountry db
|> function
    | Some c -> printfn $"Nombre: {c.Name}\nCapital: {c.Capital}\nHabitantes: {c.Population}"
    | None -> printfn "No se encontro nada."


(* TODO: Como hacer para buscar cualquier pais sin importar como lo escrina *)

(* OPTION es una forma de reportar errores *)


(* Programa simple de manipulación de records *)
