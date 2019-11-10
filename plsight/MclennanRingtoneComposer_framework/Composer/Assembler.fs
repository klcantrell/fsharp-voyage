module Assembler

open Audio
open ComposerParsing
open Samples

let tokenToSound token =
    generateSamples (durationFromToken token) (frequency token)

let assemble tokens =
    tokens |> List.map tokenToSound |> Seq.concat

let assembleToPackedStream (score:string) =
    match parse score with
    | Choice1Of2 errorMsg -> Choice1Of2 errorMsg
    | Choice2Of2 tokens -> 
        assemble tokens 
        |> Array.ofSeq
        |> pack
        |> Choice2Of2
