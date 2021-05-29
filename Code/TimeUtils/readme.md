# Timer V2

Nova versão do timer. Esse timer pode ser instanciado em qualquer lugar do código.

Propriedades:

- CurrentTime
- RemainingTime
- Completion

Modo de utilização:

```csharp
// Timer em loop
var loopTimer = new TimerV2(5f, () => {
    // do stuff when complete a loop
})
    .SetName("Loop counter timer")
    .Loop()
    .Start();

// Timer que roda apenas uma vez
var runOnceTimer = new TimerV2(5f, () => {
    // do stuff when finished
})
    .SetName("Loop run once timer")
    .Start();
```

![exemplo de utilização](../../Docs/timer_v2_demo.PNG)