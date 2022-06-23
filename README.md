# UnityFoundation

# Installation

## Dependencies

### Unity Registry packages

- Cinemachine
- Core RP
- Test Framework ^2
- Input System
- Text Mesh Pro (TMP)

### C# packages

Create a folder called `Packages` on Assets folder. Drop all dlls.

- Castle.Core.4.4.0
- Microsoft.Bcl.AsyncInterfaces.1.0.0
- Microsoft.Bcl.HashCode.1.0.0
- Microsoft.NETCore.Platforms.1.1.0
- Moq.4.16.1
- NETStandard.Library.1.6.1
- System.Buffers.4.4.0
- System.Memory.4.5.0
- System.Runtime.CompilerServices.Unsafe.4.5.3
- System.Threading.Tasks.Extensions.4.5.4
- Csv Helper
  - Used on Dialgue System export/import features
  - https://joshclose.github.io/CsvHelper/

### Unity Plugins (opcional)

- Nuget for Unity
  - Used to manage external nuget packages
  - https://github.com/GlitchEnzo/NuGetForUnity

### Add Input System testable packages

Add to `Packages/manifest.json` the follow section:

```
"testables": [
  "com.unity.inputsystem"
]
```

# Features

## Procedural platform generation

Location: UnityFoundation -> ProceduralGeneration -> PlatformBuilder -> ProceduralPlatform.prefab

Creates platform grid using the Random Walk Algorithm.

![](Docs/proceduralPlatform.PNG)

## Dialogue System

Location: UnityFoundation -> Systems -> DialogueSystem
Mais informações: UnityFoundation -> Systems -> DialogueSystem -> readme.md

Sistema genérico de Diálogos. Pode ser utilizado apenas importando os scripts.

- Exemplo de diálogo

![](./Docs/dialogue_example.gif)

- Exemplo do Editor de diálogo

![](./Docs/dialogue_editor.png)