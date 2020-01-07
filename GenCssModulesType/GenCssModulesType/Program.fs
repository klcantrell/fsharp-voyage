open System
open System.IO
open Lib

let printWithColor (color: ConsoleColor) (message: string) =
    Console.ForegroundColor <- color
    Console.WriteLine(message)
    Console.ResetColor()

let printErrorWithColor (color: ConsoleColor) (message: string) =
    Console.ForegroundColor <- color
    Console.Error.WriteLine(message)
    Console.ResetColor()

let handleOnChange path =
    let rec handler retries =
        if retries > 10_000
        then
             "Could not open file" |> printErrorWithColor ConsoleColor.DarkRed 
        else
            try
                File.ReadAllText path
                |> parseRules
                |> List.iter(
                    fun rule -> ("The rule is " + rule) |> printWithColor ConsoleColor.Magenta)
            with
                | :? System.IO.IOException -> 
                    ("Retrying, attempt: " + (retries + 1).ToString()) |> printWithColor ConsoleColor.Yellow
                    handler (retries + 1)
    handler 0

[<EntryPoint>]
let main argv =
    let fileSystemWatcher = new FileSystemWatcher()
    fileSystemWatcher.Path <- "."
    fileSystemWatcher.Filter <- "*.css"
    fileSystemWatcher.Changed.Add(fun e -> handleOnChange e.FullPath)
    fileSystemWatcher.EnableRaisingEvents <- true

    while (Console.Read() |> Convert.ToChar <>  'q') do
        ()
    0

