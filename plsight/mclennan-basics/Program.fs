﻿module Program

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

//1 + 3.14 |> ignore //this won't compile

printfn "%f" (float "3.14")

printfn "%s" "It was the best of times,
it was the worst of times,".[0..5]

printfn "%b" ("03249" |> String.forall System.Char.IsDigit)

printfn "%A" (1 :: [2;3])

printfn "%A" [10..-1..0]

printfn "%A" ([10..-1..0] @ [42])

printfn "%A" [for x in 1..10 -> 2 * x]

printfn "%A" [
    for r in 1..8 do
    for c in 1..8 do
        if r <> c then
            yield (r, c)
]

printfn "%A" [|1;2;3|].[0]

[<EntryPoint>]
let main argv =
    printArray argv
    0