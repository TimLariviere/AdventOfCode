System.IO.File.ReadAllLines "2018/Day 2/data.txt"
|> (fun lines -> seq {
    for i = 0 to (lines.Length - 2) do
        for j = i to (lines.Length - 1) do
            let convert = (lines.[i] |> String.mapi (fun index c -> if c = lines.[j].[index] then c else ' '))
            yield convert })
|> Seq.tryFind (fun str ->
    match str |> String.filter (fun c -> c = ' ') |> String.length with
    | 1 -> true
    | _ -> false
)
|> function
   | Some str -> str |> String.filter (fun c -> c <> ' ')
   | None -> ""