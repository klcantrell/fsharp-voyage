module Consumer

open CSharpLibrary

type Consumer () =
    let c1 = new Number()
    member this.X = c1.FirstCountingNumber()

    interface ICanAddNumbers with
        member this.Add (a, b) = a + b