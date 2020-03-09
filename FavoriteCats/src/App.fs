module App

open Fable.Core
open Fable.React
open Fable.React.Props
open Browser.Dom

[<ImportDefault("./App.css")>]
let styles: {| app: string |} = jsNative

type AppProps =
    { message: string }

let appView (props: AppProps) =
    let { message = message } = props
    div [ ClassName styles.app ] [ str message ]

let App = FunctionComponent.Of appView

ReactDom.render (App({ message = "sup" }), document.getElementById "app")
