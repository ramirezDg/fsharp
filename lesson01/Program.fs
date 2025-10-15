// For more information see https://aka.ms/fsharp-console-apps
let a = 2.0
let b = 2.0

let f a b =
    a + b

let res = f a b

let stringRes = res.ToString()

printfn $"The result is: {stringRes}"

