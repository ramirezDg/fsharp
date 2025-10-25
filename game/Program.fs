open System

// Juego de Póker clásico: 1 jugador vs máquina
// Tipos para cartas y jugadores

type Palo = Corazones | Diamantes | Tréboles | Picas

type Valor = Dos | Tres | Cuatro | Cinco | Seis | Siete | Ocho | Nueve | Diez | Jota | Reina | Rey | As

type Carta = { Palo: Palo; Valor: Valor }

type TipoJugador = Humano | Máquina

type Jugador = { Nombre: string; Mano: Carta list; TipoJugador: TipoJugador }

type EstadoJuego = { Baraja: Carta list; Jugadores: Jugador list }

// Crear baraja completa
let todosLosPalos = [Corazones; Diamantes; Tréboles; Picas]
let todosLosValores = [Dos; Tres; Cuatro; Cinco; Seis; Siete; Ocho; Nueve; Diez; Jota; Reina; Rey; As]
let barajaCompleta = [ for palo in todosLosPalos do for valor in todosLosValores do yield { Palo = palo; Valor = valor } ]

// Mezclar baraja
let mezclarBaraja baraja = baraja |> List.sortBy (fun _ -> Guid.NewGuid())

// Repartir cartas (ejemplo: 5 cartas por jugador)
let repartirCartas (baraja: Carta list) (numJugadores: int) (numCartas: int) =
    let rec repartir (b: Carta list) (js: Carta list list) (n: int) =
        if n = 0 then b, js
        else
            let manos = [ for i in 0 .. numJugadores-1 -> js.[i] @ [b.[i]] ]
            let nuevaBaraja = b |> List.skip numJugadores
            repartir nuevaBaraja manos (n-1)
    let manosVacias = [ for _ in 1 .. numJugadores -> [] ]
    let baraja', manos = repartir baraja manosVacias numCartas
    baraja', manos

// Mostrar cartas
let mostrarCarta carta =
    let paloStr =
        match carta.Palo with
        | Corazones -> "♥"
        | Diamantes -> "♦"
        | Tréboles -> "♣"
        | Picas -> "♠"
    let valorStr =
        match carta.Valor with
        | Dos -> "2"
        | Tres -> "3"
        | Cuatro -> "4"
        | Cinco -> "5"
        | Seis -> "6"
        | Siete -> "7"
        | Ocho -> "8"
        | Nueve -> "9"
        | Diez -> "10"
        | Jota -> "J"
        | Reina -> "Q"
        | Rey -> "K"
        | As -> "A"
    sprintf "%s%s" valorStr paloStr

// Inicializar jugadores y estado
let humano = { Nombre = "Tú"; Mano = []; TipoJugador = Humano }
let maquina = { Nombre = "Máquina"; Mano = []; TipoJugador = Máquina }
let estadoInicial = { Baraja = mezclarBaraja barajaCompleta; Jugadores = [humano; maquina] }

// Repartir manos
let baraja', manos = repartirCartas estadoInicial.Baraja 2 5
let jugadores' = [ { humano with Mano = manos.[0] }; { maquina with Mano = manos.[1] } ]
let estado = { Baraja = baraja'; Jugadores = jugadores' }

// Mostrar manos
// Muestra cada carta de la mano de forma independiente, estilo ASCII
let mostrarCartaAscii carta =
    let paloStr =
        match carta.Palo with
        | Corazones -> "♥"
        | Diamantes -> "♦"
        | Tréboles -> "♣"
        | Picas -> "♠"
    let valorStr =
        match carta.Valor with
        | Dos -> "2"
        | Tres -> "3"
        | Cuatro -> "4"
        | Cinco -> "5"
        | Seis -> "6"
        | Siete -> "7"
        | Ocho -> "8"
        | Nueve -> "9"
        | Diez -> "10"
        | Jota -> "J"
        | Reina -> "Q"
        | Rey -> "K"
        | As -> "A"
    [
        "┌─────────┐"
        sprintf "│ %-2s      │" valorStr
        "│         │"
        sprintf "│    %s    │" paloStr
        "│         │"
        sprintf "│      %-2s │" valorStr
        "└─────────┘"
    ]

