module Program

open System
open System.IO
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

try
    failwith "An error message"
with
    | Failure msg -> printfn "Failed with %s" msg

try
    try
        failwith "Another error message"
    with
        | Failure msg -> printfn "Failed with %s" msg
finally
    printfn "This always evaluates"

try
    try
        failwith "Yet another error message"
    finally
        printfn "This will also always evaluate"
with
    | Failure msg -> printfn "Failed with %s" msg

exception MyAwesomeException of string * int

try
    raise (MyAwesomeException("number", 1))
with
    | MyAwesomeException(msg, num) ->
        printfn "Failed: %s. The number is %d" msg num

try
    1 / 0
with
    | :? System.DivideByZeroException as ex ->
        printfn "Don't do that %s" ex.Message
        0
    | :? System.Exception as ex when ex.Message = "General error" ->
        printfn "Some other error %s" ex.Message
        0

let readAFile () =
    use reader = new StreamReader(
                    "text.txt")
    reader.ReadToEnd()

printfn "%s" (readAFile ())

let addPair (f, s) =
    f + s

let addPairWithMatch p =
    match p with
        | (f, 0) -> f
        | (0, s) -> s
        | (f, s) -> f + s

printfn "%i" (addPair (1, 2))
printfn "%i" (addPairWithMatch (0, 20))

let fizzbuzzer i =
    match i with
        | _ when i % 3 = 0 && i % 5 = 0 -> "fizzbuzz"
        | _ when i % 3 = 0-> "fizz"
        | _ when i % 5 = 0-> "buzz"
        | _ -> string i

printfn "%A" ([1..100] |> List.map fizzbuzzer)

open FsCheck

let appendedListLength l1 l2 =
    (l1 @ l2).Length = l1.Length + l2.Length 

let falsifiableAppendedListLength l1 l2 =
    (l1 @ l2).Length = l1.Length + 1

Check.Quick appendedListLength
Check.Quick falsifiableAppendedListLength

let firstOdd = 
    List.tryPick (fun x -> if x % 2 = 1 then Some x else None)

let printIntIfExists = function
    | Some x -> printfn "%s" ("The value is " + string x)
    | None -> printfn "No value here..."

let printDoubleIfExists = function
    | Some x -> printfn "%s" ("The value is " + string x)
    | None -> printfn "No value here..."

(firstOdd [2;4;6]) |> printIntIfExists
printIntIfExists <| firstOdd [2;4;5]

let toNumberAndSquare (o: Option<string>) =
    o
    |> Option.bind (fun s ->
                        let (succeeded, value) = Double.TryParse(s)
                        if succeeded then Some value else None)
    |> Option.bind (fun n -> n * n |> Some)

Some "5" |> toNumberAndSquare |> printDoubleIfExists
Some "foo" |> toNumberAndSquare |> printDoubleIfExists

let div num den = num / den

printfn "%A" (div 15 3)
//div 15 0

let safeDiv num den =
    if den = 0 then
        Choice1Of2 "divide by zero is undefined"
    else
        Choice2Of2 (num / den)

printfn "%A" (safeDiv 15 3)
printfn "%A" (safeDiv 15 0)

printfn "%A" (seq {0..2..10})
printfn "%A" (seq {for i in [1..10] -> i*i})

let board = 
    seq {
        for row in [1..8] do
            for column in [1..8] do
                yield pown (-1) (row + column)
    }
    |> Seq.map (fun i -> if i = -1 then "x " else "o ")

let printBoardSpace i v =
    if i > 0 && i % 8 = 0 then
        printfn ""
    printf "%s" v
Seq.iteri printBoardSpace board
printfn ""
printfn "%i" (Seq.length board)

type Person = {
    name: string
    age: int
}

let peopleData = [
    { name = "Abigail"; age = 6  }
    { name = "Ben"; age = 12  }
    { name = "Christine"; age = 18  }
    { name = "Daniel"; age = 24  }
    { name = "Emily"; age = 30  }
]

// using F# collection functions
let namesOfAdults =
    List.filter(fun p -> p.age >= 18) >> List.map(fun p -> p.name)

printfn "%A" (namesOfAdults peopleData)

// LINQ extension methods
open System.Linq
let namesOfAdultsLinq (data: List<Person>) = 
    data
        .Where(fun p -> p.age >= 18)
        .Select(fun p -> p.name)

printfn "%A" (namesOfAdultsLinq peopleData)

// LINQ query expressions
let namesOfAdultsLinqQuery (data: List<Person>) = 
    query {
        for p in data do
        where (p.age >= 18)
        select p.name
    }

printfn "%A" (namesOfAdultsLinqQuery peopleData)
