System.IO.File.ReadLines "2018/Day 2/data.txt"
|> Seq.map (fun s ->
    Seq.distinct s
    |> Seq.map (fun c -> s |> Seq.filter (fun c2 -> c = c2) |> Seq.length)
    |> (fun counts ->
        let hasCountFor number =
            match Seq.tryFind (fun x -> x = number) counts with
            | Some _ -> 1
            | None -> 0
        (hasCountFor 2, hasCountFor 3)
    )
)
|> Seq.reduce (fun (a2, a3) (b2, b3) -> (a2 + b2), (a3 + b3))
|> (fun (twos, threes) -> twos * threes)