module App

open Browser.Dom
open Fable.Core
open Fable.React
open Fable.React.Props
open Thoth.Fetch
open Thoth.Json
open Types
open StyledComponents

[<Import("CATS_API_KEY", "./env")>]
let (catsApiKey: string) = jsNative

let inline TestMultipleChildren (elements: ReactElement list): ReactElement =
    ofImport "default" "./TestMultipleChildren" [] elements

type TestRenderPropProps =
    { render: string -> ReactElement }

let TestRenderProp = FunctionComponent.Of (fun (props: TestRenderPropProps) ->
    let secretMessage = "sup"
    props.render(secretMessage))

type CatData =
    { url: string }

let catListsDecoder = 
    Decode.list (Decode.object (fun get -> 
        { url = get.Required.Field "url" Decode.string }))

type CatImageProps = 
    { src: string }

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

let StyledCatImage: Custom.SelfClosingReactComponent = styled @@ Img [| "
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

    fragment [] [
        GlobalStyles [] []
        StyledCatImage [ Src catImageUrl.current ] []
        TestMultipleChildren [
            div [] [str "yo"]
            p [] [str "dude"] 
        ]
        TestRenderProp { render = fun message ->
            div [ Style [ Color "white" ] ] [ str message ] }
    ]
)

let App = StyledApp [] [ appView() ]

ReactDom.render (App, document.getElementById "app")
