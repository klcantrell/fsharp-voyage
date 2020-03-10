module StyledComponents

open Fable.Core
open Fable.React.ReactBindings

type IStyled =
    { div: obj array -> obj }

[<ImportDefault("styled-components")>]
let _styled: IStyled = jsNative

module styled =
    let div styles props element = React.createElement (_styled.div styles, props, [ element ])
