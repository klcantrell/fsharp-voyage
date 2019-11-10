namespace ComposerTests

module ComposerParsing = 

    open NUnit.Framework
    open ComposerParsing

    [<TestFixture>]
    type ``when parsing a score``() =
        
        [<Test>]
        member this.``it should parse a simple score``() =
            let score = "32.#d3 16-"
            let result = parse score
            let assertFirstToken { length=l; sound=s } =
                Assert.AreEqual({ fraction = Thirtyseconth; extended = true }, l)
                Assert.AreEqual(Tone (DSharp, Three), s)
            let assertSecondToken { length=l; sound=s } =
                Assert.AreEqual({ fraction = Sixteenth; extended = false }, l)
                Assert.AreEqual(Rest, s)

            match result with
            | Choice1Of2 errorMsg -> Assert.Fail(errorMsg)
            | Choice2Of2 tokens -> 
                Assert.AreEqual(2, List.length tokens)
                List.item 0 tokens |> assertFirstToken
                List.item 1 tokens |> assertSecondToken

    [<TestFixture>]
    type ``when calculating the frequency of notes``() =
        
        [<Test>]
        member this.``A2 should be 440``() =
            Assert.AreEqual(
                440.,
                { length={ fraction=Full; extended=false }; sound=Tone (A, Two) } |> frequency,
                0.1
            )
