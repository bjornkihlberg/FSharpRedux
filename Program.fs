let store (init: 'State) (reducer: 'State -> 'Action -> 'State) =
    let inputEvent = Event<'Action>()
    Observable.scan reducer init inputEvent.Publish, inputEvent.Trigger

[<EntryPoint>]
let main _ =
    printfn "Hello, world!"
    0 // return an integer exit code