let mostrarManoAscii mano =
    let cartasAscii = mano |> List.map mostrarCartaAscii
    // Imprime las cartas una al lado de la otra
    for fila in 0 .. 6 do
        cartasAscii
        |> List.map (fun carta -> carta.[fila])
        |> String.concat "  "
        |> printfn "%s"

// Mostrar ambas manos lado a lado en la consola

// Mostrar manos en columnas verticales (jugador a la izquierda, máquina a la derecha)
let mostrarManosVerticalesLadoALado nombre1 mano1 nombre2 mano2 =
    let cartasAscii1 = mano1 |> List.map mostrarCartaAscii
    let cartasAscii2 = mano2 |> List.map mostrarCartaAscii
    let anchoCarta = 11
    let espacioEntre = 8

    // Imprime nombres sobre cada columna
    printfn "%s%*s" nombre1 (anchoCarta + espacioEntre + 2) nombre2

    // Imprime ambas columnas lado a lado
    let filasPorCarta = 7
    for i in 0 .. max (List.length cartasAscii1) (List.length cartasAscii2) - 1 do
        // Imprime carta del jugador
        if i < List.length cartasAscii1 then
            cartasAscii1.[i] |> List.iter (fun fila -> printf "%s" fila; printf "\n")
            printfn ""
        else
            for _ in 1 .. filasPorCarta do printfn ""
        // Espacio entre columnas
        for _ in 1 .. 1 do printfn ""
        // Imprime carta de la máquina
        if i < List.length cartasAscii2 then
            cartasAscii2.[i] |> List.iter (fun fila -> printf "%*s\n" (anchoCarta + espacioEntre) fila)
            printfn ""
        else
            for _ in 1 .. filasPorCarta do printfn ""


// Ejemplo de uso: mostrar las 5 cartas del jugador humano y la máquina lado a lado
mostrarManosVerticalesLadoALado estado.Jugadores.[0].Nombre estado.Jugadores.[0].Mano estado.Jugadores.[1].Nombre estado.Jugadores.[1].Mano

// --- Refactorización del flujo principal ---

let pedirNombreJugador () =
    printf "Bienvenido al juego de Póker!\nPor favor, ingresa tu nombre: "
    let nombre = Console.ReadLine()
    printfn "¡Hola %s!" nombre
    nombre

let pedirTipoJuego () =
    printfn "\n--- Selecciona el tipo de juego ---"
    printfn "1. Clásico (vs Máquina)"
    printfn "(Por ahora solo disponible el modo clásico)"
    "Clásico"

let preguntarIniciar () =
    printf "¿Deseas iniciar la partida? (s/n): "
    let resp = Console.ReadLine()
    resp.ToLower() = "s"

let mostrarMezclaBaraja () =
    printfn "\nMezclando la baraja..."
    for i in 1 .. 5 do
        printf "."
        System.Threading.Thread.Sleep(300)
    printfn "\n¡Baraja mezclada!\n"

let preguntarSeguirJugando () =
    printf "¿Quieres jugar otra vez? (s/n): "
    let resp = Console.ReadLine()
    resp.ToLower() = "s"

let rec bucleJuego () =
    let nombreJugador = pedirNombreJugador ()
    let tipoJuego = pedirTipoJuego ()
    if preguntarIniciar () then
        mostrarMezclaBaraja ()
        let barajaMezclada = mezclarBaraja barajaCompleta
        let baraja', manos = repartirCartas barajaMezclada 2 5
        let jugadores' = [ { humano with Nombre = nombreJugador; Mano = manos.[0] }; { maquina with Mano = manos.[1] } ]
        let estado = { Baraja = baraja'; Jugadores = jugadores' }
        printfn "¡Comienza la partida!\n"
        mostrarManosVerticalesLadoALado nombreJugador estado.Jugadores.[0].Mano estado.Jugadores.[1].Nombre estado.Jugadores.[1].Mano
        if preguntarSeguirJugando () then bucleJuego ()
        else printfn "¡Gracias por jugar!"
    else
        printfn "¡Hasta luego!"

bucleJuego()
