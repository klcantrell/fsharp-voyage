open System
open System.IO
open Lib

[<EntryPoint>]
let main argv =

    Lib.testInput
    |> parseRules
    |> List.iter(fun rule -> Console.WriteLine("The rule is " + rule))

    let fileSystemWatcher = new FileSystemWatcher()
    fileSystemWatcher.Path <- "."
    fileSystemWatcher.Filter <- "*.css"
    fileSystemWatcher.Changed.Add(fun _ -> Console.WriteLine("A css file changed"))
    fileSystemWatcher.EnableRaisingEvents <- true
    while (Console.Read() |> Convert.ToChar <>  'q') do
        ()
    0

