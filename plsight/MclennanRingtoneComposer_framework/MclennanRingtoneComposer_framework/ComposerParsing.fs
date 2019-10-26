module ComposerParsing

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

printfn "%A" (test plength aspiration)
