﻿module ComposerParsing

open FParsec

let test p str =
    match run p str with
    | Success(result, _, _) -> printfn "Success: %A" result
    | Failure(errorMsg, _, _) -> printfn "Failure: %A" errorMsg

type MeasureFraction = Half | Quarter | Eigth | Sixteenth | Thirysecondth
type Length = { fraction: MeasureFraction; extended: bool }
type Note = A | ASharp | B | C | CSharp | D | DSharp | E | F | FSharp | G | GSharp
type Octave = One | Two | Three
type Sound = Rest | Tone of note: Note * octave: Octave
type Token = { length: Length; sound: Sound }

let aspiration = "32.#d3"

let pmeasurefraction = 
    (stringReturn "2" Half)
    <|> (stringReturn "4" Quarter)
    <|> (stringReturn "8" Sixteenth)
    <|> (stringReturn "16" Sixteenth)
    <|> (stringReturn "32" Thirysecondth)

let pextended = (stringReturn "." true) <|> (stringReturn "" false)

let plength = 
    pipe2
        pmeasurefraction
        pextended
        (fun t e -> { fraction = t; extended = e })

let pnotsharpablenote = anyOf "be" |>> (function
                            | 'b' -> B
                            | 'e' -> E
                            | unknown -> sprintf "Unknown note %c" unknown |> failwith)


let psharp = (stringReturn "#" true) <|> (stringReturn "" false)

let psharpnote = pipe2
                    psharp
                    (anyOf "acdfg")
                    (fun isSharp note ->
                        match (isSharp, note) with
                        | (false, 'a') -> A
                        | (true, 'a') -> ASharp
                        | (false, 'c') -> C
                        | (true, 'c') -> CSharp
                        | (false, 'd') -> D
                        | (true, 'd') -> DSharp
                        | (false, 'f') -> F
                        | (true, 'f') -> FSharp
                        | (false, 'g') -> G
                        | (true, 'g') -> GSharp
                        | (_, unknown) -> sprintf "Unknown note %c" unknown |> failwith)

let pnote = pnotsharpablenote <|> psharpnote

let poctave = anyOf "123" |>> (function
                 | '1' -> One
                 | '2' -> Two
                 | '3' -> Three
                 | unknown -> sprintf "Unknown octave %c" unknown |> failwith)

let ptone = pipe2 pnote poctave (fun n o -> Tone(note = n, octave = o))

let prest = stringReturn "-" Rest

let ptoken = pipe2 plength (prest <|> ptone) (fun l s -> { length = l; sound = s })

let pscore = sepBy ptoken (pstring " ")

printfn "%A" (test ptoken aspiration)
