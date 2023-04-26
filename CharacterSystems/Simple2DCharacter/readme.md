# Character2D

Definição base de configuração de um personagem 2D utilizando State Pattern para alterar os estados do personagem durante o jogo.

Principais informações:

- BaseCharacter: classe abstrata que garante o comportamento de mudança de estados
- Player: classe base para jogador já com alguns estados implementados.

# Scripts

- CheckGround: script base para checar se o personagem se encontra no chão ou não. É necessário configurar o chão com a tag **ground**.

- FlipWithSprite: script base para chegar se o sprite do personagem está apontando para a esquerda ou para a direita.