module Program

open Printer

let minus1 x = x - 1
let times2 = (*) 2

printfn "%s %i" "minus1" (minus1 9)
printfn "%s %i" "times2" (times2 8)

let minus1Times2 = minus1 >> times2
let anotherMinus1Times2 = times2 << minus1

printfn "%s %i" "minus1Times2" (minus1Times2 9)
printfn "%s %i" "anotherMinus1Times" (9 |> anotherMinus1Times2)
printfn "%s %i" "checking pipe precedence" ((9 |> minus1) + times2 2)

[<EntryPoint>]
let main argv =
    printArray argv
    0
