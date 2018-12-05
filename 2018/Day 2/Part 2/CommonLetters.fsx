System.IO.File.ReadAllLines "2018/Day 2/data.txt"
|> (fun lines ->
    let mutable foundChars = ""
    for i = 0 to (lines.Length - 2) do
        for j = i to (lines.Length - 1) do
            if foundChars = "" then
                let map = lines.[i] |> String.mapi (fun index c -> if c = lines.[j].[index] then c else ' ')
                let errors = map |> String.filter (fun c -> c = ' ') |> String.length
                if errors = 1 then
                    foundChars <- map |> String.filter (fun c -> c <> ' ')
    foundChars
)