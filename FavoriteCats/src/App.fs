module App

open Browser.Dom
open Fable.Core
open Fable.Core.JsInterop
open Fable.React
open Fable.React.Props
open Thoth.Fetch
open Thoth.Json

open Types
open StyledComponents
open ReactBeautifulDnd

[<Import("CATS_API_KEY", "./env")>]
let (catsApiKey: string) = jsNative

type CatData = { url: string }

let catListsDecoder = 
    Decode.list (Decode.object (fun get -> 
        { url = get.Required.Field "url" Decode.string }))

type CatImageProps = { src: string }

let GlobalStyles: Custom.ReactComponent = styled @@ createGlobalStyle [| "
    body {
        background-color: black;
    }
" |]

let StyledApp: Custom.ReactComponent = styled @@ Div [| "
    border: 1px solid black;
    display: flex;
    justify-content: center;
    width: 500px;
    margin: auto;
" |]

let StyledCatImage: Custom.ReactComponent = styled @@ Img [| "
    width: 500px;
    height: 500px;
    object-fit: cover;
    object-position: top center;
" |]

let appView = FunctionComponent.Of (fun () -> 
    let catImageUrl = Hooks.useState("")

    Hooks.useEffect((fun () ->
        promise {
            let! catList = 
                Fetch.get (
                    "https://api.thecatapi.com/v1/images/search", 
                    headers = [ Fetch.Types.Custom("x-api-key", catsApiKey ) ],
                    decoder = catListsDecoder)

            catList
            |> List.head
            |> fun cat -> catImageUrl.update(cat.url)
        } |> ignore
    ), [||])

    DragDropContext [] [
        GlobalStyles [] []
        Droppable [
            DroppableId "main"
            DroppableProps.Children (fun provided snapshot -> 
                div [ Style [ Color "white" ]; Ref provided.innerRef ] [
                    StyledCatImage [ 
                        Src catImageUrl.current 
                    ] []
                    if snapshot.isDraggingOver then str "dragging is over" else str "dragging ain't over"
                ])
        ]
    ])

let App = StyledApp [] [ appView() ]

ReactDom.render (App, document.getElementById "app")
