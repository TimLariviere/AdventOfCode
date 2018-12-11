let rec simplifyPolymer (polymer: string) =
    let mutable resultingPolymer = polymer

    ['a'..'z']
    |> List.iter (fun c ->
        let upperC = System.Char.ToUpper(c).ToString()
        let coupleA = (c.ToString()) + upperC
        let coupleB = upperC + (c.ToString())

        resultingPolymer <-
            resultingPolymer.Replace(coupleA, "")
                            .Replace(coupleB, "")
    )

    if resultingPolymer = polymer then
       polymer
    else
       simplifyPolymer resultingPolymer

System.IO.File.ReadAllText "2018/Day 5/data.txt"
|> (fun polymer ->
   ['a'..'z']
   |> List.map (fun c ->
        let upperC = System.Char.ToUpper(c).ToString()
        polymer.Replace(c.ToString(), "").Replace(upperC.ToString(), "")
   )
)
|> List.map (simplifyPolymer >> String.length)
|> List.minBy id