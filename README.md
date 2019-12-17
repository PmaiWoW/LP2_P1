## MyIMDBsearcher

André Vitorino - a21902663  
Hugo Feliciano - a21805809  
Pedro Inácio &nbsp; &nbsp; - a21802056

# Repositório

https://github.com/PmaiWoW/LP2_P1/tree/master

# Distribuição

O programa começou pelo membro Pedro Inácio, inciando o repositório e criando
algumas das `structs` necessárias para guardar os dados dos vários ficheiros. Os
seguintes membros adicionaram o resto das `structs` necessárias para o resto do
projeto, apesar de no final algumas destas `structs` não serem utilizadas e 
eliminadas.

Seguidamente o membro Pedro Inácio criou a classe `static` responsável por fazer
`parse` dos documentos criando o método `LoadTitleBasics` e de seguida o Hugo 
Feliciano criando o método `LoadTitleRatings` para as suas respetivas `structs`,
mais tarde esta classe foi alterada pelo membro André Vitorino para apresentar 
uma barra de carregamento. 

Este membro também iniciou a parte de procura, criando a classe `TitleSearch` 
recebendo um título e procurando num `IEnumerable<TitleBasics>` pelo título 
pretendido, a classe ainda permitia a apresentação de 30 títulos requerendo 
input do utilizador para ver a seguinte página e alguns sorts rudimentares 
para teste. 

O Pedro Inácio criou enums para género e tipo como também implementar sorts para
todos os parametros de `TitleBasics`.

O mebro André Vitorino alterou a classe `TitleSearch` para utilizar `ConsoleKey`
em vez de `Console.ReadLine()` permitindo a utilização de todoas as teclas, 
isto permitiu a implementação de um cursor que se movia para cima e baixo em
que clicando no `Enter` apresentava a informação desse título utilizando uma
nova classe denominada `TitleDetails`. Também passsou os métodos de sort para
para uma classe à parte chamada `Sorter` que mais tarde voltou para a classe
`TitleSearch` de acordo com o feedback fornecido pelo docente.

Seguidamente Pedro Inácio melhorou a lógica e estrutura do código em várias
classes melhorando a legibilidade do código sem aftetar *performace*. >>
E permitindo ao aluno Hugo Feliciano criar filtros de pesquisa dentro da classe
`TitleSearch` depois de fornecer um título, que mais tarde o aluno André 
Vitorino passou para a classe `SearchMenu` responsável por dar ao utilizador
as variáveis especificas que quer pesquisar em forma de menu visual. Por esta
altura também foi tentado várias maneiras de juntar as duas listas com o método
`Join()`.

De seguida o Pedro Inácio cria a classe `UserInterface` responsável pela
maioria das funções gráficas do programa, na qual o Hugo Feliciano altera o
código para ser mais simples e fácil de ler.

Por fim com a ajuda do aluno João Moreira o aluno André Vitorino alterou o
código de maneira a fazer `GroupJoin()` em vez de `Join()`.

Nota: Apenas foram descritas grandes alterações e/ou principais. Todos os 
membros do grupo trabalharam em todo o projeto e feito melhorias ao código uns
dos outros.

# Arquitetura

O programa está essencialmente estruturado por "menus", inputs do utilizador 
levam maioritariamente a novas classes com *ciclos* diferentes de acordo com a 
intenção do seu uso, no entanto se o utilizador escolher alterações ao estado 
atual não sai para outro *ciclo* utilizando apenas métodos para fazer ordenação 
ou mostrar a informação.

Tentou-se ao máximo manter-se o mais genérico possível nas coleções, utilizando 
`IEnumerable<T>` para quase tudo, convertendo estas para `ToList()` ou 
`ToHashSet()` quando necessário.

Cada um dos ficheiros têm um método diferente na classe `FileLoader`, em que lê 
cada frase uma a uma, divide essa frase pelos *tabs* encontrados, e, se 
necessário faz `Parse` dessa string guardando os valores lidos em variáveis 
locais. Por fim constroi uma nova `struct` do tipo que precisa, returnando um 
`IEnumerable<T>` dessa `struct`.

A nível de optimizações, infelizmente não chegámos a optimizar as coleções para 
utilizarem um tamanho especifico. No entanto, em vez de ler e guardar a lista de
 resultados do `FileLoader` todo, o que ocupava entre 3 a 4 GB de memória sempre
 , quando se lê o ficheiro procura-se diretamente o que o utilizador pediu, 
 guardando apenas a lista de resultados que correspondem ao pedido. No entanto 
 este método vem com a desvantagem de quando se procura por um título novo tem 
 de se correr o método da classe `FileLoader` outra vez.

Para de alguma forma negar isto, se o utilizador quiser voltar ao estado 
original antes de ter ordenado ou realizado outras ações, está guardado uma 
cópia da coleção com os títulos e parametros que o utilizador procurou antes de 
o utilizar agir sobre os resultados.

Para apresentar os resultados no ecrâ apenas se envia a lista com o número de 
elementos que se quer apresentar em vez de se enviar a coleção inteira.

