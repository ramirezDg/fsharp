module App.Letras

let letraA =
    [|
        " ████ "
        "█    █"
        "█    █"
        "██████"
        "█    █"
        "█    █"
    |]

let letraB =
    [|
        "█████ "
        "█    █"
        "█████ "
        "█    █"
        "█    █"
        "█████ "
    |]

let letraD =
    [|
        "█████ "
        "█    █"
        "█    █"
        "█    █"
        "█    █"
        "█████ "
    |]

let letraF =
    [|
        "██████"
        "█"
        "█████"
        "█"
        "█"
        "█"
    |]

let letraH =
    [|
        "█    █"
        "█    █"
        "██████"
        "█    █"
        "█    █"
        "█    █"
    |]

let letraJ =
    [|
        "   ███"
        "    █"
        "    █"
        "    █"
        "█   █"
        " ███"
    |]

let letraP =
    [|
        "█████ "
        "█    █"
        "█████ "
        "█"
        "█"
        "█"
    |]

let letraQ =
    [|
        " ████ "
        "█    █"
        "█    █"
        "█  █ █"
        "█   █"
        " ███ █"
    |]

let letraS =
    [|
        " ████ "
        "█    █"
        " ███"
        "    █"
        "█    █"
        " ████ "
    |]

let letraU =
    [|
        "█    █"
        "█    █"
        "█    █"
        "█    █"
        "█    █"
        " ████ "
    |]

let letraW =
    [|
        "█    █"
        "█    █"
        "█    █"
        "█ ██ █"
        "██  ██"
        "█    █"
    |]

let letraX =
    [|
        "█    █"
        " █  █"
        "  ██"
        "  ██"
        " █  █"
        "█    █"
    |]

let letraY =
    [|
        "█    █"
        " █  █"
        "  ██"
        "   █"
        "   █"
        "   █"
    |]

let letraZ =
    [|
        "██████"
        "    █"
        "   █"
        "  █"
        " █"
        "██████"
    |]
let letraC =
    [|
        " ████ "
        "█    █"
        "█"
        "█"
        "█    █"
        " ████"
    |]

let letraG =
    [|
        " ████ "
        "█    █"
        "█"
        "█  ███ "
        "█    █"
        " ████"
    |]

let letraO =
    [|
        " ████ "
        "█    █"
        "█    █"
        "█    █"
        "█    █"
        " ████"
    |]

let letraL =
    [|
        "█"
        "█"
        "█"
        "█"
        "█"
        "██████"
    |]

let letraI =
    [|
        "  ███ "
        "   █"
        "   █"
        "   █"
        "   █"
        "  ███"
    |]

let letraE =
    [|
        "██████"
        "█"
        "█████"
        "█    "
        "█"
        "██████"

    |]

let letraN =
    [|
        "█    █"
        "██   █"
        "█ █  █"
        "█  █ █"
        "█   ██"
        "█    █"
    |]

let letraT =
    [|
        "█████ "
        "  █"
        "  █"
        "  █"
        "  █"
        "  █"
    |]

let letraK =
    [|
        "█    █"
        "█   █ "
        "████"
        "█   █"
        "█    █"
        "█     █"
    |]

let letraSpc =
    [|
        " "
        " "
        " "
        " "
        " "
        " "
    |]

let letraM =
    [|
        "█    █"
        "██  ██"
        "█ ██ █"
        "█    █"
        "█    █"
        "█    █"
    |]

let letraR =
    [|
        "█████ "
        "█    █"
        "█    █"
        "█████"
        "█   █"
        "█    █"
    |]

let letraV =
    [|
        "█    █"
        "█    █"
        "█    █"
        "█    █"
        " █  █"
        "  ██"
    |]

let mapaDeLetras =
    [
        'A',letraA
        'B',letraB
        'C',letraC
        'D',letraD
        'E',letraE
        'F',letraF
        'G',letraG
        'H',letraH
        'I',letraI
        'J',letraJ
        'K',letraK
        'L',letraL
        'M',letraM
        'N',letraN
        'O',letraO
        'P',letraP
        'Q',letraQ
        'R',letraR
        'S',letraS
        'T',letraT
        'U',letraU
        'V',letraV
        'W',letraW
        'X',letraX
        'Y',letraY
        'Z',letraZ
        ' ',letraSpc
    ]
    |> Map.ofList

let encontrarLetra letra =
    mapaDeLetras
    |> Map.tryFind letra
    |> Option.defaultValue letraA
