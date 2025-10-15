open System
open System.IO
open System.Text.Json

type Friend = {
    Name: string
    LastName: string
    NickName: string option
    Age: decimal
    Phone: decimal
    Email: string
}

type StateProgram = {
    DataBase: Friend list
}

let fileNameDb = "file.json"
let rec getInputDecimal (prompt: string) =
    printf "%s" prompt
    match Decimal.TryParse(Console.ReadLine()) with
    | true, value -> value
    | false, _ ->
        printfn "Error: Please enter a valid decimal number."
        getInputDecimal prompt

let rec getValidEmail (prompt: string) =
    printf "%s" prompt
    let email = Console.ReadLine()
    let isValid =
        email.Contains("@") && email.Contains(".") && email.Length > 5
    match isValid with
    | true -> email
    | false ->
        printfn "Error: Please enter a valid email address."
        getValidEmail prompt

let rec getInt (prompt: string) =
    printf "%s" prompt
    match Int32.TryParse(Console.ReadLine()) with
    | true, value -> value
    | false, _ ->
        printfn "Error: Please enter a valid integer."
        getInt prompt

let getDataFriend() =
    printf "Name: "
    let name = Console.ReadLine()

    printf "Last Name: "
    let lastName = Console.ReadLine()

    printf "NickName: "
    let isNickName =
        Console.ReadLine()
        |> fun nickName -> if nickName = "" then None else Some nickName

    let email = getValidEmail "Email: "

    let phone = getInputDecimal "Phone: "

    let age = getInputDecimal "Age: "

    {
        Name = name
        LastName = lastName
        NickName = isNickName
        Age = age
        Phone = phone
        Email = email
    }

let printFriend friend =
    printfn "%-15s %-18s %-15s %6.0f %14.0f %-35s"
        friend.Name
        friend.LastName
        (match friend.NickName with Some n -> n | None -> "(N/A)")
        friend.Age
        friend.Phone
        friend.Email

let rec createDb listFriends =
    let friend = getDataFriend()
    let newList = friend :: listFriends
    printf "Do you want to add another friend (y/n): "
    if (Console.ReadLine() = "n") then
        newList
    else
        createDb newList
let serializarLista lista =
    JsonSerializer.Serialize(lista)
let deserializarLista (json: string) =
    JsonSerializer.Deserialize<Friend list>(json)
let readDbFriends fileName =
    File.ReadAllText(fileName)
    |> deserializarLista
let getDbFriends data =
    data
    |> Seq.sortBy (fun r -> r.LastName)
    |> Seq.iter printFriend
let searchfriend list  =
    printf "Enter your friend's first name: "
    let nameS = Console.ReadLine()

    printf "Enter your friend's last name: "
    let lastNameS = Console.ReadLine()

    list
    |> List.tryFind (fun r ->
        String.Equals(r.Name, nameS, StringComparison.OrdinalIgnoreCase) &&
        String.Equals(r.LastName, lastNameS, StringComparison.OrdinalIgnoreCase)
    )
let searchDbfriend list =
    match searchfriend list with
    | None -> printfn "404 : Friend not found"
    | Some x -> printFriend x
let addDbFriend list =
    let newFriend = getDataFriend()
    let updatedList = newFriend :: list
    File.WriteAllText(fileNameDb, updatedList |> serializarLista)
    updatedList
