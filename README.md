# FSharpRedux
[Redux](https://redux.js.org/)
implemented in
[F#](https://fsharp.org/)
in two lines of code. Very simple but I thought it was so cool I wanted to save it!

## How it works
This is the entire implementation of Redux in F#:
```fs
let createStore (init: 'State) (reducer: 'State -> 'Action -> 'State) =
    let inputEvent = Event<'Action>()
    Observable.scan reducer init inputEvent.Publish, inputEvent.Trigger
```