module StyledComponents

open Fable.Core
open Fable.Core.JsInterop
open Fable.React.ReactBindings

type StyledComponent = obj

type IStyled = { 
    div: obj array -> StyledComponent
    img: obj array -> StyledComponent
}

[<ImportDefault("styled-components")>]
let _styled: IStyled = jsNative

[<Import("createGlobalStyle", "styled-components")>]
let createGlobalStyle: obj array -> StyledComponent = jsNative

let styled (styledComponent: StyledComponent) props element = 
    React.createElement (styledComponent, keyValueList CaseRules.LowerFirst props, element)

let Div = _styled.div
let Img = _styled.img

let (@@) styledComponentRenderer (styledComponent: StyledComponent) = styledComponentRenderer styledComponent
