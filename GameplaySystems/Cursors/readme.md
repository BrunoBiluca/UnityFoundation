# Cursores

Cursores são estruturas que mantém coordenadas dentro de um espaço

## Cursor de tela (ScreenCursor)

O cursor de tela é uma estrutura que mapeia coordenadas da tela em demais coordenadas que podem ser utilizada na lógica dos jogos.

### ScreenCursor

> Classe: ScreenCursor

O **ScreenCursor** recebe um **ICursorInput** que provê as coordenadas no espaço mapeado.

#### Conversões

Conversões são adicionadas ao cursor para provêr um maior mapeamento entre os demais espaços.

- BoundariesChecker
- CanvasResolutionConverter
- InvertYConverter
- PivotConverter
- ScreenPositionConverterGroup