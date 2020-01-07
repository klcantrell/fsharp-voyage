module Lib

open System.Text.RegularExpressions

let parseRules str =
    let rulePattern = @"\.\w+"
    let matches = Regex.Matches(str, rulePattern)
    if matches.Count > 0 then [for m in matches -> m.Value] else []

let testInput = "
    .sup
        color: blue;
    }
    .dude {
        color: red;
    }
"
