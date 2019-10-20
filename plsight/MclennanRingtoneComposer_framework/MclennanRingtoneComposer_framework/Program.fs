module Program

let samples = Samples.generateSamples 1500. 440. 

Array.ofSeq samples
    |> Audio.pack
    |> Audio.write "test.wav"
