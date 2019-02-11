open System

let createStore (init: 'State) (reducer: 'State -> 'Action -> 'State) =
    let inputEvent = Event<'Action>()
    Observable.scan reducer init inputEvent.Publish, inputEvent.Trigger

type State = { i: int }

type Action = Increment
            | Decrement

let reducer (state: State): Action -> State = function
    | Increment -> { state with i = state.i + 1 }
    | Decrement -> { state with i = state.i - 1 }

let states, dispatch = createStore { i = 0 } reducer

[<EntryPoint>]
let main _ =
    Observable.add (printfn "%A") states

    printfn "Let's go!"
    while true do
        match Console.ReadLine() with
        | "+" -> dispatch Increment
        | "-" -> dispatch Decrement
        | _ -> printfn "What do you mean?"
    0 // return an integer exit code
