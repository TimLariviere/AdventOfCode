System.IO.File.ReadLines "2018/Day 1/data.txt"
|> Seq.map int
|> Seq.reduce (+)