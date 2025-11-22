module App.Types

type MenuCommand =
| NewGame
| LoadGame
| Exit

type PauseCommand =
| Continue
| SaveGame
| Exit

type GameOverCommand =
| NewGame
| Exit


(* GAME TYPES*)

type ProgramState =
| Running
| Paused
| Terminated

type ObjectState =
| Visible
| Colisionado
| Pausado

type Missil = {
    X: int
    Y: int
}
type State = {
    AlienX: int
    AlienY: int
    AlienState: ObjectState
    AlienVidas: int
    AlienColisionTime: int
    Counter: int
    Misiles: Missil list
    Tick: int
    Width: int
    Height: int
    ProgramState: ProgramState
    EnemyX: int
    EnemyY: int
    EnemySpeed: float
    EnemyState: ObjectState
    ColisionTime: int
    Puntaje: int
    MisilesEnemigos: Missil list
    UltimoDisparo: int
    EnemyAngle: float
}
