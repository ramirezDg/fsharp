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

type StateSystem = {
    Foreground: ConsoleColor
    Background: ConsoleColor
    CursorLeft: int
    CursorTop: int
    WindowTitle: string
}

let getCurrentState () = {
    Foreground = Console.ForegroundColor
    Background = Console.BackgroundColor
    CursorLeft = Console.CursorLeft
    CursorTop = Console.CursorTop
    WindowTitle = Console.Title
}

let setState (state: StateSystem) =
    Console.ForegroundColor <- state.Foreground
    Console.BackgroundColor <- state.Background
    Console.SetCursorPosition(state.CursorLeft, state.CursorTop)
    Console.Title <- state.WindowTitle

let printCentered (msg: string) (color: ConsoleColor) =
    let x = (Console.WindowWidth - msg.Length) / 2
    let y = Console.CursorTop
    Console.SetCursorPosition(x, y)
    Console.ForegroundColor <- color
    Console.WriteLine msg
    Console.ResetColor()

// --- Mostrar carta con color y posición ---
let mostrarCartaAsciiColoreada carta x y =
    // Símbolo de palo y colores clásicos
    let paloStr, colorPalo, marcoColor, valorColor =
        match carta.Palo with
        | Corazones -> "♥", ConsoleColor.Red, ConsoleColor.Red, ConsoleColor.Red
        | Diamantes -> "♦", ConsoleColor.Red, ConsoleColor.Red, ConsoleColor.Red
        | Tréboles -> "♣", ConsoleColor.Black, ConsoleColor.Black, ConsoleColor.Black
        | Picas -> "♠", ConsoleColor.Black, ConsoleColor.Black, ConsoleColor.Black
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
    let esFigura = match carta.Valor with Jota | Reina | Rey | As -> true | _ -> false
    let marcoColorFinal = if esFigura then ConsoleColor.Yellow else marcoColor
    let valorColorFinal = if esFigura then ConsoleColor.Yellow else valorColor
    let fondoColor = ConsoleColor.White
    let lineas =
        [| "┌─────────┐"
           ; sprintf "│ %-2s      │" valorStr
           ; "│         │"
           ; sprintf "│    %s    │" paloStr
           ; "│         │"
           ; sprintf "│      %-2s │" valorStr
           ; "└─────────┘" |]
    for i = 0 to lineas.Length - 1 do
        Console.SetCursorPosition(x, y + i)
        match i with
        | 0 | 6 -> Console.ForegroundColor <- marcoColorFinal; Console.BackgroundColor <- fondoColor; Console.Write(lineas.[i]); Console.ResetColor()
        | 1 | 5 -> Console.ForegroundColor <- valorColorFinal; Console.BackgroundColor <- fondoColor; Console.Write(lineas.[i]); Console.ResetColor()
        | 3 -> Console.ForegroundColor <- colorPalo; Console.BackgroundColor <- fondoColor; Console.Write(lineas.[i]); Console.ResetColor()
        | _ -> Console.ForegroundColor <- colorPalo; Console.BackgroundColor <- fondoColor; Console.Write(lineas.[i]); Console.ResetColor()

// --- Mostrar manos alineadas y con color ---
let mostrarManosVerticalesLadoALado (nombre1: string) (mano1: Carta list) (nombre2: string) (mano2: Carta list) =
    Console.Clear()
    let anchoCarta = 11
    let altoCarta = 7
    let espacioSuperior = 4
    let espacioInferior = 2
    // Altura máxima necesaria: 3 cartas en columna (3*8-1=23), 2 en la otra (2*8-1=15), pero ambas columnas empiezan en el mismo yBase
    let alturaCartas = max (3 * (altoCarta + 1) - 1) (2 * (altoCarta + 1) - 1)
    let yBasePropuesto = Console.CursorTop + espacioSuperior
    let yBase =
        let maxY = Console.WindowHeight - espacioInferior - alturaCartas
        if yBasePropuesto > maxY then max 0 maxY else max 0 yBasePropuesto
    let xIzq = 2
    let xDer = Console.WindowWidth - (anchoCarta * 2) - 4
    // Nombres centrados sobre cada mano, en recuadro de color
    let centrarNombre (nombre: string) x color =
        let xNombre = max 0 (x + (anchoCarta - nombre.Length) / 2 - 2)
        let yNombre =
            let y = yBase - 2
            if y < 0 then 0
            elif y >= Console.WindowHeight then Console.WindowHeight - 1
            else y
        Console.SetCursorPosition(xNombre, yNombre)
        Console.BackgroundColor <- color
        Console.ForegroundColor <- ConsoleColor.Black
        Console.Write($"  {nombre}  ")
        Console.ResetColor()
    centrarNombre nombre1 xIzq ConsoleColor.Yellow
    centrarNombre nombre2 xDer ConsoleColor.Cyan
    // Función para mostrar una mano en formato 3+2
    let mostrarMano mano x0 =
        // 3 cartas en columna izquierda
        for i = 0 to 2 do
            let yCarta = yBase + i * (altoCarta + 1)
            if yCarta + altoCarta < Console.WindowHeight && i < List.length mano then
                mostrarCartaAsciiColoreada (List.item i mano) x0 yCarta
        // 2 cartas en columna derecha
        let x1 = x0 + anchoCarta + 4
        for i = 3 to 4 do
            let yCarta = yBase + (i - 3) * (altoCarta + 1)
            if yCarta + altoCarta < Console.WindowHeight && i < List.length mano then
                mostrarCartaAsciiColoreada (List.item i mano) x1 yCarta
    // Mano jugador a la izquierda, máquina a la derecha
    mostrarMano mano1 xIzq
    mostrarMano mano2 xDer
