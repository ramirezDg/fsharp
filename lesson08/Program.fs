open System

type Document =
| TI of decimal
| Cedula of decimal
| CedulaExtranjeria of decimal
| NIT of decimal

let printIdentification (doc: Document, num: decimal) =
    let tipo =
        match doc with
        | TI -> "TI"
        | Cedula -> "ID Card"
        | CedulaExtranjeria -> "Foreigner ID Card"
        | NIT -> "NIT"
    printfn $"Your document type is: {tipo}"
    printfn $"Your document number is: {num}"

let printSelectedDocument (doc: Document) =
    match doc with
    | TI -> printfn "You selected: TI"
    | Cedula -> printfn "You selected: ID Card"
    | CedulaExtranjeria -> printfn "You selected: Foreigner ID Card"
    | NIT -> printfn "You selected: NIT"

let printTypeDocument () =
    printfn "Document Types:"
    printfn "TI"
    printfn "ID Card"
    printfn "Foreigner ID Card"
    printfn "NIT"

let errorMsg = "Invalid input"

let rec getDecimal (msg: string) =
    printfn $"{msg}"
    let res = Console.ReadLine()
    match Decimal.TryParse res with
    | true,x -> x
    | false,_ ->
        printfn $"{errorMsg}"
        getDecimal msg
let rec getDocument () =
    printTypeDocument()
    printfn "Enter the document type (ti, cc, ce, nit): "
    match Console.ReadLine() with
    | "ti" -> TI
    | "cc" -> Cedula
    | "ce" -> CedulaExtranjeria
    | "nit" -> NIT
    | _ ->
        printfn $"{errorMsg}"
        getDocument()

let getIdentification () =
    let doc = getDocument()
    printSelectedDocument doc
    let num = getDecimal "Enter the document number: "
    let result = (doc, num)
    printIdentification result
    result

getIdentification() |> ignore
