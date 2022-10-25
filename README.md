# UnityFoundation

# Installation

From the root of the project folder run the following command

`git submodule add https://github.com/BrunoBiluca/UnityFoundation.git .\Assets\UnityFoundation `

## Dependencies

### Unity Registry packages

- Cinemachine
- Core RP
- Test Framework ^2
  - package name: com.unity.test-framework
  - package version: 2.0.1-exp.1
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

Other way of import the necessary packages is to download NugetForUnity.

- Nuget for Unity
  - Used to manage external NuGet packages
  - https://github.com/GlitchEnzo/NuGetForUnity
  
After downloading it, update the packages.config file in the Assets folder to the follow:

```xml
<?xml version="1.0" encoding="utf-8"?>
<packages>
  <package id="Castle.Core" version="5.1.0" />
  <package id="CsvHelper" version="29.0.0" />
  <package id="Microsoft.Bcl.AsyncInterfaces" version="1.0.0" />
  <package id="Microsoft.Bcl.HashCode" version="1.0.0" />
  <package id="Moq" version="4.18.2" />
  <package id="System.Diagnostics.EventLog" version="4.7.0" />
</packages>
```

### Add Input System testable packages

Add to `Packages/manifest.json` the following section:

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
