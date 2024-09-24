
# Documentação do Projeto de Plotagem e Recorte de Linhas

## Visão Geral
Este projeto, desenvolvido em C#, implementa uma variedade de algoritmos clássicos de computação gráfica para plotagem de linhas e círculos, bem como algoritmos de recorte de linhas. Com uma interface gráfica intuitiva, o usuário pode criar formas geométricas, aplicar transformações 2D e visualizar os resultados em tempo real. Abaixo, estão descritas as principais funcionalidades do projeto e como utilizá-las.

## Funcionalidades Principais

### 1. **Plotagem de Linhas e Círculos**
- **Algoritmos de Plotagem de Linhas:**
  - Implementação dos algoritmos *Digital Differential Analyzer (DDA)* e *Bresenham*, que permitem a plotagem precisa e eficiente de linhas, determinando os pontos a serem desenhados entre dois pontos dados.
- **Algoritmo de Plotagem de Círculos:**
  - Algoritmo de Bresenham para a construção de círculos com precisão, baseado em cálculo incremental de pontos ao redor do centro.

### 2. **Transformações 2D**
- **Escalonamento (Escala):**
  - Aumenta ou diminui o tamanho da forma desenhada de acordo com um fator de escala especificado.
- **Translação:**
  - Move a forma desenhada para uma nova posição no plano, mantendo suas dimensões e orientação.
- **Rotação:**
  - Gira a forma em torno de um ponto especificado (normalmente o centro) por um ângulo fornecido.
- **Reflexão:**
  - Espelha a forma em relação a um eixo, como X ou Y, criando um reflexo da forma original.

### 3. **Algoritmos de Recorte de Linhas**
- **Algoritmo Cohen-Sutherland:**
  - Efetua o recorte de linhas em relação a um retângulo de recorte, descartando porções que estão fora da região de interesse e desenhando apenas as partes visíveis.
- **Algoritmo Liang-Barsky:**
  - Alternativa eficiente para recorte de linhas, utilizando parâmetros de interseção para determinar quais partes da linha são visíveis dentro do retângulo de recorte.

### 4. **Métodos Auxiliares**
- **Redimensionamento de Bitmap:**
  - Suporte para ajuste do tamanho do bitmap de desenho, mantendo a proporção e qualidade visual, permitindo manipulação flexível da área de desenho.

## Como Usar

### 1. **Configuração Inicial**
- Certifique-se de que o .NET Framework está instalado em sua máquina.
- Abra a solução do projeto no Visual Studio ou no IDE de sua preferência.
- Compile e execute o aplicativo para abrir a interface gráfica.

### 2. **Criando e Manipulando Formas**
- **Plotagem de Linhas e Círculos:**
  - Use o mouse para definir os pontos iniciais e finais de uma linha ou o centro e raio de um círculo.
  - Selecione o algoritmo desejado para a plotagem (DDA ou Bresenham).
- **Aplicando Transformações:**
  - Desenhe uma forma e selecione-a clicando nela.
  - Escolha a transformação desejada (Escala, Translação, Rotação, Reflexão) e ajuste os parâmetros nos controles apropriados.
- **Utilizando os Algoritmos de Recorte:**
  - Selecione a ferramenta de recorte e defina a região de interesse desenhando um retângulo ao redor da área desejada.
  - Aplique um dos algoritmos de recorte para visualizar apenas a parte da forma que está dentro da região de recorte.

## Dicas Importantes
- Experimente diferentes transformações para observar como elas afetam as formas geométricas no espaço bidimensional.
- Compare os algoritmos de recorte Cohen-Sutherland e Liang-Barsky para ver qual se adapta melhor ao seu caso de uso específico.
- Utilize a funcionalidade de alterar cor para diferenciar as formas desenhadas e facilitar a visualização.

## Estrutura do Projeto

O projeto é composto pelos seguintes componentes principais:
- **Formulário Principal:** Interface gráfica com botões e área de desenho para interação com o usuário.
- **Classe de Algoritmos:** Contém a implementação dos algoritmos de plotagem e recorte.
- **Transformações 2D:** Métodos responsáveis por aplicar as transformações geométricas.

Com essas funcionalidades, o projeto oferece uma ferramenta educativa para o estudo e a visualização de algoritmos de computação gráfica, facilitando a compreensão de conceitos fundamentais de maneira prática e visual.
