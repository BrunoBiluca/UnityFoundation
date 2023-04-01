# UnityFoundation.Code

![](https://img.shields.io/badge/Code%20Coverage-44.6%25-success?style=flat)

O UnityFoundation.Code é o principal pacote disponível na biblioteca do UnityFoundation.
Esse pacote é responsável por conter os principais códigos auxiliares, estruturas de dados e adaptadores da Unity. Além de códigos com propósitos gerais.

Principais funcionalidades:

- Código auxiliar para programação assíncrona
- Variadas estruturas de dados para qualquer tipo de arquitetura
- Funcionalidades genéricas para criação de sistema de jogos
- Principais operações matemáticas
- Adaptação da Unity a fim de proporcionar interfaces mais genéricas e testáveis que a própria Unity não disponibiliza

## Hierarquia de pastas

```
📦 UnityFoundation.Code
 ┣ 📂 _Tests                     # Testes unitários do pacote
 ┣ 📂 Async                      # Programação assíncrona
 ┣ 📂 DataStructures             # Estruturas de dados variadas
 ┣ 📂 Features                   # Códigos diversos para reutilização
 ┣ 📂 Math                       # Códigos matemáticos diversos
 ┣ 📂 UnityAdapter               # Adapter utilizado para Unity
 ┗ 📜readme.md
```

## Uso e explicação

Essa seção visa explicar e demonstrar cada sistema implementado no pacote UnityFoundation.Code.

Os tópicos serão separados por conceito e necessidade.

### Programação assíncrona (async programming)

#### Async Processor
⚒️ `/Async/AsyncProcessor`

#### Loop Condition

### Estruturas de dados

O pacote de estruturas de dados visa disponibilizar estruturas de dados com comportamentos variados de forma a aplicar ao problema a melhor estrutura possível.

#### Árvores (Trees)

Árvores são ótimas para abstrair dados em um formato hierárquico de organização. Cada nodo representa um ponto da árvore e esses nós podem ser organizados para formar vários tipos de comportamentos.

##### BinaryDecisionTree 
⚒️ `/DataStructures/BinaryDecisionTree`

Estrutura de dados utilizada para organizar comportamentos de forma hierárquica binariamente.

Cada nodo da árvore é executado, caso o resultado da execução seja verdadeiro o próximo nodo registro será executado, caso falso será executado o nodo de falha.

Exemplo de uso

```csharp
var decisionTree = new BinaryDecisionTree(
    new BinaryDecision(
        () => {
            Debug.Log("first");
            return true;
        })
        .SetNext(new BinaryDecision(() => {
            Debug.Log("success");
            return true;
        }))
        .SetFailed(new BinaryDecision(() => {
            Debug.Log("failed");
            return true;
        }))
);

decisionTree.Execute();
// result: first
// result: success
```

##### ContextDecisionTree

...TODO

#### Container de Injeção de Dependência
⚒️ `/DataStructures/DependencyContainer`

Inversão de controle é um padrão de projetos que visa modularizar os componentes do projeto onde cada componente passa a receber suas dependências necessárias para a execução, assim cada compomente passa a ser tratado como uma entidade isolada e assim simplesmente testada de forma independente.

O sistema de IoC container implementado separada o container em duas classes:

- DependencyBinder
  - Responsável por registrar os tipos no container
- DependencyContainer
  - Responsável por resolver as instâncias requisitadas pelo cliente

Exemplo de uso

```csharp

class CustomClass {
    public string Field = "campo da classe";
}

DependencyBinder binder = new();
binder.Register("a");
binder.Register(1);
binder.Register<CustomClass>();

IDependencyContainer container = binder.Build();
var customClassInstance = container.Resolve<CustomClass>();

Debug.Log(customClassInstance.Field);
// result: campo da classe
```

Outras funcionalidades

- IDependencySetup, classes que implementam o IDependencySetup recebem a passagem de parâmetros pelo método implementado. Muito utilizado para classes `MonoBehaviour` que sua instanciação é contralada pela Unity.

```csharp
public class CustomClass : MonoBehaviour, IDependencySetup<string, int> {
    public string field1;
    public int field2;

    public void Setup(string param1, int param2) {
        field1 = param1;
        field2 = param2;
    }
}

DependencyBinder binder = new();
binder.Register("passado por parametro");
binder.Register(123456);
binder.Register<CustomClass>();

IDependencyContainer container = binder.Build();
var customClassInstance = container.Resolve<CustomClass>();
Debug.Log(customClassInstance.field1);
Debug.Log(customClassInstance.field2);
// result: passado por parametro
// result: 123456
```

- IContainerProvide, classes que implementam o IContainerProvide recebem a instância do `IDependencyContainer` e podem utilizar esse container para resolver outras instâncias.

#### Grid

#### Optional
⚒️ `/DataStructures/Optional`

Optional é um padrão de projeto que visa entender o comportamento dado o estado de um entidade. Quando dizemos que um valor é opicional, estamos explicitando que este valor pode existir ou não quando consultado. Isso ajuda a garantir que um determinado comportamento não seja executado caso o valor não exista.

Modo de uso

```csharp
var optionalValue = Optional<string>.Some("Manual do Mundo");
optionalValue.Some(value => Debug.Log(value));
// result: Manual do Mundo

var noneValue = Optional<string>.None();
string value = noneValue.OrElse("default value");
Debug.Log(value);
// result: default value
```

#### Promise

### Object Pooling
⚒️ `/Features/ObjectPooling`

Object Pooling é uma técnica muito utilizada no desenvolvimento de games. Ela resolve o problema da instanciação de objetos, que é uma operação muito custosa, de forma que essas instâncias forma um grupo de objetos que podem ser reutilizados em vez de recriados a todo momento.

O sistema de Object Pooling implementado é separado em duas classes

- PooledObject, uma extensão do ``MonoBehaviour` que implementa o comportamento do objeto desejado
- ObjectPooling, classe responsável por gerenciar os objetos instanciados.

Funcionalidades

- PoolSize, número máximo de instâncias do objeto desejado
- CanGrown, configurar para permitir criar objetos quando não existe um objeto disponível para ser reutilizado.

Forma de uso

```csharp
var pooledObject = new GameObject("pooled_object").AddComponent<PooledObject>();
var objectPooling = new GameObject("object_pooling").AddComponent<ObjectPooling>();

objectPooling.Setup(new ObjectPoolingSettings() {
    ObjectPrefab = pooledObject.gameObject,
    PoolSize = 3,
    CanGrown = false
});

objectPooling.InstantiateObjects();
```

### Pathfinder

### Timer

### Math

### UnityAdapter

O UnityAdapter foi desenvolvido com a intensão de separar a lógica do código do jogo de elementos da Unity.

Vários dos componentes da Unity não implementam interface e não são possíveis instanciar, dificultando bastante na criação de testes unitários para esses códigos.

Além disso podemos utilizar o UnityAdapter para extender as principais funcionalidades dos componentes da Unity e criar o nosso próprio comportamento.

```
📦 UnityAdapter
 ┣ 📂 _Interface
 ┣ 📂 Components
 ┣ 📂 Debugging
 ┣ 📂 Extensions
 ┣ 📂 Handlers
 ┣ 📂 Inspector
 ┗ 📜readme.md
```