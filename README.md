# FSharpRedux
[Redux](https://redux.js.org/)
implemented in
[F#](https://fsharp.org/)
in two lines of code. Very simple but I thought it was so cool I wanted to save it!

---
## How it works
This is the entire implementation of Redux in F#:
```fs
let createStore (init: 'State) (reducer: 'State -> 'Action -> 'State) =
    let inputEvent = Event<'Action>()
    Observable.scan reducer init inputEvent.Publish, inputEvent.Trigger
```
It is quite self explanatory. `Observable.scan` is of extra interest though. It has the following type signature:
```fs
Observable.scan: ('T -> 'U -> 'T) -> 'T -> 'U IObservable -> 'T IObservable
```
It is very similar to
[Seq.reduce](https://msdn.microsoft.com/visualfsharpdocs/conceptual/seq.reduce%5b%27t%5d-function-%5bfsharp%5d)
or
[Seq.fold](https://msdn.microsoft.com/visualfsharpdocs/conceptual/seq.fold%5b%27t%2c%27state%5d-function-%5bfsharp%5d). There is in fact even an equivalent called
[Seq.scan](https://msdn.microsoft.com/visualfsharpdocs/conceptual/seq.scan%5b%27t%2c%27state%5d-function-%5bfsharp%5d).

`fold` is the most generic of the three because it can imitate the other two.
```fs
Seq.fold: ('T -> 'U -> 'T) -> 'T -> 'U seq -> 'T
```
Basically it takes an initial value and builds a new value of the same type based on values in an sequence. It could be a sequence of strings and it would count by adding up the occurances of a certain string for example.

`reduce` simply defaults to using the first value in the sequence as the initial value which means that it can only build a value of the same type as the values in the sequence.
```fs
Seq.reduce: ('T -> 'T -> 'T) -> 'T seq -> 'T
```
`scan` works like fold but instead of returning the final value, it returns another sequence of all intermediate values.
```fs
Seq.scan: ('T -> 'U -> 'T) -> 'T -> 'U seq -> 'T seq
```
So it could be used for example to conceptually consider all future states based on a sequence conceptually containing all future events a user might trigger. `Seq` is generally used to "pull" values from a sequence as you need them whereas `Observable` is used to "push" values onto a sequence as you produce them.