let rec menuEditFriend originalFriend editedFriend list =
    printfn "\nCurrent Friend Information:"
    printFriend { editedFriend with NickName = match editedFriend.NickName with Some n -> Some n | None -> Some "(N/A)" }
    printfn "\nSelect the field to modify:"
    printfn " 1. Name"
    printfn " 2. Last Name"
    printfn " 3. NickName"
    printfn " 4. Age"
    printfn " 5. Phone"
    printfn " 6. Email"
    printfn " 7. Save"
    printfn " 8. Cancel"

    let opt = getInt "Select an option (1-8): "

    match opt with
    | 1 ->
        printf "Enter new name: "
        let newFriend = { editedFriend with Name = Console.ReadLine() }
        menuEditFriend originalFriend newFriend list
    | 2 ->
        printf "Enter new last name: "
        let newFriend = { editedFriend with LastName = Console.ReadLine() }
        menuEditFriend originalFriend newFriend list
    | 3 ->
        printf "Enter new nickname (leave blank for none): "
        let newNick = Console.ReadLine()
        let newFriend = if newNick = "" then { editedFriend with NickName = None } else { editedFriend with NickName = Some newNick }
        menuEditFriend originalFriend newFriend list
    | 4 ->
        let newAge = getInputDecimal "Enter new age: "
        let newFriend = { editedFriend with Age = newAge }
        menuEditFriend originalFriend newFriend list
    | 5 ->
        let newPhone = getInputDecimal "Enter new phone: "
        let newFriend = { editedFriend with Phone = newPhone }
        menuEditFriend originalFriend newFriend list
    | 6 ->
        let newEmail = getValidEmail "Enter new email: "
        let newFriend = { editedFriend with Email = newEmail }
        menuEditFriend originalFriend newFriend list
    | 7 ->
        let updatedList =
            list
            |> List.map (fun f ->
                if f.Name = originalFriend.Name && f.LastName = originalFriend.LastName then editedFriend else f
            )
        File.WriteAllText(fileNameDb, updatedList |> serializarLista)
        printfn "Friend updated successfully."
        updatedList
    | 8 ->
        printfn "Edit cancelled."
        list
    | _ ->
        printfn "Invalid option."
        menuEditFriend originalFriend editedFriend list
let updateDbFriend state =
    let friendModifi = searchfriend state.DataBase
    match friendModifi with
    | None ->
        printfn "Friend does not exist"
        state.DataBase
    | Some friendModifi ->
        menuEditFriend friendModifi friendModifi state.DataBase

let deleteDbFriend list =
    match searchfriend list with
    | None ->
        printfn "Friend does not exist"
        list
    | Some friendToDelete ->
        let updatedList =
            list
            |> List.filter (fun f ->
                not (
                    String.Equals(f.Name, friendToDelete.Name, StringComparison.OrdinalIgnoreCase) &&
                    String.Equals(f.LastName, friendToDelete.LastName, StringComparison.OrdinalIgnoreCase)
                )
            )
        File.WriteAllText(fileNameDb, updatedList |> serializarLista)
        printfn ""
        printfn "Friend deleted successfully."
        printfn ""
        updatedList

let rec printMenu state =
    Console.Clear()
    printfn "\n========== Friends Database =========="
    printfn " 1. Add a New Friend"
    printfn " 2. Search Friend by Name"
    printfn " 3. List All Friends"
    printfn " 4. Update Friend Information"
    printfn " 5. Delete a Friend"
    printfn " 6. Exit"
    printfn "======================================"
    let opt = getInt "Select an option (1-6): "
    let newState =
        match opt with
        | 1 ->
            let updatedDb = addDbFriend state.DataBase
            getDbFriends updatedDb
            { state with DataBase = updatedDb }
        | 2 ->
            searchDbfriend state.DataBase
            state
        | 3 ->
            getDbFriends state.DataBase
            state
        | 4 ->
            let updatedDb = updateDbFriend state
            getDbFriends updatedDb
            { state with DataBase = updatedDb }
        | 5 ->
            let updatedDb = deleteDbFriend state.DataBase
            getDbFriends updatedDb
            { state with DataBase = updatedDb }
        | 6 ->
            printfn "Exit"
            state
        | _ ->
            printfn "Error. Option Undefined"
            state
    if opt <> 6 then
        printf "\nDo you want to return to the main menu? (y/n): "
        let resp = Console.ReadLine()
        if resp = "y" then
            Console.Clear()
            printMenu newState
        else
            newState
    else
        newState

let getDbDataFriends () =
    if File.Exists fileNameDb then
        readDbFriends fileNameDb
    else
        let list = createDb []
        File.WriteAllText(fileNameDb, list |> serializarLista)
        list

getDbDataFriends()
|> fun db ->
    { DataBase = db}
|> printMenu
|> ignore


(* printfn "%-12s %-12s %-12s %8s %12s %-25s" "Name" "Last Name" "NickName" "Age" "Phone" "Email"
db |> Seq.iter printFriend *)

(* Escribir texto y leet texto *)
(*
    Para escribir podemos usar File.WriteAllText()
    Para leer podemos usar File.ReadAllText()
 *)


(* State Machine *)

(*
    Representa el estado de programa en cualquier momento
    la intereaccion del usuario con el programa modifica el state
 *)


(*
    DRY DONT REPEAT YOURSELF
 *)
