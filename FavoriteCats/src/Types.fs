module Types

open Fable.React
open Fable.React.Props

module Custom = 
  type ReactComponent = IHTMLProp list -> ReactElement list -> ReactElement
  type SelfClosingReactComponent = HTMLAttr list -> ReactElement list -> ReactElement
