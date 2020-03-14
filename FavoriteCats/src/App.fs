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

let emptyProps = {||}

type CatData =
    { url: string }

let catListsDecoder = 
    Decode.list (Decode.object (fun get -> 
        { url = get.Required.Field "url" Decode.string }))

type CatImageProps = 
    { src: string }

let GlobalStyles = styledSelf $ createGlobalStyle [| "
    body {
        background-color: black;
    }
" |]

let StyledApp = styledParent $ Div [| "
    border: 1px solid black;
    display: flex;
    justify-content: center;
    width: 500px;
    margin: auto;
" |]

let StyledCatImage = styledSelf $ Img [| "
    width: 500px;
    height: 500px;
    object-fit: cover;
    object-position: top center;
" |]

let appView = FunctionComponent.Of (fun () -> 
    let catImageUrl = Hooks.useState("")

    Hooks.useEffect((fun () ->
        let catListPromise = promise {    
            let! result = 
                Fetch.get (
                    "https://api.thecatapi.com/v1/images/search", 
                    headers = [ Fetch.Types.Custom("x-api-key", catsApiKey ) ],
                    decoder = catListsDecoder)
            return result;
        }
        catListPromise 
        |> Promise.iter(fun catList ->
            catList
            |> List.head
            |> fun cat -> catImageUrl.update(cat.url))
    ), [||])

    fragment [] [
        GlobalStyles emptyProps
        StyledCatImage { src = catImageUrl.current }
    ]
)

let App = StyledApp emptyProps (appView())

ReactDom.render (App, document.getElementById "app")
