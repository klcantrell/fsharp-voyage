module Program
open System

[<EntryPoint>]
let main argv =
    let number = new CSharpLibrary.Number ()
    printfn "%i" (number.FirstCountingNumber ())

    let adder = {new CSharpLibrary.ICanAddNumbers with member this.Add (a, b) = a + b}
    printfn "%i" (adder.Add (1, 3))

    Double.TryParse("3.14159") |> function
        | (true, value) -> printfn "%f" value
        | (false, _) -> printfn "could not parse"

    Double.TryParse("DANG") |> function
        | (true, value) -> printfn "%f" value
        | (false, _) -> printfn "could not parse"

    0
