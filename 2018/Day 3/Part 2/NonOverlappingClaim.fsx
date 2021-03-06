type Claim = 
    { Id: int
      Position: int * int
      Size: int * int }

let regex = new System.Text.RegularExpressions.Regex("#(?<id>[0-9]+) @ (?<left>[0-9]+),(?<top>[0-9]+): (?<width>[0-9]+)x(?<height>[0-9]+)") 
let fabric = Array2D.init 1000 1000 (fun _ _ -> ".")

let claims =
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
    |> Seq.toArray

claims
|> Array.iter (fun claim ->
    let (left, top) = claim.Position
    let (width, height) = claim.Size

    for i = 0 to width - 1 do
        for j = 0 to height - 1 do
            fabric.[(left + i), (top + j)] <-
                match fabric.[(left + i), (top + j)] with
                | "." -> string claim.Id
                | _ -> "X"
)

claims
|> Array.tryFind (fun claim ->
    let (left, top) = claim.Position
    let (width, height) = claim.Size

    let mutable result = true

    for i = 0 to width - 1 do
        for j = 0 to height - 1 do
            if fabric.[(left + i), (top + j)] <> string claim.Id then
               result <- false

    result
)
|> function
   | Some claim -> claim.Id
   | None -> -1