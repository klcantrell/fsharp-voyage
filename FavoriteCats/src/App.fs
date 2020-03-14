module App

open Browser.Dom
open Fable.Core
open Fable.React
open Fable.React.Props
open Thoth.Fetch
open Thoth.Json
open StyledComponents

[<Import("CATS_API_KEY", "./env")>]
let (catsApiKey: string) = jsNative

type StyledAppProps = 
    { primary: bool }

let StyledApp = styled.div [| "
    background-color: ", (fun props -> if props.primary then "blue" else "red"), ";
    height: 100px;
    border: ", (fun props -> if props.primary then "20px" else "10px"), " solid black;
    border-radius: 10px;
" |]

type Cat =
    { url: string }

let catListsDecoder = 
        (Decode.list (Decode.object (fun get -> 
        { url = get.Required.Field "url" Decode.string })))

let appView = FunctionComponent.Of (fun () -> 
    let catImage = Hooks.useState("")
    Hooks.useEffect((fun () ->
        let result = promise {    
            let! result = 
                Fetch.get (
                    "https://api.thecatapi.com/v1/images/search", 
                    headers = [ Fetch.Types.Custom("x-api-key", catsApiKey ) ],
                    decoder = catListsDecoder)
            return result;
        }
        result 
        |> Promise.iter(fun catList ->
            catList
            |> List.head
            |> fun cat -> catImage.update(cat.url))
    ), [||])
    div [] [ str catImage.current ])

let App = StyledApp { primary = true } (appView())

ReactDom.render (App, document.getElementById "app")
