﻿namespace ComposerTests

module AssemblerTests =

    open NUnit.Framework
    open Assembler
    open ComposerParsing

    [<TestFixture>]
    type ``when assembling a composition`` () = 
        let extractChoice2 v =
            match v with
            | Choice1Of2 s -> sprintf "unexpected choice value %A" s |> failwith
            | Choice2Of2 s -> s

        [<Test>]
        member this.``the result should have the correct length`` () =
            let scoreD = parse "2#d3 2- 2- 8#d3 4c2 4c2 8c1 2- 4c1" |> extractChoice2
            let samples = assemble scoreD
            let expectedSamples = 6. * 44100.
            Assert.AreEqual(expectedSamples, Seq.length samples)
