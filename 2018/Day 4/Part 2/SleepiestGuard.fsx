type RecordType = BeginShift | FallAsleep | WakeUp

type Record =
    { Date: System.DateTime
      Guard: int
      Type: RecordType }

let mutable currentGuard = 0

System.IO.File.ReadLines "2018/Day 4/data.txt"
|> Seq.map (fun line ->
    let closingBracketIndex = line.IndexOf("]")
    let date = System.DateTime.ParseExact(line.[1..(closingBracketIndex - 1)], "yyyy-MM-dd HH:mm", System.Globalization.CultureInfo.InvariantCulture)
    (date, line.[(closingBracketIndex + 2)..])
)
|> Seq.sortBy (fun (d, _) -> d)
|> Seq.map (fun (date, line) ->
    let (guard, recordType) =
        match line with
        | "falls asleep" -> (currentGuard, FallAsleep)
        | "wakes up" -> (currentGuard, WakeUp)
        | _ ->
            let substr = line.[(line.IndexOf("#") + 1)..]
            let guardId = substr.[..(substr.IndexOf(" ") - 1)] |> int
            currentGuard <- guardId
            (guardId, BeginShift)
    { Date = date
      Guard = guard
      Type = recordType }
)
|> Seq.groupBy (fun record -> record.Guard)
|> Seq.map (fun (guard, records) ->
    let sleeps = new System.Collections.Generic.List<int * int>()
    let mutable lastAsleepRecord = None

    for record in records do
        match record.Type with
        | FallAsleep -> lastAsleepRecord <- Some record.Date.Minute
        | WakeUp -> sleeps.Add((lastAsleepRecord.Value, record.Date.Minute))
        | _ -> ()

    (guard, sleeps |> Seq.toArray)    
)
|> Seq.map (fun (guard, sleeps) ->
    let (mostSleptMinute, count) =
        match sleeps.Length with
        | 0 -> (0, 0)
        | _ ->
            sleeps
            |> Array.map (fun (start, stop) -> Array.init (stop - start) (fun i -> start + i))
            |> Array.concat
            |> Array.countBy (fun m -> m)
            |> Array.sortByDescending (fun (_, c) -> c)
            |> Array.head

    (guard, mostSleptMinute, count)
)
|> Seq.sortByDescending (fun (g, m, t) -> t)
|> Seq.map (fun (g, m, t) -> g * m)
|> Seq.head