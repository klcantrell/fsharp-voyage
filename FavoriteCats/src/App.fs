module App

open Fable.React
open Fable.React.Props
open Browser.Dom
open StyledComponents

type StyledAppProps =
    { primary: bool }

let StyledApp = styled.div [| "
    background-color: ", (fun props -> if props.primary then "blue" else "red"), ";
    height: 100px;
    border: ", (fun props -> if props.primary then "20px" else "10px"), " solid black;
    border-radius: 10px;
" |]

let appView = FunctionComponent.Of (fun () -> 
    div [ Style [ Border "1px solid black" ] ] [ str "hi" ])

let App = StyledApp { primary = true } (appView())

ReactDom.render (App, document.getElementById "app")
