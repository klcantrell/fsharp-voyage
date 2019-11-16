module WebApp

open Giraffe
open Giraffe.GiraffeViewEngine

[<CLIMutable>]
type PracticeModel =
    {
        WelcomeText : string
    }

let practiceModel = { WelcomeText = "Sup y'all" }

let view (model : PracticeModel) =
    h1 [] [ str model.WelcomeText]

let webApp : HttpHandler = 
    choose [
        GET >=> 
            choose [
                route "/" >=> (view practiceModel |> htmlView)
            ]
    ]