// --- Animación de mezcla con color ---
let mostrarMezclaBaraja () =
    Console.Write("Mezclando la baraja")
    for i in 1 .. 10 do
        Console.ForegroundColor <- if i % 2 = 0 then ConsoleColor.Magenta else ConsoleColor.Green
        Console.Write(" ░")
        System.Threading.Thread.Sleep(120)
    Console.ForegroundColor <- ConsoleColor.Cyan
    Console.WriteLine(" ¡Listo!")
    Console.ResetColor()
    for i in 1 .. 10 do
        Console.ForegroundColor <- if i % 2 = 0 then ConsoleColor.Magenta else ConsoleColor.Green
        Console.Write(" ░")
        System.Threading.Thread.Sleep(120)
    Console.ForegroundColor <- ConsoleColor.Cyan
    Console.WriteLine(" ¡Listo!")
    Console.ResetColor()

let limpiarLineaInferior () =
    let y = Console.WindowHeight - 2
    Console.SetCursorPosition(0, y)
    Console.Write(new string(' ', Console.WindowWidth))
    Console.SetCursorPosition(0, y)

let preguntarInferior (pregunta: string) (color: ConsoleColor) =
    limpiarLineaInferior()
    Console.ForegroundColor <- color
    Console.Write(pregunta)
    Console.ResetColor()
    Console.ReadLine()

// --- Pedir nombre con pantalla centrada y color ---
let pedirNombreJugador () =
    Console.Clear()
    printCentered "╔══════════════════════════════════════╗" ConsoleColor.Cyan
    printCentered "║   Bienvenido al juego de Póker!     ║" ConsoleColor.Cyan
    printCentered "╚══════════════════════════════════════╝" ConsoleColor.Cyan
    printCentered "" ConsoleColor.Gray
    printCentered "Cada jugador recibe 5 cartas." ConsoleColor.Yellow
    let nombre = preguntarInferior "Por favor, ingresa tu nombre: " ConsoleColor.Yellow
    limpiarLineaInferior()
    printCentered ($"¡Hola {nombre}!") ConsoleColor.Green
    nombre

let pedirTipoJuego () =
    limpiarLineaInferior()
    Console.SetCursorPosition(0, Console.WindowHeight - 2)
    Console.ForegroundColor <- ConsoleColor.Yellow
    Console.Write("Tipo de juego: 1. Clásico (vs Máquina)  (Por ahora solo disponible el modo clásico)")
    Console.ResetColor()
    System.Threading.Thread.Sleep(1200)
    "Clásico"

let preguntarIniciar () =
    let resp = preguntarInferior "¿Deseas iniciar la partida? (s/n): " ConsoleColor.Cyan
    limpiarLineaInferior()
    resp.ToLower() = "s"

let preguntarSeguirJugando () =
    let resp = preguntarInferior "¿Quieres jugar otra vez? (s/n): " ConsoleColor.Cyan
    limpiarLineaInferior()
    resp.ToLower() = "s"

let juego () =
    let nombreJugador = pedirNombreJugador ()
    let _ = pedirTipoJuego ()
    let rec loop () =
        if preguntarIniciar () then
            mostrarMezclaBaraja ()
            let barajaMezclada = mezclarBaraja barajaCompleta
            let baraja', manos = repartirCartas barajaMezclada 2 5
            let jugadores' = [ { humano with Nombre = nombreJugador; Mano = manos.[0] }; { maquina with Mano = manos.[1] } ]
            let estado = { Baraja = baraja'; Jugadores = jugadores' }
            printCentered "¡Comienza la partida!" ConsoleColor.Cyan
            mostrarManosVerticalesLadoALado nombreJugador estado.Jugadores.[0].Mano estado.Jugadores.[1].Nombre estado.Jugadores.[1].Mano
            if preguntarSeguirJugando () then loop ()
            else printCentered "¡Gracias por jugar!" ConsoleColor.Green
        else
            printCentered "¡Hasta luego!" ConsoleColor.Green
    loop ()

juego ()
