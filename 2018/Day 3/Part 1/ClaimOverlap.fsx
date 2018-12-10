type Claim = 
    { Id: int
      Position: int * int
      Size: int * int }

let regex = new System.Text.RegularExpressions.Regex("#(?<id>[0-9]+) @ (?<left>[0-9]+),(?<top>[0-9]+): (?<width>[0-9]+)x(?<height>[0-9]+)") 
let fabric = Array2D.init 1000 1000 (fun _ _ -> 0)

System.IO.File.ReadLines "2018/Day 3/data.txt"
|> Seq.map (fun line ->
    let parse (r: System.Text.RegularExpressions.Match) (name: string) = r.Groups.[name].Value |> int

    let r = regex.Match line
    match r.Success with
    | false -> None
    | true ->
        Some { Id = parse r "id"
               Position = (parse r "left"), (parse r "top")
               Size = (parse r "width"), (parse r "height") }
)
|> Seq.filter Option.isSome
|> Seq.map Option.get
|> Seq.iter (fun claim ->
    let (left, top) = claim.Position
    let (width, height) = claim.Size

    for i = 0 to width - 1 do
        for j = 0 to height - 1 do
            fabric.[(left + i), (top + j)] <- fabric.[(left + i), (top + j)] + 1
)

fabric |> Seq.cast<int> |> Seq.filter (fun x -> x > 1) |> Seq.length