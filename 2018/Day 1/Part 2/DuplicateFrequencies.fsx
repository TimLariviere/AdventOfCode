#r "packages/FSharpx.Collections/lib/net40/FSharpx.Collections.dll"

open FSharpx.Collections

System.IO.File.ReadAllLines "2018/Day 1/data.txt"
|> Array.map int
|> (fun lines -> Seq.initInfinite (fun i -> lines.[i % lines.Length]))
|> LazyList.ofSeq
|> (fun input ->
        let knownFrequencies = System.Collections.Generic.HashSet<int>()
        knownFrequencies.Add 0 |> ignore

        let rec loop input acc =
            match LazyList.isEmpty input with
            | true -> None
            | false ->
                let r = acc + (LazyList.head input)
                match knownFrequencies.Contains(r) with
                | true -> Some r
                | false ->
                    knownFrequencies.Add r |> ignore
                    loop (LazyList.skip 1 input) r

        loop input 0)